using System.Collections.Generic;

namespace Infrastructure.ApiClients.Contracts
{
    public class GetProvinceCityResponse : BaseAsnafResponse
    {
        public List<AsnafProvince> Response { get; set; }
    }
}