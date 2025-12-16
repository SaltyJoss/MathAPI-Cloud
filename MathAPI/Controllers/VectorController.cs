using MathAPI.RequestValidation;
using MathCore;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    [ApiController]
    [Route("linearalgebra/vector")]
    public class VectorController : Controller
    {
        [HttpPost("add")]
        public IActionResult AddVectors([FromBody] VectorRequest request)
        {
            var errA = RequestValidator.ValidateVector(request.VecA);
            if (errA is not null) return errA;

            var errB = RequestValidator.ValidateVector(request.VecB);
            if (errB is not null) return errB;

            if (request.VecA!.Length != request.VecB!.Length)
                return BadRequest(new {error = "VecA and VecB must be of the same length."});

            var result = LinearAlgebra.AddVectors(request.VecA!, request.VecB!).ToArray();
            return Ok(new { result });
        }

        [HttpPost("subtract")]
        public IActionResult SubtractVectors([FromBody] VectorRequest request)
        {
            var errA = RequestValidator.ValidateVector(request.VecA);
            if (errA is not null) return errA;

            var errB = RequestValidator.ValidateVector(request.VecB);
            if (errB is not null) return errB;

            if (request.VecA!.Length != request.VecB!.Length)
                return BadRequest(new { error = "VecA and VecB must be of the same length." });

            var result = LinearAlgebra.SubtractVectors(request.VecA!, request.VecB!).ToArray();
            return Ok(new { result });
        }
        [HttpPost("dot")]
        public IActionResult CalculateDotProduct([FromBody] VectorRequest request)
        {
            var errA = RequestValidator.ValidateVector(request.VecA);
            if (errA is not null) return errA;

            var errB = RequestValidator.ValidateVector(request.VecB);
            if (errB is not null) return errB;

            if (request.VecA!.Length != request.VecB!.Length)
                return BadRequest(new { error = "VecA and VecB must be of the same length." });

            double result = LinearAlgebra.DotProduct(request.VecA!, request.VecB!);
            return Ok(new { result });
        }
    }
}
