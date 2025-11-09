using System.ComponentModel.DataAnnotations;

namespace Application.Utilities.Extensions
{
    internal static class ValidatorHelper<T> where T : class
    {
        public static string Errors(T request)
        {
            string errors = string.Empty;

            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(request);

            var isValid = Validator.TryValidateObject(request, context, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                errors = string.Join(", ", validationResults.Select(e => e.ErrorMessage).ToList());
            }

            return errors;
        }
    }
}
