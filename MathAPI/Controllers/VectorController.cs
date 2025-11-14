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
        public record VectorRequest(string vA, string vB);

        private double[] toVector(string vector)
        {

            string[] values = vector.Split(',');
            double[] vec = new double[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                vec[i] = double.Parse(values[i]);
            }

            return vec;
        }

        [HttpPost("add")]
        public IActionResult AddVectors(VectorRequest request)
        {
            double[] vecA = toVector(request.vA);
            double[] vecB = toVector(request.vB);
            var result = LinearAlgebra.AddVectors(vecA, vecB).ToArray();
            Console.WriteLine($"Result: {result}. VecA: {vecA}, VecB: {vecB}.\n Original values (A,B): {request.vA}, {request.vB}");
            return Ok(new { Result = result });
        }

        [HttpPost("subtract")]
        public IActionResult SubtractVectors(VectorRequest request)
        {
            double[] vecA = toVector(request.vA);
            double[] vecB = toVector(request.vB);
            var result = LinearAlgebra.SubtractVectors(vecA, vecB).ToArray();
            Console.WriteLine($"Result: {result}. VecA: {vecA}, VecB: {vecB}.\n Original values (A,B): {request.vA}, {request.vB}");
            return Ok(new { Result = result });
        }

        // POST endpoint to calculate the dot product of two vectors
        [HttpPost("dot")]
        public IActionResult CalculateDotProduct(VectorRequest request)
        {
            double[] vecA = toVector(request.vA);
            double[] vecB = toVector(request.vB);
            double dotProduct = LinearAlgebra.DotProduct(vecA, vecB);
            Console.WriteLine($"Result: {dotProduct}. VecA: {vecA}, VecB: {vecB}.\n Original values (A,B): {request.vA}, {request.vB}");
            return Ok(new { DotProduct = dotProduct });
        }
    }
}
