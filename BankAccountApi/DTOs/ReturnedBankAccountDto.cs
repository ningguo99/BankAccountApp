namespace BankAccountApi.DTOs
{
    public class ReturnedBankAccountDto
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public int Balance { get; set; }
    }
}