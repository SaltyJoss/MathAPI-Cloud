using MathCore;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    [ApiController]
    [Route("algebra")]
    public class AlgebraController : Controller
    {
        // Request model for algebraic operations
        public record AlgebraRequest(double a, double b);

        [HttpPost("add")]
        public IActionResult Add(AlgebraRequest request)
        {
            var result = Algebra.Add(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("subtract")]
        public IActionResult Subtract(AlgebraRequest request)
        {
            var result = Algebra.Subtract(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("multiply")]
        public IActionResult Multiply(AlgebraRequest request)
        {
            var result = Algebra.Multiply(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("divide")]
        public IActionResult Divide(AlgebraRequest request)
        {
            var result = Algebra.Divide(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("power")]
        public IActionResult Power(AlgebraRequest request)
        {
            var result = Algebra.Power(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("sqrt")]
        public IActionResult SqrRoot(AlgebraRequest request)
        {
            var result = Algebra.SquareRoot(request.a);
            return Ok(new { result });
        }

        [HttpPost("log")]
        public IActionResult Logarithm(AlgebraRequest request)
        {
            var result = Algebra.Logarithm(request.a, request.b);
            return Ok(new { result });

        }
    }
}
