namespace BankAccountApi.Errors
{
    public class ApiException
    {
        public ApiException(long referenceNumber, string message = null, string details = null)
        {
            ReferenceNumber = referenceNumber;
            Message = message;
            Details = details;
        }

        public long ReferenceNumber { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}