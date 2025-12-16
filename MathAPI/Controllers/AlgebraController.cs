using MathAPI.RequestValidation;
using MathCore;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    [ApiController]
    [Route("algebra")]
    public class AlgebraController : Controller
    {
        [HttpPost("add")]
        public IActionResult Add([FromBody] ScalarRequest request)
        {
            var errA = RequestValidator.ValidateScalar(request.a);
            if (errA != null) return errA;

            var errB = RequestValidator.ValidateScalar(request.b);
            if (errB != null) return errB;

            var result = Algebra.Add(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("subtract")]
        public IActionResult Subtract([FromBody] ScalarRequest request)
        {
            var result = Algebra.Subtract(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("multiply")]
        public IActionResult Multiply([FromBody] ScalarRequest request)
        {
            var result = Algebra.Multiply(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("divide")]
        public IActionResult Divide([FromBody] ScalarRequest request)
        {
            var result = Algebra.Divide(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("power")]
        public IActionResult Power([FromBody] ScalarRequest request)
        {
            var result = Algebra.Power(request.a, request.b);
            return Ok(new { result });
        }

        [HttpPost("sqrt")]
        public IActionResult SqrRoot([FromBody] ScalarRequest request)
        {
            var result = Algebra.SquareRoot(request.a);
            return Ok(new { result });
        }

        [HttpPost("log")]
        public IActionResult Logarithm([FromBody] ScalarRequest request)
        {
            var result = Algebra.Logarithm(request.a, request.b);
            return Ok(new { result });

        }
    }
}
