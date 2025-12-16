using System.ComponentModel.DataAnnotations;

namespace MathAPI.RequestValidation
{
    public class ODERequest
    {
        [Required]
        [MinLength(1)]
        public string? func { get; init; }
        [Required]
        [MinLength(1)]
        public double? y0 { get; init; }
        [Required]
        [MinLength(1)]
        public double? t0 { get; init; }
        [Required]
        [MinLength(1)]
        public double? dt { get; init; }
        [Required]
        [MinLength(1)]
        public int? n { get; init; }
    }
}
