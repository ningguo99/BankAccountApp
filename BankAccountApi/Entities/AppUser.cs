using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BankAccountApi.Entities
{
    public class AppUser
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string Username { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
        public ICollection<BankAccount> BankAccounts { get; set; }
        
    }
}