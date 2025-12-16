using System.ComponentModel.DataAnnotations;

namespace MathAPI.RequestValidation
{
    public class VectorRequest
    {
        [Required]
        [MinLength(1)]
        public double[]? VecA { get; init; }
        [Required]
        [MinLength(1)]
        public double[]? VecB { get; init; }
    }
}
