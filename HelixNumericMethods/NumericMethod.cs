using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixNumericMethods
{
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
        /// vector of x-coordinates
        /// </summary>
        protected double[] x;
        /// <summary>
        /// vector of y-coordinates
        /// </summary>
        protected double[] y;
        /// <summary>
        /// head of the x-interval [a,b]
        /// </summary>
        protected double a;
        /// <summary>
        /// tail of the x-interval [a,b]
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
                x = new double[n];
                y = new double[n];
                x[0] = a;
                for (int i = 1; i < n; i++)
                    x[i] = x[i - 1] + h;
            }
        }

        /// <summary>
        /// Accessor to x-array
        /// </summary>
        public double[] x_points
        {
            get
            {
                double[] result = new double[n];
                Array.Copy(x, result, n);
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
    }

    /// <summary>
    /// Class of any abstract numerical solution for the 1st order ODE
    /// </summary>
    public abstract class NumericalMethod
    {
        /// <summary>
        /// Gets the next point y[i+1] of the approximate solution
        /// depending on the previous point (x[i],y[i]) and the step h of the grid
        /// </summary>
        /// <param name="equation">equationFormulaView to be solved</param>
        /// <param name="xi">x-coordinate previous point (x[i], y[i]) of approximate solution</param>
        /// <param name="yi">y-coordinate previous point (x[i], y[i]) of approximate solution</param>
        /// <param name="h">grid step</param>
        /// <returns>value of the approximate solution at the next point y[i+1]</returns>
        public abstract double y_i1(iFunction_X_Y equation, double xi, double yi, double h);
    }
}
