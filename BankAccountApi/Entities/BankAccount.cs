using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankAccountApi.Entities
{
    [Table("BankAccounts")]
    public class BankAccount
    {
        public int Id { get; set; }
        [Required]
        public string AccountName { get; set; }
        [Required]
        public string AccountNumber { get; set; }
        [Required]
        [Range(0, double.MaxValue)]
        public double Balance { get; set; } = 0;
        public AppUser Owner { get; set; }
        public int AppUserId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}