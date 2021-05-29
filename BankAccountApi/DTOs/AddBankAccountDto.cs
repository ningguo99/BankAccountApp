using System.ComponentModel.DataAnnotations;

namespace BankAccountApi.DTOs
{
    public class AddBankAccountDto
    {
        [Required]
        public string AccountName { get; set; }
    }
}