using System.ComponentModel.DataAnnotations;

namespace BankAccountApi.DTOs
{
    public class CreateUserDto
    {
        [Required]
        [MaxLength(10)]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}