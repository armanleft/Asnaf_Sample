namespace Infrastructure.ApiClients.Contracts
{
    public class CancelBranchRequest
    {
        public string Password { get; set; }
        public string CompanyId { get; set; }
        public string BranchCode { get; set; }
        public string CancelFormFileName { get; set; }
        public string CancelForm { get; set; }
    }
}