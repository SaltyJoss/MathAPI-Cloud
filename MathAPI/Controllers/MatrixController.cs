using MathAPI.RequestValidation;
using MathCore;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    [ApiController]
    [Route("linearalgebra/matrix")]
    public class MatrixController : Controller
    {
        // POST endpoint to calculate the determinant of a matrix
        [HttpPost("determinant")]
        public IActionResult CalculateDeterminant([FromBody] MatrixRequest request)
        {
            var errA = RequestValidator.ValidateMatrix(request.MatA, "MatA");
            if (errA is not null) return errA;

            double result = LinearAlgebra.Determinant(request.MatA!);
            return Ok(new { result });
        }

        // POST endpoint to multiply a matrix by a vector
        [HttpPost("multiply")]
        public IActionResult MatrixMultiply([FromBody] MatrixRequest request)
        {
            var errA = RequestValidator.ValidateMatrix(request.MatA, "MatA");
            if (errA is not null) return errA;

            if (request.Vec is null) return BadRequest(new { error = "Vector is required for multiplication operations." });
            var errV = RequestValidator.ValidateVector(request.Vec, "Vector");
            if (errV is not null) return errV;

            var result = LinearAlgebra.MultiplyMatrixByVector(request.MatA!, request.Vec!).ToArray();
            return Ok(new { result });
        }

        // POST endpoint to add two matrices
        [HttpPost("add")]
        public IActionResult MatrixAdd([FromBody] MatrixRequest request)
        {
            var errA = RequestValidator.ValidateMatrix(request.MatA, "MatA");
            if (errA is not null) return errA;

            if (request.MatB is null) return BadRequest(new { error = "MatB is required for addition operations." });
            var errB = RequestValidator.ValidateMatrix(request.MatB, "MatB");
            if (errB is not null) return errB;


            var result = ToJagged(LinearAlgebra.AddMatrices(request.MatA!, request.MatB!).ToArray());
            return Ok(new { result });
        }

        // POST endpoint to transpose a matrix
        [HttpPost("transpose")]
        public IActionResult MatrixTranspose([FromBody] MatrixRequest request)
        {
            var errA = RequestValidator.ValidateMatrix(request.MatA, "MatA");
            if (errA is not null) return errA;

            var result = ToJagged(LinearAlgebra.TransposeMatrix(request.MatA!).ToArray());
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
