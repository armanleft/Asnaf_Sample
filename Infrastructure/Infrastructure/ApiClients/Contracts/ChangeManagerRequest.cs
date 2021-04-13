namespace Infrastructure.ApiClients.Contracts
{
    public class ChangeManagerRequest
    {
        public string Password { get; set; }
        public string CompanyId { get; set; }
        public string BranchCode { get; set; }
        public string ManagerName { get; set; }
        public string ManagerNationalCode { get; set; }
        public string ManagerMobile { get; set; }
        public string NationalCardCopyFileName { get; set; }
        public string IdentificationCopyFileName { get; set; }
        public string EstablishFormFileName { get; set; }
        public string NationalCardCopy { get; set; }
        public string IdentificationCopy { get; set; }
        public string EstablishForm { get; set; }
    }
}