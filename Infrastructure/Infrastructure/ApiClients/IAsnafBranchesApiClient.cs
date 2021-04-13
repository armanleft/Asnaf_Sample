using System.Threading.Tasks;
using Infrastructure.ApiClients.Contracts;

namespace Infrastructure.ApiClients
{
    public interface IAsnafBranchesApiClient
    {
        /// <summary>
        /// دریافت لیست شهر‌ها و استان‌ها
        /// </summary>
        Task<GetProvinceCityResponse> GetProvinceCityListAsync();

        /// <summary>
        /// صدور شعبه
        /// </summary>
        Task<IssuanceResponse> BranchIssuanceAsync(BranchIssuanceRequest request);

        /// <summary>
        /// تغییر مسئول شعبه
        /// </summary>
        Task<ChangeManagerResponse> ChangeManagerAsync(ChangeManagerRequest request);

        /// <summary>
        /// تغییر آدرس شعبه
        /// </summary>
        Task<ChangeAddressResponse> ChangeAddressAsync(ChangeAddressRequest request);

        /// <summary>
        /// مجوز لغو شعبه
        /// </summary>
        Task<CancelResponse> CancelBranchAsync(CancelBranchRequest request);
    }
}