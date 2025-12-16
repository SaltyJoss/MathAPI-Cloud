using System.ComponentModel.DataAnnotations;

namespace MathAPI.RequestValidation
{
    public record ScalarRequest(
        [Required] double a,
        [Required] double b   
    );
}
