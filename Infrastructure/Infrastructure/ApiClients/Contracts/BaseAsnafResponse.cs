namespace Infrastructure.ApiClients.Contracts
{
    public abstract class BaseAsnafResponse
    {
        public int Status { get; set; }
        public int ErrorCode { get; set; }
        public string Message { get; set; }
    }
}