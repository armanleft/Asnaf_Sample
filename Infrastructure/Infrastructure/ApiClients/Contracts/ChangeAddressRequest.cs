namespace Infrastructure.ApiClients.Contracts
{
    public class ChangeAddressRequest
    {
        public string Password { get; set; }
        public string CompanyId { get; set; }
        public string BranchCode { get; set; }
        public string PostalCode { get; set; }
        public string Address { get; set; }
        public string ChangeAddressFormFileName { get; set; }
        public string RentalContractFileName { get; set; }
        public string OfficialNewspaperFileName { get; set; }
        public string ChangeAddressForm { get; set; }
        public string RentalContract { get; set; }
        public string OfficialNewspaper { get; set; }
    }
}