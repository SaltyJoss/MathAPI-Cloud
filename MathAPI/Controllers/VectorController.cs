using MathCore;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    [ApiController]
    [Route("linearalgebra/vector")]
    public class VectorController : Controller
    {
        // Request model for vector operations
        // Request model for vector operations
        public record VectorRequest(double[] VectorA, double[] VectorB);

        [HttpPost("add")]
        public IActionResult AddVectors(VectorRequest request)
        {
            var result = LinearAlgebra.AddVectors(request.VectorA, request.VectorB).ToArray();
            return Ok(new { result });
        }

        [HttpPost("subtract")]
        public IActionResult SubtractVectors(VectorRequest request)
        {
            var result = LinearAlgebra.SubtractVectors(request.VectorA, request.VectorB).ToArray();
            return Ok(new { result });
        }
        [HttpPost("dot")]
        public IActionResult CalculateDotProduct(VectorRequest request)
        {
            double result = LinearAlgebra.DotProduct(request.VectorA, request.VectorB);
            return Ok(new { result });
        }
    }
}
