using HelixSolver;
using HelixToolkit.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace MyNNSolver._2nd_order_equation
{
    public partial class FormSolution : Form
    {
        ScottPlot.WinForms.FormsPlot displacementPlot, velocityPlot, Y_V_Plot;
        private double[] t_points, y_points, v_points;

        public FormSolution(
            List<Tuple<PlottingData, LocalErrors>> local_errors,
            List<Tuple<PlottingData, GlobalErrors>> global_errors,
            List<NumericalSolution> solutions)
        {
            t_points = solutions[3].t_points;
            y_points = solutions[3].y_points;
            v_points = solutions[3].v_points;

            InitializeComponent();
            uC_ball1.displacements = y_points;
            uC_ball1.sliderLenght.Minimum = 0;
            uC_ball1.sliderLenght.Maximum = y_points.Count() - 1;
            uC_ball1.T = y_points.Count() - 1;

            displacementPlot = new ScottPlot.WinForms.FormsPlot { Dock = DockStyle.Fill };
            velocityPlot = new ScottPlot.WinForms.FormsPlot { Dock = DockStyle.Fill };
            Y_V_Plot = new ScottPlot.WinForms.FormsPlot { Dock = DockStyle.Fill };

            Controls.Add(displacementPlot);
            Controls.Add(velocityPlot);
            Controls.Add(Y_V_Plot);

            displacementPlot.Plot.Add.Scatter(t_points, y_points);
            velocityPlot.Plot.Add.Scatter(t_points, solutions[3].v_points.Select(Math.Abs).ToArray());
            Y_V_Plot.Plot.Add.Scatter(y_points, v_points);

            displacementPlot.Plot.Add.ScatterPoints(new double[] { t_points[0] }, new double[] { y_points[0] },
                                                    color: ScottPlot.Color.FromARGB((uint)System.Drawing.Color.Red.ToArgb()));
            velocityPlot.Plot.Add.ScatterPoints(new double[] { t_points[0] }, new double[] { Math.Abs(v_points[0]) },
                                                    color: ScottPlot.Color.FromARGB((uint)System.Drawing.Color.Red.ToArgb()));
            Y_V_Plot.Plot.Add.ScatterPoints(new double[] { y_points[0] }, new double[] { v_points[0] },
                                                    color: ScottPlot.Color.FromARGB((uint)System.Drawing.Color.Red.ToArgb()));

            displacementPlot.Refresh();
            velocityPlot.Refresh();
            Y_V_Plot.Refresh();

            panel_YtoT.Controls.Add(displacementPlot);
            panel_VToT.Controls.Add(velocityPlot);
            panel_YToV.Controls.Add(Y_V_Plot);

            uC_ball1.timestepChanged += new EventHandler(handleTimestepChanged);
        }

        private void handleTimestepChanged(object sender, EventArgs e)
        {
            int timestep = uC_ball1.timestep;

            displacementPlot.Plot.PlottableList.RemoveAt(1);
            velocityPlot.Plot.PlottableList.RemoveAt(1);
            Y_V_Plot.Plot.PlottableList.RemoveAt(1);

            displacementPlot.Plot.Add.ScatterPoints(new double[] { t_points[timestep] }, new double[] { y_points[timestep] }, 
                                                    color: ScottPlot.Color.FromARGB((uint)System.Drawing.Color.Red.ToArgb()));
            velocityPlot.Plot.Add.ScatterPoints(new double[] { t_points[timestep] }, new double[] { Math.Abs(v_points[timestep]) },
                                                    color: ScottPlot.Color.FromARGB((uint)System.Drawing.Color.Red.ToArgb()));
            Y_V_Plot.Plot.Add.ScatterPoints(new double[] { y_points[timestep] }, new double[] { v_points[timestep] },
                                                    color: ScottPlot.Color.FromARGB((uint)System.Drawing.Color.Red.ToArgb()));

            displacementPlot.Refresh();
            velocityPlot.Refresh();
            Y_V_Plot.Refresh();
        }
    }
}
