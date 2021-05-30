using System.ComponentModel.DataAnnotations;
using BankAccountApi.Helpers;

namespace BankAccountApi.DTOs
{
    public class UpdateAddressDto
    {
        [Required]
        [AuState]
        public string State { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{4}$", ErrorMessage = "PostCode must be 4 digits.")]
        public string PostCode { get; set; }
    }
}