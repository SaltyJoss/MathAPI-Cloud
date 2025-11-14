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
        public record MatrixRequest(double[][] MatrixA, double[][] MatrixB, double[]? Vector);


        // POST endpoint to calculate the determinant of a matrix
        [HttpPost("det")]
        public IActionResult CalculateDeterminant(MatrixRequest request)
        {
            double determinant = LinearAlgebra.Determinant(request.MatrixA);
            return Ok(new { Determinant = determinant });
        }

        // POST endpoint to multiply a matrix by a vector
        [HttpPost("multiply")]
        public IActionResult MatrixMultiply(MatrixRequest request)
        {
            var multiply = LinearAlgebra.MultiplyMatrixByVector(request.MatrixA, request.Vector!).ToArray();
            return Ok(new { MatrixMultiply = multiply });
        }

        // POST endpoint to add two matrices
        [HttpPost("add")]
        public IActionResult MatrixAdd(MatrixRequest request)
        {
            var result = LinearAlgebra.AddMatrices(request.MatrixA, request.MatrixB).ToArray();
            return Ok(new { result });
        }

        // POST endpoint to transpose a matrix
        [HttpPost("transpose")]
        public IActionResult MatrixTranspose(MatrixRequest request)
        {
            var transpose = LinearAlgebra.TransposeMatrix(request.MatrixA).ToArray();
            return Ok(new { Transpose = transpose });
        }
    }
}
