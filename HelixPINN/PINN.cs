using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TorchSharp;
using TorchSharp.Modules;
using static TorchSharp.torch;
using static TorchSharp.torch.optim;

namespace HelixPINN
{
    /// <summary>
    /// Class of the Cauchy initial value problem of the first order
    /// </summary>
    public class CauchyProblem
    {
        /// <summary>
        /// A first order equationFormulaView
        /// </summary>
        public iFunction_X_Y_Tensor eq { get; }
        /// <summary>
        /// x0-head of the interval (read only)
        /// </summary>
        public double x0 { get; }
        /// <summary>
        /// y0-initial condition (read only)
        /// </summary>
        public double y0 { get; }
        /// <summary>
        /// xN-tail of the interval (read only)
        /// </summary>
        public double xN { get; }
        /// <summary>
        /// Constructor of the 1st order Cauchy problem
        /// </summary>
        /// <param name="equation"></param>
        /// <param name="x0"></param>
        /// <param name="y0"></param>
        /// <param name="xN"></param>
        public CauchyProblem(iFunction_X_Y_Tensor eq, double x0, double y0, double xN)
        {
            this.eq = eq;
            this.x0 = x0;
            this.y0 = y0;
            this.xN = xN;
        }
    }

    /// <summary>
    /// Equation class for 2x
    /// </summary>
    public class eq1 : iFunction_X_Y_Tensor
    {
        public double f(double x, double y) => 2.0 * x;

        public Tensor f(Tensor x, Tensor y) => torch.tensor(2.0f) * x;
    }

    /// <summary>
    /// Solution class for 2x
    /// </summary>
    public class sol_eq1 : iFunction_X_Y_Z
    {
        public double f(double x, double x0, double y0) => x * x + y0 - x0 * x0;
    }

    // Instance of the CauchyProblem for the first equation
    CauchyProblem cp1 = new CauchyProblem(new eq1(), 1.0, 1.0, 5.0);

    /// <summary>
    /// Equation class for cos(x)
    /// </summary>
    public class eq2 : iFunction_X_Y_Tensor
    {
        public double f(double x, double y) => Math.Cos(x);

        public Tensor f(Tensor x, Tensor y) => torch.cos(x);
    }

    /// <summary>
    /// Solution class for cos(x)
    /// </summary>
    public class sol_eq2 : iFunction_X_Y_Z
    {
        public double f(double x, double x0, double y0) => Math.Sin(x) + y0 - Math.Sin(x0);
    }

    // Instance of the CauchyProblem for the second equation
    CauchyProblem cp2 = new CauchyProblem(new eq2(), 0.0, 1.0, 2 * Math.PI);
}
