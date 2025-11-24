using MathCore;
using Microsoft.AspNetCore.Mvc;

namespace MathAPI.Controllers
{
    [ApiController]
    [Route("calculus")]
    public class CalculusController : Controller
    {
        // Request model for ODE solving
        public record ODERequest(string func, double y0, double t0, double dt, int n);

        // Select ODE by name
        private static double F(string name, double t, double y)
        {
            return name.ToLower() switch
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
                _ => throw new ArgumentException($"Unknown ODE function '{name}'")
            };
        }

        // POST endpoint to solve ODE using Euler's method
        [HttpPost("Euler")]
        public IActionResult SolveODEWithEuler(ODERequest request)
        {
            var result = ODE.EulerMethod((t, y) => F(request.func, t, y), request.y0, request.t0, request.dt, request.n);
            return Ok(new { result });
        }

        // POST endpoint to solve ODE using Improved Euler method
        [HttpPost("Heuns")]
        public IActionResult SolveODEWithImprovedEuler(ODERequest request)
        {
            var result = ODE.ImprovedEulerMethod((t, y) => F(request.func, t, y), request.y0, request.t0, request.dt, request.n);
            return Ok(new { result });
        }

        // POST endpoint to solve ODE using RK2 method
        [HttpPost("RK2")]
        public IActionResult SolveODEWithRK2(ODERequest request)
        {
            var result = ODE.RK2((t, y) => F(request.func, t, y), request.y0, request.t0, request.dt, request.n);
            return Ok(new { result });
        }

        // POST endpoint to solve ODE using RK4 method
        [HttpPost("RK4")]
        public IActionResult SolveODEWithRK4(ODERequest request)
        {
            var result = ODE.RK4((t, y) => F(request.func, t, y), request.y0, request.t0, request.dt, request.n);
            return Ok(new { result });
        }
    }
}