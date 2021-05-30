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
        public string PostCode { get; set; }
    }
}