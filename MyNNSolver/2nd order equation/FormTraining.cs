using HelixSolver;
using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using TorchSharp;
using static TorchSharp.torch;
using static TorchSharp.torch.optim;

namespace MyNNSolver._2nd_order_equation
{
    public partial class Form_Training : Form
    {
        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private List<int> tEpoch = new List<int>();
        private List<double> yLoss = new List<double>();

        private Parameter parameter;
        private Tensor t_train;
        private double best_loss;

        private CauchyProblem cp;
        private HelixEquation helixEquation;
        private ExactSolution exactSolution;
        private NumericalSolution EulerSolution, ImpEuler_Solution, RK_Solution, PINN_solution;
        private NeuralNetwork NN, y_NN, v_NN, best_NN;
        private optim.Optimizer optimizer;
        private torch.Device device;

        private BackgroundWorker backgroundWorker;
        private bool completedTraining;

        public Form_Training(Parameter parameter)
        {
            torch.manual_seed(parameter.random_seed);
            torch.set_default_dtype(torch.float64);

            this.parameter = parameter;
            completedTraining = false;

            double delta, omega0, phi;
            delta = parameter.system_info.c / (2 * parameter.system_info.m);
            omega0 = Math.Sqrt(parameter.system_info.k / parameter.system_info.m);
            phi = Math.Sqrt(Math.Pow(omega0, 2) - Math.Pow(delta, 2));

            helixEquation = new HelixEquation(parameter.system_info.y0, parameter.system_info.v0, delta, omega0, phi);
            cp = new CauchyProblem(helixEquation, 0.0, parameter.system_info.T);

            device = parameter.pinn_info.trainOnGPU ? torch.CUDA: torch.CPU;
            IEnumerable<TorchSharp.Modules.Parameter> model_parameters;

            if (parameter.pinn_info.learn_2nd)
            {
                NN = new NeuralNetwork(parameter.pinn_info.numLayers, parameter.pinn_info.numNeurons, parameter.pinn_info.activationFunc).to(device);
                model_parameters = NN.parameters();

                best_NN = NN;
            }
            else
            {
                y_NN = new NeuralNetwork(parameter.pinn_info.numLayers, parameter.pinn_info.numNeurons, parameter.pinn_info.activationFunc).to(device);
                v_NN = new NeuralNetwork(parameter.pinn_info.numLayers, parameter.pinn_info.numNeurons, parameter.pinn_info.activationFunc).to(device);
                model_parameters = y_NN.parameters().Concat(v_NN.parameters());

                best_NN = y_NN;
            }

            switch (parameter.optim_info.optim_name)
            {
                case "Adam":
                    optimizer = Adam(model_parameters, lr: parameter.optim_info.lr);
                    break;
                case "SGD":
                    optimizer = SGD(model_parameters, learningRate: parameter.optim_info.lr, momentum: parameter.optim_info.momentum);
                    break;
                default:
                    optimizer = LBFGS(model_parameters, lr: parameter.optim_info.lr);
                    break;
            }
            
            t_train = torch.linspace(cp.t0, cp.tN, parameter._N, device: device).view(-1, 1).requires_grad_(true);

            exactSolution = new ExactSolution(cp, helixEquation, 5*parameter._N);

            best_loss = 100.0;

            InitializeComponent();

            formsPlot1 = new ScottPlot.WinForms.FormsPlot { Dock = DockStyle.Fill };

            // create a minor tick generator that places log-distributed minor ticks
            ScottPlot.TickGenerators.LogMinorTickGenerator minorTickGen = new ScottPlot.TickGenerators.LogMinorTickGenerator();

            // create a numeric tick generator that uses our custom minor tick generator
            ScottPlot.TickGenerators.NumericAutomatic tickGen = new ScottPlot.TickGenerators.NumericAutomatic();
            tickGen.MinorTickGenerator = minorTickGen;

            // create a custom tick formatter to set the label text for each tick
            string LogTickLabelFormatter(double y) => $"{Math.Exp(y):N6}";

            // tell our custom tick generator to use our new label formatter
            tickGen.LabelFormatter = LogTickLabelFormatter;

            // tell the left axis to use our custom tick generator
            formsPlot1.Plot.Axes.Left.TickGenerator = tickGen;

            formsPlot1.Plot.Axes.SetLimits(0, parameter.epoch_info.numEpochs + 1, Math.Log(1e-6), Math.Log(20));
            formsPlot1.Refresh();

            panelLossEpoch.Controls.Add(formsPlot1);

            progressBarTraining.Maximum = parameter.epoch_info.numEpochs;

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.WorkerReportsProgress = true;
            backgroundWorker.WorkerSupportsCancellation = true;
            backgroundWorker.DoWork += new DoWorkEventHandler(TrainingProgress);
            backgroundWorker.ProgressChanged += new ProgressChangedEventHandler(TrainingProgressUpdate);
            backgroundWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(endTraining);

            backgroundWorker.RunWorkerAsync();
        }

        void showSolutions()
        {
            EulerSolution = new NumericalSolution(cp, parameter._N*5, new EulersMethod());
            ImpEuler_Solution = new NumericalSolution(cp, parameter._N*5, new ImprovedEulersMethod());
            RK_Solution = new NumericalSolution(cp, parameter._N * 5, new RungeKuttaMethod());
            PINN_solution = new NumericalSolution(getSolution, cp, best_NN, parameter._N * 5);

            formsPlot1.Plot.Clear();
            formsPlot1.Plot.Axes.Left.TickGenerator = new ScottPlot.TickGenerators.NumericAutomatic();

            formsPlot1.Plot.Axes.SetLimits(0, cp.tN, exactSolution.y_points.Min(), exactSolution.y_points.Max());
            formsPlot1.Plot.Legend.Alignment = Alignment.UpperRight;

            var plot = formsPlot1.Plot.Add.Scatter(EulerSolution.t_points, EulerSolution.y_points,
                                        color: ScottPlot.Color.FromARGB((uint)parameter.EulerSol_plotting.color.ToArgb()));
            plot.LegendText = "Euler";
            
            plot = formsPlot1.Plot.Add.Scatter(ImpEuler_Solution.t_points, ImpEuler_Solution.y_points,
                                        color: ScottPlot.Color.FromARGB((uint)parameter.ImprEulerSol_plotting.color.ToArgb()));
            plot.LegendText = "Imp. Euler";
            
            plot = formsPlot1.Plot.Add.Scatter(RK_Solution.t_points, RK_Solution.y_points,
                                        color: ScottPlot.Color.FromARGB((uint)parameter.RK4Sol_plotting.color.ToArgb()));
            plot.LegendText = "RungeKutta";
            
            plot = formsPlot1.Plot.Add.Scatter(exactSolution.t_points, exactSolution.y_points,
                                        color: ScottPlot.Color.FromARGB((uint)parameter.exactSol_plotting.color.ToArgb()));
            plot.LegendText = "Exact Solution";
            
            plot = formsPlot1.Plot.Add.Scatter(PINN_solution.t_points, PINN_solution.y_points,
                                        color: ScottPlot.Color.FromARGB((uint)parameter.PINN_plotting.color.ToArgb()));
            plot.LegendText = "PINN";

            formsPlot1.Refresh();
        }

        private void TrainingProgress(object sender, DoWorkEventArgs e)
        {
            torch.manual_seed(parameter.random_seed);
            torch.set_default_dtype(torch.float64);

            int epochs = parameter.epoch_info.numEpochs;
            int printStep = parameter.epoch_info.printEvery;
            int plotStep = parameter.epoch_info.plotEvery;
            bool learn_2nd = parameter.pinn_info.learn_2nd;

            double m = parameter.system_info.m;
            double c = parameter.system_info.c;
            double k = parameter.system_info.k;
            double y0 = parameter.system_info.y0;
            double v0 = parameter.system_info.v0;

            double lambda1, lambda2, delta;
            lambda1 = 1e-1;
            lambda2 = 1e-4;

            delta = c / (2 * m);

            Func<Tensor> closure = () =>
            {
                optimizer.zero_grad();
                Tensor l = learn_2nd ? loss_2nd() : loss();

                l.backward();
                return l;
            };

            Tensor loss()
            {
                Tensor y = y_NN.forward(t_train);
                Tensor v = v_NN.forward(t_train);

                Tensor yb_loss = torch.square(y[0] - y0);
                Tensor vb_loss = torch.square(v[0] - v0);

                Tensor y_prime_t =
                    torch.autograd.grad(
                        new Tensor[] { y }, new Tensor[] { t_train}, new Tensor[] { torch.ones_like(y) },
                        create_graph: true, retain_graph: true)[0];

                Tensor v_prime_t =
                    torch.autograd.grad(
                        new Tensor[] { v }, new Tensor[] { t_train }, new Tensor[] { torch.ones_like(v) },
                        create_graph: true, retain_graph: true)[0];

                Tensor y_loss = torch.square(y_prime_t - v);
                Tensor v_loss = torch.square(m * v_prime_t + c * v + k * y);

                //Console.WriteLine(
                //      torch.mean(y_loss, dimensions: new long[] { 0 }).ToString(style: TorchSharp.TensorStringStyle.Numpy)
                //    + " "
                //    + torch.mean(v_loss, dimensions: new long[] { 0 }).ToString(style: TorchSharp.TensorStringStyle.Numpy)
                //);

                Tensor PINN_loss = yb_loss + lambda1 * vb_loss + torch.mean((y_loss + lambda2 * v_loss), dimensions: new long[] { 0 });
                return PINN_loss;
            }

            Tensor loss_2nd()
            {
                Tensor NNy = NN.forward(t_train);

                Tensor NNv =
                    torch.autograd.grad(
                        new Tensor[] { NNy }, new Tensor[] { t_train }, new Tensor[] { torch.ones_like(NNy) },
                        create_graph: true, retain_graph: true)[0];

                Tensor NNv_prime_t =
                    torch.autograd.grad(
                        new Tensor[] { NNv }, new Tensor[] { t_train }, new Tensor[] { torch.ones_like(NNv) },
                        create_graph: true, retain_graph: true)[0];

                Tensor yb_loss = torch.square(NNy[0] - y0);
                Tensor vb_loss = torch.square(NNv[0] - v0);

                Tensor y_loss = torch.mean(torch.square(m * NNv_prime_t + c * NNv + k * NNy), dimensions: new long[] {0});

                Tensor PINN_loss = yb_loss + lambda1 * vb_loss + lambda2 * y_loss;
                return PINN_loss;
            }

            DateTime start = DateTime.Now;
            for (int i = 0; i <= epochs; i++)
            {
                optimizer.step(closure);
                optimizer.zero_grad();

                Tensor l = learn_2nd ? loss_2nd() : loss();
                best_NN = learn_2nd ? NN : y_NN;

                double printLoss = double.NaN;
                if (i % printStep == 0 || i % plotStep == 0)
                {
                    printLoss = l.data<double>()[0];
                }

                backgroundWorker.ReportProgress(i, new Tuple<double, int, int>(printLoss, printStep, plotStep));

                if (backgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                
                GC.Collect();
            }
            DateTime end = DateTime.Now;
            backgroundWorker.ReportProgress(-1, $"Learning time: {end - start} sec.\n");
        }

        public Tuple<double[], double[]> getSolution(CauchyProblem cp, NeuralNetwork NN, int n)
        {
            Tensor t_sol = torch.linspace(cp.t0, cp.tN, n, device: device).view(-1, 1).requires_grad_(true);
            Tensor y_nn_sol, v_nn_sol;

            y_nn_sol = NN.forward(t_sol);

            v_nn_sol =
                    torch.autograd.grad(
                        new Tensor[] { y_nn_sol}, new Tensor[] { t_sol }, new Tensor[] { torch.ones_like(y_nn_sol) },
                        create_graph: true)[0];

            TorchSharp.Utils.TensorAccessor<double> y_nn_sol_accessor = y_nn_sol.data<double>();
            TorchSharp.Utils.TensorAccessor<double> v_nn_sol_accessor = v_nn_sol.data<double>();

            double[] y_nn_sol_arr = y_nn_sol_accessor.ToArray();
            double[] v_nn_sol_arr = v_nn_sol_accessor.ToArray();

            return new Tuple<double[], double[]> (y_nn_sol_arr, v_nn_sol_arr);
        }

        private void TrainingProgressUpdate(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage != -1)
            {
                progressBarTraining.Value = e.ProgressPercentage;
                Tuple<double, int, int> context = (Tuple<double, int, int>)e.UserState;
                if (e.ProgressPercentage % context.Item2 == 0)
                {
                    richTB_TrainingLogs.AppendText($"Epoch #{e.ProgressPercentage}: loss: {context.Item1}\n");
                }
                if (e.ProgressPercentage % context.Item3 == 0)
                {
                    tEpoch.Add(e.ProgressPercentage);
                    yLoss.Add(context.Item1);

                    List<double> logYLoss = yLoss.Select((Func<double, double>)Math.Log).ToList();

                    formsPlot1.Plot.Clear();
                    formsPlot1.Plot.Add.Scatter(tEpoch, logYLoss);
                    formsPlot1.Refresh();
                }
            }
            else
            {
                richTB_TrainingLogs.AppendText(e.UserState.ToString());
            }
        }

        private void endTraining(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                completedTraining = true;
                showSolutions();
            }
        }

        private void buttonContunue_Click(object sender, EventArgs e)
        {
            if (completedTraining)
            {
                LocalErrors eu_le1 = new LocalErrors(cp, parameter._N, helixEquation, new EulersMethod());
                LocalErrors imeu_le1 = new LocalErrors(cp, parameter._N, helixEquation, new ImprovedEulersMethod());
                LocalErrors rk_le1 = new LocalErrors(cp, parameter._N, helixEquation, new RungeKuttaMethod());
                LocalErrors pinn_le1 = new LocalErrors(cp, parameter._N, helixEquation, getSolution, NN);

                GlobalErrors eu_ge1 = new GlobalErrors(parameter.Error_plotting.n1, parameter.Error_plotting.n2, cp, helixEquation, new EulersMethod());
                GlobalErrors imeu_ge1 = new GlobalErrors(parameter.Error_plotting.n1, parameter.Error_plotting.n2, cp, helixEquation, new ImprovedEulersMethod());
                GlobalErrors rk_ge1 = new GlobalErrors(parameter.Error_plotting.n1, parameter.Error_plotting.n2, cp, helixEquation, new RungeKuttaMethod());
                GlobalErrors pinn_ge1 = new GlobalErrors(parameter.Error_plotting.n1, parameter.Error_plotting.n2, cp, helixEquation, getSolution, NN);

                List<Tuple<PlottingData, LocalErrors>> localErrors = new List<Tuple<PlottingData, LocalErrors>> {
                    new Tuple<PlottingData, LocalErrors>(parameter.EulerSol_plotting, eu_le1),
                    new Tuple<PlottingData, LocalErrors>(parameter.ImprEulerSol_plotting, imeu_le1),
                    new Tuple<PlottingData, LocalErrors>(parameter.RK4Sol_plotting, rk_le1),
                    new Tuple<PlottingData, LocalErrors>(parameter.PINN_plotting, pinn_le1),
                };

                List<Tuple<PlottingData, GlobalErrors>> globalErrors = new List<Tuple<PlottingData, GlobalErrors>> {
                    new Tuple<PlottingData, GlobalErrors>(parameter.EulerSol_plotting, eu_ge1),
                    new Tuple<PlottingData, GlobalErrors>(parameter.ImprEulerSol_plotting, imeu_ge1),
                    new Tuple<PlottingData, GlobalErrors>(parameter.RK4Sol_plotting, rk_ge1),
                    new Tuple<PlottingData, GlobalErrors>(parameter.PINN_plotting, pinn_ge1),
                };

                List<NumericalSolution> solutions = new List<NumericalSolution>() { 
                    EulerSolution, ImpEuler_Solution, RK_Solution, PINN_solution
                };

                FormSolution formSolution = new FormSolution(localErrors, globalErrors, solutions);
                formSolution.Show();
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            backgroundWorker.CancelAsync();
            GC.Collect();
            Close();
        }
    }
}