using MathAPI.RequestValidation;
using MathCore;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    [ApiController]
    [Route("calculus")]
    public class CalculusController : Controller
    {
        // Select ODE by name
        private static double F(string name, double t, double y) => name.ToLowerInvariant() switch
        {
            "tplusy" => t + y,
            "linear" => 2 * y,
            "sin" => Math.Sin(t),
            "exp" => Math.Exp(t),
            "quad" => y * y,
            "logistic" => y * (1 - y / 10.0),
            "harmonic" => -t,
            "damped" => -0.3 * y,
            "tcosy" => t * Math.Cos(y),
            "cubic" => (y * y * y) - y,
            "forced" => Math.Sin(t) - y,
            "mix" => Math.Exp(t) + y,
            _ => 0.0
        };

        // POST endpoint to solve ODE using Euler's method
        [HttpPost("Euler")]
        public IActionResult SolveODEWithEuler([FromBody] ODERequest request)
        {
            var err = RequestValidator.ValidateODERequest(request);
            if (err is not null) return err;

            var result = ODE.EulerMethod((t, y) => F(request.func!, t, y), request.y0!.Value, request.t0!.Value, request.dt!.Value, request.n!.Value);
            return Ok(new { result });
        }

        // POST endpoint to solve ODE using Improved Euler method`
        [HttpPost("Heuns")]
        public IActionResult SolveODEWithImprovedEuler([FromBody] ODERequest request)
        {
            var err = RequestValidator.ValidateODERequest(request);
            if (err is not null) return err;

            var result = ODE.ImprovedEulerMethod((t, y) => F(request.func!, t, y), request.y0!.Value, request.t0!.Value, request.dt!.Value, request.n!.Value);
            return Ok(new { result });
        }

        // POST endpoint to solve ODE using RK2 method
        [HttpPost("RK2")]
        public IActionResult SolveODEWithRK2([FromBody] ODERequest request)
        {
            var err = RequestValidator.ValidateODERequest(request);
            if (err is not null) return err;

            var result = ODE.RK2((t, y) => F(request.func!, t, y), request.y0!.Value, request.t0!.Value, request.dt!.Value, request.n!.Value);
            return Ok(new { result });
        }

        // POST endpoint to solve ODE using RK4 method
        [HttpPost("RK4")]
        public IActionResult SolveODEWithRK4([FromBody] ODERequest request)
        {
            var err = RequestValidator.ValidateODERequest(request);
            if (err is not null) return err;

            var result = ODE.RK4((t, y) => F(request.func!, t, y), request.y0!.Value, request.t0!.Value, request.dt!.Value, request.n!.Value);
            return Ok(new { result });
        }
    }
}