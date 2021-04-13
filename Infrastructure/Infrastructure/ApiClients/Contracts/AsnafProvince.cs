using System.Collections.Generic;

namespace Infrastructure.ApiClients.Contracts
{
    public class AsnafProvince
    {
        public string ProvinceId { get; set; }
        public string ProvinceTitle { get; set; }
        public List<AsnafCity> ProvinceCity { get; set; }
    }
}