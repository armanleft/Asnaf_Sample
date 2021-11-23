using System.Threading.Tasks;
using Infrastructure.ApiClients.Contracts;

namespace Infrastructure.ApiClients
{
    public interface IAsnafProductsApiClient
    {
        /// <summary>
        /// بارگذاری کالا
        /// </summary>
        Task<ProductResponse> ProductAsync(ProductRequest request);
    }
}