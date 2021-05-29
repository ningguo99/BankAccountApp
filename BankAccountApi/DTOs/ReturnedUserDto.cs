using System;
using System.Collections.Generic;

namespace BankAccountApi.DTOs
{
    public class ReturnedUserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public ICollection<ReturnedBankAccountDto> BankAccounts { get; set; }
    }
}