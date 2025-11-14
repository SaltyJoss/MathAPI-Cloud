using MathCore;
using MathNet.Numerics;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    [ApiController]
    [Route("linearalgebra/vector")]
    public class VectorController : Controller
    {
        // Request model for vector operations
        public record VectorRequest(double[] VectorA, double[] VectorB);

        [HttpPost("add")]
        public IActionResult AddVectors(VectorRequest request)
        {
            var result = LinearAlgebra.AddVectors(request.VectorA, request.VectorB).ToArray();
            return Ok(new { Result = result });
        }

        [HttpPost("subtract")]
        public IActionResult SubtractVectors(VectorRequest request)
        {
            var result = LinearAlgebra.SubtractVectors(request.VectorA, request.VectorB).ToArray();
            return Ok(new { Result = result });
        }

        // POST endpoint to calculate the dot product of two vectors
        [HttpPost("dot")]
        public IActionResult CalculateDotProduct(VectorRequest request)
        {
            double dotProduct = LinearAlgebra.DotProduct(request.VectorA, request.VectorB);
            return Ok(new { DotProduct = dotProduct });
        }
    }
}
