using Microsoft.AspNetCore.Mvc;
using static MathAPI.Controllers.CalculusController;

namespace MathAPI.RequestValidation
{
    // Static class for validating request parameters, added after deployment due to realisation of need for robust validation!
    public static class RequestValidator
    {
        // Set of valid ODE function names.
        private static readonly HashSet<string> AllowedFuncs = new()
        {
            "tplusy", "linear", "sin", "exp", "quad", "logistic",
            "harmonic", "damped", "tcosy", "cubic", "forced", "mix"
        };

        // Validates ODERequest parameters.
        public static IActionResult? ValidateODERequest(ODERequest req)
        {
            if (req is null) return new BadRequestObjectResult("Request body is required.");
            if (string.IsNullOrWhiteSpace(req.func)) return new BadRequestObjectResult("func is required.");
            if (!AllowedFuncs.Contains(req.func)) return new BadRequestObjectResult($"Unknown ODE function '{req.func}'.");
            if (req.y0 is null) return new BadRequestObjectResult("y0 is required.");
            if (req.t0 is null) return new BadRequestObjectResult("t0 is required.");
            if (req.dt is null) return new BadRequestObjectResult("dt is required.");
            if (req.n is null) return new BadRequestObjectResult("n is required.");
            if (req.dt <= 0) return new BadRequestObjectResult("dt must be > 0.");
            if (req.n <= 0) return new BadRequestObjectResult("n must be > 0.");
            return null;
        }

        // Validates that a matrix is non-null, non-empty, and rectangular.
        public static IActionResult? ValidateMatrix(double[][]? M, string paramName = "Matrix")
        {
            if (M is null) return new BadRequestObjectResult($"{paramName} is required");
            if (M.Length == 0)
                return new BadRequestObjectResult($"{paramName} must have at least 1 row");

            int cols = M[0]?.Length ?? 0;
            if (cols == 0)
                return new BadRequestObjectResult($"{paramName} must have at least 1 column");

            for (int i = 1; i < M.Length; i++)
            {
                if (M[i] is null) return new BadRequestObjectResult($"{paramName}[{i}] is null");
                if (M[i].Length != cols)
                    return new BadRequestObjectResult($"{paramName} must be rectangular (row {i} length differs).");
            }

            return null;
        }

        // Validates that a matrix is square.
        public static IActionResult? ValidateSquare(double[][]? M, string paramName = "Matrix")
        {
            var err = ValidateMatrix(M, paramName);
            if (err is not null) return err;
            if (M!.Length != M[0].Length)
                return new BadRequestObjectResult($"{paramName} must be square");
            return null;
        }

        // Validates that a vector is non-null and has at least one element.
        public static IActionResult? ValidateVector(double[]? v, string paramName = "Vector")
        {
            if (v is null) return new BadRequestObjectResult($"{paramName} is required");
            if (v.Length == 0)
                return new BadRequestObjectResult($"{paramName} must have at least 1 element");
            return null;
        }

        // Validates that two vectors are non-null, non-empty, and of the same length.
        public static IActionResult? ValidateSameLength(double[]? a, double[]? b, string nameA = "VectorA", string nameB = "VectorB")
        {
            if (a is null) return new BadRequestObjectResult($"{nameA} is required");
            if (b is null) return new BadRequestObjectResult($"{nameB} is required");
            if (a.Length == 0 || b.Length == 0) return new BadRequestObjectResult("Vectors must be of 2 or 3 Dimensions");
            if (a.Length != b.Length) return new BadRequestObjectResult("Vectors must be the same length");
            return null;
        }

        public static IActionResult? ValidateScalar(double? value, string paramName = "Scalar")
        {
            if (value is null) return new BadRequestObjectResult($"{paramName} is required");
            return null;
        }
    }
}
