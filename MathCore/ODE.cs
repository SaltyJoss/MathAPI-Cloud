using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public class ODE
    {
        // Euler's Method for solving ODEs
        public static double EulerMethod(Func<double, double, double> f, double y0, double t0, double dt, int n)
        {
            double t = t0;
            double y = y0;
            for (int i = 0; i < n; i++)
            {
                y += dt * f(t, y);
                t += dt;
            }
            return y;
        }

        // Runge-Kutta 2nd Order (Improved Euler / Heun's / Trapezoidal) Method for solving ODEs
        public static double ImprovedEulerMethod(Func<double, double, double> f, double y0, double t0, double dt, int n)
        {
            double t = t0;
            double y = y0;
            for (int i = 0; i < n; i++)
            {
                double k1 = f(t, y);
                double k2 = f(t + dt, y + dt * k1);
                y += dt * (k1 + k2) / 2;
                t += dt;
            }
            return y;
        }

        // Runge-Kutta 2nd Order (Midpoint) Method for solving ODEs
        public static double RK2(Func<double, double, double> f, double y0, double t0, double dt, int n)
        {
            double t = t0;
            double y = y0;
            for (int i = 0; i < n; i++)
            {
                double k1 = f(t, y);
                double k2 = f(t + dt / 2, y + dt / 2 * k1);
                y += dt * k2;
                t += dt;
            }
            return y;
        }

        // Runge-Kutta 4th Order Method for solving ODEs
        public static double RK4(Func<double, double, double> f, double y0, double t0, double dt, int n)
        {
            double t = t0;
            double y = y0;
            for (int i = 0; i < n; i++)
            {
                double k1 = dt * f(t, y);
                double k2 = dt * f(t + dt / 2, y + k1 / 2);
                double k3 = dt * f(t + dt / 2, y + k2 / 2);
                double k4 = dt * f(t + dt, y + k3);
                y += (k1 + 2 * k2 + 2 * k3 + k4) / 6;
                t += dt;
            }
            return y;
        }
    }
}
