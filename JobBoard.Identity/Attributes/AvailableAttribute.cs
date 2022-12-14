using System.ComponentModel.DataAnnotations;

namespace JobBoard.Identity.Attributes
{
    public class AvailableAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                var code = new HttpClient().GetAsync(value?.ToString()).Result.StatusCode;

                if (code != System.Net.HttpStatusCode.OK)
                    throw new Exception();

                return ValidationResult.Success;
            }
            catch (Exception)
            {
                return new ValidationResult("Link should be available for opening");
            }
        }
    }
}