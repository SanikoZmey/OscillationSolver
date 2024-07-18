using System.Collections.Generic;
using System.ComponentModel;
using TorchSharp;


namespace MyNNSolver._2nd_order_equation
{
    class TextChoiceConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            // false - allow to write custom
            // true - only predefined optimizers
            return true;
        }
    }

    class OptimTypeConverter : TextChoiceConverter
    {
        public override StandardValuesCollection GetStandardValues(
        ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new List<string> { "SGD", "Adam", "L-BFGS" });
        }
    }
    class ActivationFuncTypeConverter : TextChoiceConverter
    {
        public override StandardValuesCollection GetStandardValues(
        ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(new List<string> { "Tanh", "HardTanh", "Tanhshrink", "Mish", "APTx" });
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class SystemData
    {
        private double _m, _c, _k, _T, _y0, _v0;
        public SystemData()
        {
            _m = 1.0;
            _c = 8.0;
            _k = 1000;
            _T = 1.0;
            _y0 = 3.0;
            _v0 = .0;
        }

        [DisplayName("m")]
        [Description("Object mass(m) in my'' + cy' + ky=0")]
        public double m
        {
            get { return _m; }
            set { _m = value; }
        }
        [DisplayName("c")]
        [Description("Coeffitient of friction(c) in my'' + cy' + ky=0")]
        public double c
        {
            get { return _c; }
            set { _c = value; }
        }
        [DisplayName("k")]
        [Description("Spring stiffness(k) in my'' + cy' + ky=0")]
        public double k
        {
            get { return _k; }
            set { _k = value; }
        }
        [DisplayName("T")]
        [Description("Right bound value for the simulation time interval")]
        public double T
        {
            get { return _T; }
            set { _T = value; }
        }
        [DisplayName("y0")]
        [Description("Initial displacement")]
        public double y0
        {
            get { return _y0; }
            set { _y0 = value; }
        }
        [DisplayName("v0")]
        [Description("Initial velocity")]
        public double v0
        {
            get { return _v0; }
            set { _v0 = value; }
        }
        public override string ToString()
        {
            return "";

        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class OptimData : FilterablePropertyBase
    {
        private string _optim_name;
        private double _lr;
        private double _momentum;
        public OptimData()
        {
            _optim_name = "Adam";
            _lr = 1e-2;
            _momentum = 0.9;
        }

        [DisplayName("Type")]
        [Description("Name of optimizer to be used during the PINN training")]
        [TypeConverter(typeof(OptimTypeConverter))]
        [RefreshProperties(RefreshProperties.All)]
        public string optim_name
        {
            get { return _optim_name; }
            set { _optim_name = value; }
        }

        [DisplayName("Learning rate")]
        [Description("Learning rate to be used during the PINN training")]
        public double lr
        {
            get { return _lr; }
            set { _lr = value; }
        }

        [DisplayName("Momentum")]
        [Description("Momentum for the choosed optimzer(for now only for SGD)")]
        [DynamicPropertyFilter("optim_name", "SGD")]
        public double momentum
        {
            get { return _momentum; }
            set { _momentum = value; }
        }

        public override string ToString()
        {
            return "";
        }

    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PINNData: FilterablePropertyBase
    {
        private int _numLayers, _numNeurons;
        private string _activationFunc;
        private bool _trainGPU, _cuda_available, _learn_2nd;
        public PINNData()
        {
            _cuda_available = torch.cuda_is_available();
            _trainGPU = false;
            _learn_2nd = true;
            _numLayers = 2;
            _numNeurons = 38;
            _activationFunc = "Tanh";
        }

        [Browsable(false)]
        public bool cuda_available
        {
            get { return _cuda_available; }
        }

        [DisplayName("Train on GPU")]
        [Description("Whether to train the PINN on GPU or CPU")]
        [DynamicPropertyFilter("cuda_available", "True")]
        public bool trainOnGPU
        {
            get { return _trainGPU; }
            set { _trainGPU = value; }
        }

        [DisplayName("Solve with one PINN")]
        [Description("Whether to train one PINN with initial equation in loss function or to train 2 PINNs with system of 1st order DEs in loss function")]
        public bool learn_2nd
        {
            get { return _learn_2nd; }
            set { _learn_2nd = value; }
        }

        [DisplayName("Number of layers")]
        [Description("Number of hidden layers of the PINN")]
        public int numLayers
        {
            get { return _numLayers; }
            set { _numLayers = value; }
        }

        [DisplayName("Number of neurons")]
        [Description("Number of output neurons of hidden layers of the PINN")]
        public int numNeurons
        {
            get { return _numNeurons; }
            set { _numNeurons = value; }
        }

        [DisplayName("Activation function")]
        [Description("Activation function of output neurons of hidden layers of the PINN")]
        [TypeConverter(typeof(ActivationFuncTypeConverter))]
        public string activationFunc
        {
            get { return _activationFunc; }
            set { _activationFunc = value; }
        }

        public override string ToString()
        {
            return "";
        }

    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class GeneralTrainingData
    {
        private int _numEpochs;
        private int _printEvery;
        private int _PlotEvery;
        public GeneralTrainingData()
        {
            _numEpochs = 5000;
            _printEvery = 100;
            _PlotEvery = 200;
        }

        [DisplayName("Number of epochs")]
        [Description("Max. number of epoch for which the PINN will be trained")]
        public int numEpochs
        {
            get { return _numEpochs; }
            set { _numEpochs = value; }
        }

        [DisplayName("Print every N epochs")]
        [Description("How many epochs to wait to print current results of the training")]
        public int printEvery
        {
            get { return _printEvery; }
            set { _printEvery = value; }
        }

        [DisplayName("Plot every N epochs")]
        [Description("How many epochs to wait to plot current results of the training")]
        public int plotEvery
        {
            get { return _PlotEvery; }
            set { _PlotEvery = value; }
        }

        public override string ToString()
        {
            return "";
        }

    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class PlottingData
    {
        private string _name;
        private System.Drawing.Color _plotColor;

        public PlottingData(string name)
        {
            _name = name;
            _plotColor = System.Drawing.Color.Red;
        }

        [DisplayName("Plotting color")]
        [Description("A color of solution curve durinig plotting")]
        public System.Drawing.Color color
        {
            get { return _plotColor; }
            set { _plotColor = value; }
        }

        public override string ToString()
        {
            return "";
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class ErrorPlottingData
    {
        private int _n1;
        private int _n2;

        public ErrorPlottingData()
        {
            _n1 = 80;
            _n2 = 120;
        }

        [DisplayName("n1")]
        [Description("Lower bound for x's discretization")]
        public int n1
        {
            get { return _n1; }
            set { _n1 = value; }
        }

        [DisplayName("n2")]
        [Description("Upper bound for x's discretization")]
        public int n2
        {
            get { return _n2; }
            set { _n2 = value; }
        }

        public override string ToString()
        {
            return "";
        }
    }

    public class Parameter
    {
        public Parameter()
        {
            system_info = new SystemData();
            optim_info = new OptimData();
            pinn_info = new PINNData();
            random_seed = 42;
            epoch_info = new GeneralTrainingData();
            _N = 41;
            exactSol_plotting= new PlottingData("Exact Solution");
            EulerSol_plotting = new PlottingData("Euler method Solution") { color = System.Drawing.Color.YellowGreen };
            ImprEulerSol_plotting = new PlottingData("Improved Euler method Solution") { color = System.Drawing.Color.LightSeaGreen };
            RK4Sol_plotting = new PlottingData("RK4 Solution") { color = System.Drawing.Color.SteelBlue };
            PINN_plotting = new PlottingData("PINN Solution") { color = System.Drawing.Color.DarkOrchid };
            Error_plotting = new ErrorPlottingData();
        }

        [DisplayName("Equation coeffs and ICs")]
        [Description("Configuration of the system and its initial conditions")]
        [Category("1. System definition")]
        public SystemData system_info
        {
            get;
            set;
        }

        [DisplayName("Random seed")]
        [Description("Random seed for reproducibility")]
        [Category("2. Training hyperparameters")]
        public int random_seed
        {
            get;
            set;
        }

        [DisplayName("Discr. frequency")]
        [Description("On how many points to divide the interval of X's")]
        [Category("2. Training hyperparameters")]
        public int _N
        {
            get;
            set;
        }

        [DisplayName("Optimizer config")]
        [Description("Optimizer name and its configuration")]
        [Category("2. Training hyperparameters")]
        public OptimData optim_info
        {
            get;
            set;
        }

        [DisplayName("PINN config")]
        [Description("# of hidden layers, output neurons in them, and type of activation function for the neurons")]
        [Category("2. Training hyperparameters")]
        public PINNData pinn_info
        {
            get;
            set;
        }

        [DisplayName("Training process config")]
        [Description("Max. number of epochs and frequencies of logging results of the training")]
        [Category("2. Training hyperparameters")]
        public GeneralTrainingData epoch_info
        {
            get;
            set;
        }

        [DisplayName("Exact Solution")]
        [Description("Plotting color for Exact Solution")]
        [Category("3. Numeric solutions and plotting")]
        public PlottingData exactSol_plotting
        {
            get;
            set;
        }

        [DisplayName("Euler method Solution")]
        [Description("Plotting color for Euler method Solution")]
        [Category("3. Numeric solutions and plotting")]
        public PlottingData EulerSol_plotting
        {
            get;
            set;
        }

        [DisplayName("Impr. Euler method Solution")]
        [Description("Plotting color for Improved Euler method Solution")]
        [Category("3. Numeric solutions and plotting")]
        public PlottingData ImprEulerSol_plotting
        {
            get;
            set;
        }

        [DisplayName("Runge-Kutta's 4 Solution")]
        [Description("Plotting color for Runge-Kutta's 4 Solution")]
        [Category("3. Numeric solutions and plotting")]
        public PlottingData RK4Sol_plotting
        {
            get;
            set;
        }

        [DisplayName("PINN Solution")]
        [Description("Plotting color for PINN Solution")]
        [Category("3. Numeric solutions and plotting")]
        public PlottingData PINN_plotting
        {
            get;
            set;
        }

        [DisplayName("Error plotting")]
        [Description("n1 and n2 as lower and upper bounds for x's discretization")]
        [Category("3. Numeric solutions and plotting")]
        public ErrorPlottingData Error_plotting
        {
            get;
            set;
        }
    }
}
