using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BankAccountApi.Helpers
{
    public class AuStateAttribute : ValidationAttribute
    {
        public string[] AllowableValues { get; set; } = { "NSW", "ACT", "VIC", "QLD", "SA", "WA", "TAS", "NT" };
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (AllowableValues?.Contains(value?.ToString()) == true)
            {
                return ValidationResult.Success;
            }

            var msg = $"Please enter one of the allowable values: {string.Join(", ", (AllowableValues ?? new string[] { "No allowable values found" }))}.";
            return new ValidationResult(msg);
        }
    }
}