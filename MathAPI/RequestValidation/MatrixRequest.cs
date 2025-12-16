using System.ComponentModel.DataAnnotations;

namespace MathAPI.RequestValidation
{
    public class MatrixRequest
    {
        [Required]
        public double[][]? MatA { get; init; }
        public double[][]? MatB { get; init; }
        public double[]? Vec { get; init; }
    }
}
