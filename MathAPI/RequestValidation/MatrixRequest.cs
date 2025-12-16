using System.ComponentModel.DataAnnotations;

namespace MathAPI.RequestValidation
{
    public class MatrixRequest
    {
        [Required]
        [MinLength(1)]
        public double[][]? MatA { get; init; }
        [MinLength(1)]
        public double[][]? MatB { get; init; }
        [MinLength(1)]
        public double[]? Vec { get; init; }
    }
}
