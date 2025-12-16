using System.ComponentModel.DataAnnotations;

namespace MathAPI.RequestValidation
{
    public class VectorRequest
    {
        [Required]
        public double[]? VecA { get; init; }
        [Required]
        public double[]? VecB { get; init; }
    }
}
