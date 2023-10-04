using System.ComponentModel.DataAnnotations;
using Validation.Models;

namespace Validation.Validations
{
    public class KalleAnkaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            const string errorMessage = "Fail from Kalle Anka";

            if (value is string input)
            {
                var model = validationContext.ObjectInstance as Vehicle;

                if (model != null)
                {
                    if (model.OwnerFirstName == "Kalle" && input == "Anka")
                        return ValidationResult.Success;
                    else
                        return new ValidationResult(errorMessage);
                }

            }
            return new ValidationResult(errorMessage);
        }
    }
}