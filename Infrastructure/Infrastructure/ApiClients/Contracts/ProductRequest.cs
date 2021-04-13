namespace Infrastructure.ApiClients.Contracts
{
    public class ProductRequest
    {
        public string Password { get; set; }
        public string CompanyId { get; set; }
        public string Title { get; set; }
        public string IranCode { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string RegistrationDate { get; set; }
        public string BrandOwnerdocFileName { get; set; }
        public string BusinessLicenseFileName { get; set; }
        public string HealthLicensingFileName { get; set; }
        public string CostEstimationFileName { get; set; }
        public string CommitmentFileName { get; set; }
        public string CatalogueFileName { get; set; }
        public string FactorFileName { get; set; }
        public string OtherDocumentFileName { get; set; }
        public string BrandOwnerdoc { get; set; }
        public string BusinessLicense { get; set; }
        public string HealthLicensing { get; set; }
        public string CostEstimation { get; set; }
        public string Commitment { get; set; }
        public string Catalogue { get; set; }
        public string Factor { get; set; }
        public string OtherDocument { get; set; }
    }
}