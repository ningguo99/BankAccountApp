using System;

namespace BankAccountApi.DTOs
{
    public class ReturnedAddressDto
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}