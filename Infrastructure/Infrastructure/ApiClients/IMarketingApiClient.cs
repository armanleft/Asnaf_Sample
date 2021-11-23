using Infrastructure.ApiClients.Contracts;
using System.Threading.Tasks;

namespace Infrastructure.ApiClients
{
    public interface IMarketingApiClient
    {
        /// <summary>
        /// ثبت بازاریاب در وب سرویس بازاریابی
        /// </summary>
        Task<PublicReturnType> RegisterAsync(MarketerRegisterModel request);

        /// <summary>
        ///اطلاع از وضعیت بازاریاب که آیا در شرکتی فعال است یا خیر
        /// </summary>
        Task<MarketerState> GetPersonStatusAsync(string nationalCode);

        /// <summary>
        /// دریافت کد در صورتی که هنگام ثبت کدی برگردانده نشده باشد
        /// </summary>
        Task<PublicReturnType> GetPersonCodeAsync(string nationalCode);

        /// <summary>
        /// گزفتن نام شرکتی که بازاریاب در آن ثبت شده است
        /// </summary>
        Task<PublicReturnType> GetPersonCompanyAsync(string nationalCode);

        /// <summary>
        /// لغو قرارداد بازاریاب
        /// امکان تعلیق بازاریاب به مدت پانزده روز وجود دارد
        /// </summary>
        Task<PublicReturnType> RevokeAsync(string nationalCode);

        /// <summary>
        /// انصراف از لغو قرار داد بازاریاب
        /// </summary>
        Task<PublicReturnType> ReActiveAsync(string nationalCode);
    }
}