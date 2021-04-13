namespace Infrastructure.ApiClients.Contracts
{
    public abstract class BaseAsnafResponseWithRequestId : BaseAsnafResponse
    {
        public string RequestId { get; set; }
    }
}