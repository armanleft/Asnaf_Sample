namespace Infrastructure.ApiClients.Contracts
{
    public class ConferenceIssuanceRequest
    {
        public string Password { get; set; }
        public string CompanyId { get; set; }
        public string Title { get; set; }
        public string Date { get; set; }
        public string Location { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public string ImageFileName { get; set; }
        public string VideoExist { get; set; }
        public string CommitmentFileName { get; set; }
        public string Commitment { get; set; }
    }
}