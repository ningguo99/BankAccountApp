using System;
using System.ComponentModel.DataAnnotations;

namespace BankAccountApi.Entities
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PostCode { get; set; }
        [Required]
        public AppUser Owner { get; set; }
        [Required]
        public int AppUserId { get; set; }
        [Required]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}