using System.ComponentModel.DataAnnotations;

namespace MathAPI.RequestValidation
{
    public class ODERequest
    {
        [Required]
        public string? func { get; init; }
        [Required]
        public double? y0 { get; init; }
        [Required]
        public double? t0 { get; init; }
        [Required]
        [Range(double.Epsilon, double.MaxValue)]
        public double? dt { get; init; }
        [Required]
        [Range(1, int.MaxValue)]
        public int? n { get; init; }
    }
}
