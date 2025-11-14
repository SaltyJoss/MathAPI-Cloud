using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathCore
{
    public class Algebra
    {
        // Adds two numbers
        public static double Add(double a, double b) => a + b;

        // Subs two numbers
        public static double Subtract(double a, double b) => a - b;

        // Multiplies two numbers
        public static double Multiply(double a, double b) => a * b;

        // Divides two numbers
        public static double Divide(double a, double b) => a / b;

        // Raises a number to a power
        public static double Power(double a, double b) => Math.Pow(a, b);

        // Calculates the square root of a number
        public static double SquareRoot(double a) => Math.Sqrt(a);

        // Calculates the logarithm of a number with a specified base
        public static double Logarithm(double a, double baseValue) => Math.Log(a + baseValue);
    }
}
