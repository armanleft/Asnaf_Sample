using Infrastructure.ApiClients.Contracts;
using System.Threading.Tasks;

namespace Infrastructure.ApiClients
{
    public interface IAsnafConferenceApiClient
    {
        /// <summary>
        /// ثبت همایش
        /// </summary>
        Task<ConferenceIssuanceResponse> ConferenceIssuanceAsync(ConferenceIssuanceRequest request);
    }
}