using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccountApi.Entities
{
    [Table("Photos")]
    public class BankAccount
    {
        public int Id { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public int Balance { get; set; }
        public AppUser Owner { get; set; }
        public int AppUserId { get; set; }
    }
}