using MathCore;
using MathNet.Numerics.Providers.LinearAlgebra;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    [ApiController]
    [Route("linearalgebra/matrix")]
    public class MatrixController : Controller
    {
        // Request model for matrix operations
        public record MatrixRequest(double[][] MatrixA, double[][]? MatrixB, double[]? Vector);


        // POST endpoint to calculate the determinant of a matrix
        [HttpPost("determinant")]
        public IActionResult CalculateDeterminant(MatrixRequest request)
        {
            double result = LinearAlgebra.Determinant(request.MatrixA);
            return Ok(new { result });
        }

        // POST endpoint to multiply a matrix by a vector
        [HttpPost("multiply")]
        public IActionResult MatrixMultiply(MatrixRequest request)
        {
            var result = LinearAlgebra.MultiplyMatrixByVector(request.MatrixA, request.Vector!).ToArray();
            return Ok(new { result });
        }

        // POST endpoint to add two matrices
        [HttpPost("add")]
        public IActionResult MatrixAdd(MatrixRequest request)
        {
            var result = ToJagged(LinearAlgebra.AddMatrices(request.MatrixA, request.MatrixB!).ToArray());
            return Ok(new { result });
        }

        // POST endpoint to transpose a matrix
        [HttpPost("transpose")]
        public IActionResult MatrixTranspose(MatrixRequest request)
        {
            var result = ToJagged(LinearAlgebra.TransposeMatrix(request.MatrixA).ToArray());
            return Ok(new { result });
        }

        // Helper method to convert 2D array to jagged array
        private static double[][] ToJagged(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            var jagged = new double[rows][];

            for (int r = 0; r < rows; r++)
            {
                jagged[r] = new double[cols];
                for (int c = 0; c < cols; c++)
                    jagged[r][c] = matrix[r, c];
            }

            return jagged;
        }
    }
}
