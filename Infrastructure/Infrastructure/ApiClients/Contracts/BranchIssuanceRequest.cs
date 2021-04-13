namespace Infrastructure.ApiClients.Contracts
{
    public class BranchIssuanceRequest
    {
        public string Password { get; set; }
        public string CompanyId { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string ManagerName { get; set; }
        public string Mobile { get; set; }
        public string NationalId { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string NationalCardCopyFileName { get; set; }
        public string IdentificationCopyFileName { get; set; }
        public string ObligationFormFileName { get; set; }
        public string EstablishFormFileName { get; set; }
        public string RentalContractFileName { get; set; }
        public string OfficialNewspaperFileName { get; set; }
        public string NationalCardCopy { get; set; }
        public string IdentificationCopy { get; set; }
        public string ObligationForm { get; set; }
        public string EstablishForm { get; set; }
        public string RentalContract { get; set; }
        public string OfficialNewspaper { get; set; }
    }
}