namespace HelixSolver
{
    /// <summary>
    /// Class of any abstract numerical solution for the 1st order ODE
    /// </summary>
    public abstract class NumericalMethod
    {
        /// <summary>
        /// Gets the next point y[i+1] of the approximate solution
        /// depending on the previous point (t[i],y[i]) and the step h of the grid
        /// </summary>
        /// <param name="equation">equationFormulaView to be solved</param>
        /// <param name="ti">time-step of previous point (t[i], y[i]) of approximate solution</param>
        /// <param name="yi">y-coordinate of previous point (t[i], y[i]) of approximate solution</param>
        /// <param name="h">grid step</param>
        /// <returns>value of the approximate solution at the next point y[i+1]</returns>
        public abstract double y_i1(HelixEquation equation, double ti, double yi, double h);
       
        /// <summary>
        /// Gets the next point v[i+1] of the approximate solution
        /// depending on the previous point (t[i],v[i]) and the step h of the grid
        /// </summary>
        /// <param name="equation">equationFormulaView to be solved</param>
        /// <param name="ti">time-step of previous point (t[i], v[i]) of approximate solution</param>
        /// <param name="vi">v-coordinate of previous point (t[i], v[i]) of approximate solution</param>
        /// <param name="h">grid step</param>
        /// <returns>value of the approximate solution at the next point v[i+1]</returns>
        public abstract double v_i1(HelixEquation equation, double ti, double vi, double h);
    }

    /// <summary>
    /// Class of the Euler's method
    /// </summary>
    public class EulersMethod : NumericalMethod
    {
        /// <summary>
        /// Euler's method of computation of the next point y[i+1] of approximate solutionFormulaView
        /// </summary>
        /// <param name="eq">equationFormulaView to be solved</param>
        /// <param name="ti">time-step of previous point (t[i], y[i]) of approximate solution</param>
        /// <param name="yi">y-coordinate of previous point (t[i], y[i]) of approximate solution</param>
        /// <param name="h">grid step</param>
        /// <returns>value of the approximate solution at the next point y[i+1]</returns>
        public override double y_i1(HelixEquation eq, double ti, double yi, double h)
        {
            return yi + h * eq.y_prime(ti);
        }

        /// <summary>
        /// Euler's method of computation of the next point v[i+1] of approximate solutionFormulaView
        /// </summary>
        /// <param name="eq">equationFormulaView to be solved</param>
        /// <param name="ti">time-step of previous point (t[i], v[i]) of approximate solution</param>
        /// <param name="vi">v-coordinate of previous point (t[i], v[i]) of approximate solution</param>
        /// <param name="h">grid step</param>
        /// <returns>value of the approximate solution at the next point v[i+1]</returns>
        public override double v_i1(HelixEquation eq, double ti, double vi, double h)
        {
            return vi + h * eq.y_double_prime(ti);
        }
    }

    /// <summary>
    /// Class of the improved Euler's method
    /// </summary>
    public class ImprovedEulersMethod : NumericalMethod
    {
        /// <summary>
        /// Improved Euler's method of computation of the next point y[i+1] of approximate solutionFormulaView
        /// </summary>
        /// <param name="eq">equationFormulaView to be solved</param>
        /// <param name="ti">time-step of previous point (t[i], y[i]) of approximate solution</param>
        /// <param name="yi">y-coordinate of previous point (t[i], y[i]) of approximate solution</param>
        /// <param name="h">grid step</param>
        /// <returns>value of the approximate solution at the next point y[i+1]</returns>
        public override double y_i1(HelixEquation eq, double ti, double yi, double h)
        {
            double k1i = eq.y_prime(ti);
            double k2i = eq.y_prime(ti + h);
            return yi + h / 2 * (k1i + k2i);
        }

        /// <summary>
        /// Improved Euler's method of computation of the next point v[i+1] of approximate solutionFormulaView
        /// </summary>
        /// <param name="eq">equationFormulaView to be solved</param>
        /// <param name="ti">time-step of previous point (t[i], v[i]) of approximate solution</param>
        /// <param name="vi">v-coordinate of previous point (t[i], v[i]) of approximate solution</param>
        /// <param name="h">grid step</param>
        /// <returns>value of the approximate solution at the next point v[i+1]</returns>
        public override double v_i1(HelixEquation eq, double ti, double vi, double h)
        {
            double k1i = eq.y_double_prime(ti);
            double k2i = eq.y_double_prime(ti + h);
            return vi + h / 2 * (k1i + k2i);
        }
    }

    /// <summary>
    /// Class of the Runge-Kutta method
    /// </summary>
    public class RungeKuttaMethod : NumericalMethod
    {
        /// <summary>
        /// Runge-Kutta method of computation of the next point y[i+1] of approximate solutionFormulaView
        /// </summary>
        /// <param name="eq">equationFormulaView to be solved</param>
        /// <param name="ti">time-step of previous point (t[i], y[i]) of approximate solution</param>
        /// <param name="yi">y-coordinate of previous point (t[i], y[i]) of approximate solution</param>
        /// <param name="h">grid step</param>
        /// <returns>value of the approximate solution at the next point y[i+1]</returns>
        public override double y_i1(HelixEquation eq, double ti, double yi, double h)
        {
            double k1i = eq.y_prime(ti);
            double k2i = eq.y_prime(ti + h / 2);
            double k3i = eq.y_prime(ti + h / 2);
            double k4i = eq.y_prime(ti + h);
            return yi + h / 6 * (k1i + 2 * k2i + 2 * k3i + k4i);
        }

        /// <summary>
        /// Runge-Kutta method of computation of the next point v[i+1] of approximate solutionFormulaView
        /// </summary>
        /// <param name="eq">equationFormulaView to be solved</param>
        /// <param name="ti">time-step of previous point (t[i], v[i]) of approximate solution</param>
        /// <param name="vi">v-coordinate of previous point (t[i], v[i]) of approximate solution</param>
        /// <param name="h">grid step</param>
        /// <returns>value of the approximate solution at the next point v[i+1]</returns>
        public override double v_i1(HelixEquation eq, double ti, double vi, double h)
        {
            double k1i = eq.y_double_prime(ti);
            double k2i = eq.y_double_prime(ti + h / 2);
            double k3i = eq.y_double_prime(ti + h / 2);
            double k4i = eq.y_double_prime(ti + h);
            return vi + h / 6 * (k1i + 2 * k2i + 2 * k3i + k4i);
        }
    }
}
