using MathNet.Numerics.LinearAlgebra;

namespace MathCore
{
    public class LinearAlgebra
    {
        // Adds two vectors of the same length.
        public static double[] AddVectors(double[] vectorA, double[] vectorB) => vectorA.Zip(vectorB, (a, b) => a + b).ToArray();

        // Subtracts two vectors element-wise at a given index.
        public static double[] SubtractVectors(double[] vectorA, double[] vectorB) => vectorA.Zip(vectorB, (a, b) => a - b).ToArray();

        // Calculates the dot product of two vectors.
        public static double DotProduct(double[] vectorA, double[] vectorB)
        {
            var vA = Vector<double>.Build.Dense(vectorA);
            var vB = Vector<double>.Build.Dense(vectorB);
            return vA.DotProduct(vB);
        }

        // Calculates the determinant of a square matrix.
        public static double Determinant(double[][] matrix)
        {
            var m = Matrix<double>.Build.DenseOfRowArrays(matrix);
            return m.Determinant();
        }

        // Multiplies a matrix by a vector.
        public static Vector<double> MultiplyMatrixByVector(double[][] matrix, double[] vector)
        {
            var m = Matrix<double>.Build.DenseOfRowArrays(matrix);
            var v = Vector<double>.Build.Dense(vector);
            return m * v;
        }

        // Adds two matrices of the same dimensions.
        public static Matrix<double> AddMatrices(double[][] matrixA, double[][] matrixB)
        {
            var mA = Matrix<double>.Build.DenseOfRowArrays(matrixA);
            var mB = Matrix<double>.Build.DenseOfRowArrays(matrixB);
            return mA + mB;
        }

        // Transposes a given matrix.
        public static Matrix<double> TransposeMatrix(double[][] matrix)
        {
            var m = Matrix<double>.Build.DenseOfRowArrays(matrix);
            return m.Transpose();
        }
    }
}
