using System;
using System.Linq;
using TorchSharp;
using static TorchSharp.torch;

namespace HelixSolver
{

    /// <summary>
    /// Interface of a function for calculating y and its 1st and 2nd derivatives
    /// </summary>
    public interface Equation
    {
        /// <summary>
        /// Calculates y and its 1st and 2nd derivatives
        /// </summary>
        /// <param name="t">time-step</param>
        /// <param name="y">definition of calculating a value of y</param>
        /// <param name="y_prime">definition of calculating a value of 1st derivative of y</param>
        /// <param name="y_double_prime">definition of calculating value of 2nd derivative of y</param>
        /// <returns>the value of y or its 1st/2nd derivative</returns>

        double y(double t);
        Tensor y(Tensor t);

        double y_prime(double t);
        Tensor y_prime(Tensor t);

        double y_double_prime(double t);
        Tensor y_double_prime(Tensor t);
    }

    public class HelixEquation : Equation
    {
        /// <summary>
        /// Initial displacement (private, read only)
        /// </summary>
        private double _y0;
        /// <summary>
        /// Initial velocity(or y') (private, read only)
        /// </summary>
        private double _v0;
        /// <summary>
        /// delta = c / 2m (private, read only)
        /// </summary>
        private double _delta;
        /// <summary>
        /// omega0 = sqrt(k / m) (private, read only)
        /// </summary>
        private double _omega0;
        /// <summary>
        /// Angle for oscilation defining (private, read only)
        /// </summary>
        private double _phi;


        /// <summary>
        /// Initial displacement (public, read only)
        /// </summary>
        public double y0 { get { return _y0; } }
        /// <summary>
        /// Initial velocity(or y') (public, read only)
        /// </summary>
        public double v0 { get { return _v0; } }
        /// <summary>
        /// delta = c / 2m (public, read only)
        /// </summary>
        public double delta { get { return _delta; } }
        /// <summary>
        /// omega0 = sqrt(k / m) (public, read only)
        /// </summary>
        public double omega0 { get { return _omega0; } }
        /// <summary>
        /// Angle for oscilation defining (public, read only)
        /// </summary>
        public double phi { get { return _phi; } }

        /// <summary>
        /// Constructor of the HelixEquation
        /// </summary>
        /// <param name="y0">Initial displacement</param>
        /// <param name="v0">Initial velocity</param>
        /// <param name="delta">Precalculated coefficient</param>
        /// <param name="omega0">Precalculated coefficient</param>
        /// <param name="phi">Precalculated angle for oscillations</param>
        public HelixEquation(double y0, double v0, double delta, double omega0, double phi)
        {
            _y0 = y0;
            _v0 = v0;
            _delta = delta;
            _omega0 = omega0;
            _phi = phi;
        }

        /// <summary>
        /// Method for calculating a value of y on a particular time-step
        /// </summary>
        /// <param name="t">time-step</param>
        /// <returns>numeric value of y</returns>
        public double y(double t) =>
            Math.Pow(Math.E, -delta * t) * (y0 * Math.Cos(phi * t) + (v0 + delta * y0) / phi * Math.Sin(phi * t));

        /// <summary>
        /// Method for calculating a tensor of value(-s) of y on a particular time-step
        /// </summary>
        /// <param name="t">time-step</param>
        /// <returns>Tensor of value(-s) of y</returns>
        public Tensor y(Tensor t) =>
            torch.pow(Math.E, -delta * t) * (y0 * torch.cos(phi * t) + (v0 + delta * y0) / phi * torch.sin(phi * t));

        /// <summary>
        /// Method for calculating a value of y' on a particular time-step
        /// </summary>
        /// <param name="t">time-step</param>
        /// <returns>numeric value of y'</returns>
        public double y_prime(double t) =>
            Math.Pow(Math.E, -delta * t) * (v0 * Math.Cos(phi * t) - (delta * v0 + Math.Pow(omega0, 2) * y0) / phi * Math.Sin(phi * t));

        /// <summary>
        /// Method for calculating a tensor of value(-s) of y' on a particular time-step
        /// </summary>
        /// <param name="t">time-step</param>
        /// <returns>Tensor of value(-s) of y'</returns>
        public Tensor y_prime(Tensor t) =>
            torch.pow(Math.E, -delta * t) * (v0 * torch.cos(phi * t) - (delta * v0 + torch.square(omega0)* y0) / phi * torch.sin(phi * t));

        /// <summary>
        /// Method for calculating a value of y'' on a particular time-step
        /// </summary>
        /// <param name="t">time-step</param>
        /// <returns>numeric value of y''</returns>
        public double y_double_prime(double t) =>
            (-2 * delta) * y_prime(t) - Math.Pow(omega0, 2) * y(t);

        /// <summary>
        /// Method for calculating a tensor of value(-s) of y'' on a particular time-step
        /// </summary>
        /// <param name="t">time-step</param>
        /// <returns>Tensor of value(-s) of y''</returns>
        public Tensor y_double_prime(Tensor t) =>
            (-2 * delta) * y_prime(t) - torch.square(omega0) * y(t);
    }


    /// <summary>
    /// Class of the Cauchy initial value problem of the first order
    /// </summary>
    public class CauchyProblem
    {
        /// <summary>
        /// A 2nd order equationFormulaView
        /// </summary>
        public HelixEquation eq { get; }
        /// <summary>
        /// Initial time-step (read only)
        /// </summary>
        public double t0 { get; }
        /// <summary>
        /// Initial displacement (read only)
        /// </summary>
        public double y0 { get; }
        /// <summary>
        /// Initial velocity(or y') (read only)
        /// </summary>
        public double v0 { get; }
        /// <summary>
        /// Tail of time-step interval (read only)
        /// </summary>
        public double tN { get; }
        /// <summary>
        /// Constructor of the 2nd order Cauchy problem
        /// </summary>
        /// <param name="equation"></param>
        /// <param name="t0"></param>
        /// <param name="tN"></param>
        public CauchyProblem(HelixEquation eq, double t0, double tN)
        {
            this.eq = eq;
            this.t0 = t0;
            this.tN = tN;
            y0 = eq.y0;
            v0 = eq.v0;
        }
    }

    /// <summary>
    /// Class of the grid
    /// </summary>
    public class Grid
    {
        /// <summary>
        /// number of grid cells
        /// </summary>
        protected int n;
        /// <summary>
        /// vector of time-steps
        /// </summary>
        protected double[] t;
        /// <summary>
        /// vector of y-coordinates
        /// </summary>
        protected double[] y;
        /// <summary>
        /// vector of v-values
        /// </summary>
        protected double[] v;
        /// <summary>
        /// head of the t-interval [a,b]
        /// </summary>
        protected double a;
        /// <summary>
        /// tail of the t-interval [a,b]
        /// </summary>
        protected double b;
        /// <summary>
        /// step of the grid
        /// </summary>
        public double h
        {
            get { return (b - a) / (n - 1); }
        }

        /// <summary>
        /// X-array constructor
        /// </summary>
        /// <param name="a">the head a of the interval [a, b]</param>
        /// <param name="b">the tail b of the iterval [a, b]</param>
        /// <param name="n">the number of grid cells</param>
        public Grid(double a, double b, int n)
        {
            // initialization of the parameters
            this.a = a;
            this.b = b;
            this.n = n;
            // fulfilling the arrays of the grid
            if (n <= 0)
                throw new Exception("Incorrect grid dimension!");
            else
            {
                t = new double[n];
                y = new double[n];
                v = new double[n];
                t[0] = a;
                for (int i = 1; i < n; i++)
                    t[i] = t[i - 1] + h;
            }
        }

        /// <summary>
        /// Accessor to t-array
        /// </summary>
        public double[] t_points
        {
            get
            {
                double[] result = new double[n];
                Array.Copy(t, result, n);
                return result;
            }
        }
        /// <summary>
        /// Accessor to y-array
        /// </summary>
        public double[] y_points
        {
            get
            {
                double[] result = new double[n];
                Array.Copy(y, result, n);
                return result;
            }
        }
        /// <summary>
        /// Accessor to v-array
        /// </summary>
        public double[] v_points
        {
            get
            {
                double[] result = new double[n];
                Array.Copy(v, result, n);
                return result;
            }
        }
    }

    /// <summary>
    /// Class of the numerical solution
    /// </summary>
    public class NumericalSolution : Grid
    {
        /// <summary>
        /// Constructor of the numerical solution of the 1st order
        /// </summary>
        /// <param name="ivp">Cauchy problem to be solved</param>
        /// <param name="n">grid size</param>
        /// <param name="nm">applied numerical method of the 2nd order</param>
        public NumericalSolution(CauchyProblem ivp, int n,
            NumericalMethod nm) : base(ivp.t0, ivp.tN, n)
        {
            y[0] = ivp.y0;
            v[0] = ivp.v0;
            for (int i = 1; i < n; i++)
            {
                y[i] = nm.y_i1(ivp.eq, t[i - 1], y[i - 1], h);
                v[i] = nm.v_i1(ivp.eq, t[i - 1], v[i - 1], h);
            }
        }

        public NumericalSolution(Func<CauchyProblem, NeuralNetwork, int, Tuple<double[], double[]>> getSolution, 
                                 CauchyProblem ivp, NeuralNetwork NN, int n) : base(ivp.t0, ivp.tN, n)
        {
            Tuple<double[], double[]> sol = getSolution(ivp, NN, n);
            double[] sol_y = sol.Item1;
            double[] sol_v = sol.Item2;
            for (int i = 0; i < n; i++)
            {
                y[i] = sol_y[i];
                v[i] = sol_v[i];
            }
        }
    }

    /// <summary>
    /// Class of exact solutionFormulaView of the Cauchy problem of the 2nd order
    /// </summary>
    public class ExactSolution : Grid
    {
        /// <summary>
        /// Constructor of the exact solutionFormulaView of the Cauchy problem of 2nd order
        /// </summary>
        /// <param name="ivp">Cauchy problem to be solved</param>
        /// <param name="sol">Solution in x, x0, y0</param>
        /// <param name="n">Grid size</param>
        public ExactSolution(CauchyProblem ivp, HelixEquation sol, int n)
            : base(ivp.t0, ivp.tN, n)
        {
            for (int i = 0; i < n; i++)
            {
                y[i] = sol.y(t[i]);
                v[i] = sol.y_prime(t[i]);
            }
        }
    }

    /// <summary>
    /// Local errors of the given numerical method
    /// </summary>
    public class LocalErrors : Grid
    {
        /// <summary>
        /// Constructor of the class for calculating local errors for a particular numeric and single exact solutions
        /// </summary>
        /// <param name="ivp">Cauchy problem to be solved</param>
        /// <param name="n">grid size</param>
        /// <param name="nm">applied numerical method of the 2nd order</param>
        /// <param name="f_exSol">Exact solution of a Cauchy problem</param>
        public LocalErrors(CauchyProblem ivp, int n,
            HelixEquation f_exSol, NumericalMethod nm) : base(ivp.t0, ivp.tN, n)
        {
            NumericalSolution nmSol = new NumericalSolution(ivp, n, nm);
            ExactSolution exSol = new ExactSolution(ivp, f_exSol, n);
            for (int i = 0; i < n; i++)
            {
                y[i] = Math.Abs(exSol.y_points[i] - nmSol.y_points[i]);
                v[i] = Math.Abs(exSol.v_points[i] - nmSol.v_points[i]);
            }
        }

        public LocalErrors(CauchyProblem ivp, int n,
            HelixEquation f_exSol, Func<CauchyProblem, NeuralNetwork, int, Tuple<double[], double[]>> getSolution, NeuralNetwork NN) : base(ivp.t0, ivp.tN, n)
        {
            NumericalSolution pinnSol = new NumericalSolution(getSolution, ivp, NN, n);
            ExactSolution exSol = new ExactSolution(ivp, f_exSol, n);
            for (int i = 0; i < n; i++) 
            {
                y[i] = Math.Abs(exSol.y_points[i] - pinnSol.y_points[i]);
                v[i] = Math.Abs(exSol.v_points[i] - pinnSol.v_points[i]);
            }
        }

        /// <summary>
        /// Maximal error
        /// </summary>
        public Tuple<double, double> MaxError
        {
            get { return new Tuple<double, double> (y_points.Max(), v_points.Max()); }
        }
    }

    public class GlobalErrors : Grid
    {
        /// <summary>
        /// Constructor of the class for calculating global errors for a particular numeric and single exact solutions
        /// </summary>
        /// <param name="ivp">Cauchy problem to be solved</param>
        /// <param name="n0">head of the discretization interval [n0, nN]</param>
        /// <param name="nN">tail of the discretization interval [n0, nN]</param>
        /// <param name="nm">applied numerical method of the 2nd order</param>
        /// <param name="f_exSol">Exact solution of a Cauchy problem</param>
        public GlobalErrors(int n0, int nN, CauchyProblem ivp,
            HelixEquation f_exSol, NumericalMethod nm) : base(n0, nN, nN - n0)
        {
            for (int i = n0; i < nN; i++)
            {
                Tuple<double, double> max_errors = new LocalErrors(ivp, i, f_exSol, nm).MaxError;
                y[i - n0] = max_errors.Item1;
                v[i - n0] = max_errors.Item2;
            }
        }

        public GlobalErrors(int n0, int nN, CauchyProblem ivp,
            HelixEquation f_exSol, Func<CauchyProblem, NeuralNetwork, int, Tuple<double[], double[]>> getSolution, NeuralNetwork NN) : base(n0, nN, nN - n0)
        {
            for (int i = n0; i < nN; i++)
            {
                Tuple<double, double> max_errors = new LocalErrors(ivp, i, f_exSol, getSolution, NN).MaxError;
                y[i - n0] = max_errors.Item1;
                v[i - n0] = max_errors.Item2;
            }
        }
    }
}
