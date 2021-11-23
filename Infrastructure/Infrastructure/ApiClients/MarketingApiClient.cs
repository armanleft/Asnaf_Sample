using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Infrastructure.ApiClients.Contracts;
using MarketingService;

namespace Infrastructure.ApiClients
{
    public class MarketingApiClient : IMarketingApiClient
    {
        #region Fields

        private const string ServiceUrl = "http://unicode.mimt.gov.ir/nmir.asmx";
        private const string Username = "یوز نیم خودتون رو اینجا وارد کنید";
        private const string Password = "پسوردتون رو اینجا وارد کنید ولی به صورت دیفالت فکر کنم این باشه: 12345678";

        private readonly EndpointAddress _endpointAddress;
        private readonly BasicHttpBinding _basicHttpBinding;

        #endregion

        #region Constructors

        public MarketingApiClient()
        {
            _endpointAddress = new EndpointAddress(ServiceUrl);

            _basicHttpBinding =
                new BasicHttpBinding(_endpointAddress.Uri.Scheme.ToLower() == "http" ?
                    BasicHttpSecurityMode.None :
                    BasicHttpSecurityMode.Transport)
                {
                    OpenTimeout = TimeSpan.MaxValue,
                    CloseTimeout = TimeSpan.MaxValue,
                    ReceiveTimeout = TimeSpan.MaxValue,
                    SendTimeout = TimeSpan.MaxValue
                };
        }

        #endregion

        #region Private Methods

        private NMIRSoapClient getInstanceAsync()
        {
            return new NMIRSoapClient(_basicHttpBinding, _endpointAddress);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ثبت بازاریاب در سامانه‌
        /// </summary>
        public async Task<PublicReturnType> RegisterAsync(MarketerRegisterModel request)
        {
            try
            {
                var client = getInstanceAsync();
                var response = await client.RegisterAsync(Username, Password,
                    request.NationalCode,
                    request.FirstName,
                    request.LastName,
                    request.FatherName,
                    request.Phone1,
                    request.Phone2,
                    request.Email,
                    request.BirthDate,
                    request.IdNo,
                    request.Education,
                    request.Address,
                    request.PostalCode,
                    request.ParentNationalCode);

                if (response.Body.RegisterResult.ToLower() == "error:EmployeeIsActive".ToLower())
                    return new PublicReturnType { Result = false, Message = "بازاریاب در شرکت دیگری فعال است" };

                if (response.Body.RegisterResult.ToLower() == "error:ServerError".ToLower())
                    return new PublicReturnType { Result = false, Message = "خطایی در سمت سرور کمیته رخ داده است" };

                return new PublicReturnType { Result = true, Token = response.Body.RegisterResult };
            }
            catch (Exception)
            {
                return new PublicReturnType { Result = false, Message = "خطایی در سمت سرور کمیته رخ داده است" };
            }
        }

        /// <summary>
        ///اطلاع از وضعیت بازاریاب که آیا در شرکتی فعال است یا خیر
        /// </summary>
        public async Task<MarketerState> GetPersonStatusAsync(string nationalCode)
        {
            var client = getInstanceAsync();
            var response = await client.GetPersonStatusAsync(Username, Password, nationalCode);

            if (response.Body.GetPersonStatusResult.ToLower() == "error:ServerError".ToLower())
                throw new Exception("خطایی در سمت سرور بازاریابی رخ داده است");

            return response.Body.GetPersonStatusResult.ToLower() == "freeperson" ?
                MarketerState.FreePerson :
                MarketerState.ActivePerson;
        }

        /// <summary>
        /// دریافت کد در صورتی که هنگام ثبت کدی برگردانده نشده باشد
        /// </summary>
        public async Task<PublicReturnType> GetPersonCodeAsync(string nationalCode)
        {
            try
            {
                var client = getInstanceAsync();
                var response = await client.GetPersonCodeAsync(Username, Password, nationalCode);

                if (response.Body.GetPersonCodeResult.ToLower() == "error:ServerError".ToLower())
                    return new PublicReturnType { Result = false, Message = "خطایی در سمت سرور کمیته رخ داده است" };

                if (response.Body.GetPersonCodeResult.ToLower() == "error:InvalidPerson".ToLower())
                    return new PublicReturnType { Result = false, Message = "این بازاریاب برای شرکت سفیران ثبت نشده است" };

                return new PublicReturnType { Result = true, Token = response.Body.GetPersonCodeResult };
            }
            catch (Exception)
            {
                return new PublicReturnType { Result = false, Message = "خطایی در سمت سرور کمیته رخ داده است" };
            }
        }

        /// <summary>
        /// شرکتی که بازاریاب در آن ثبت شده است
        /// </summary>
        public async Task<PublicReturnType> GetPersonCompanyAsync(string nationalCode)
        {
            try
            {
                var client = getInstanceAsync();
                var response = await client.GetPersonCompanyAsync(Username, Password, nationalCode);

                if (response.Body.GetPersonCompanyResult.ToLower() == "error:ServerError".ToLower())
                    return new PublicReturnType { Result = false, Message = "خطایی در سمت سرور کمیته رخ داده است" };

                return new PublicReturnType { Result = true, Token = response.Body.GetPersonCompanyResult };
            }
            catch (Exception)
            {
                return new PublicReturnType { Result = false, Message = "خطایی در سمت سرور کمیته رخ داده است" };
            }
        }

        /// <summary>
        /// لغو قرارداد بازاریاب
        /// امکان تعلیق بازاریاب به مدت پانزده روز وجود دارد
        /// </summary>
        public async Task<PublicReturnType> RevokeAsync(string nationalCode)
        {
            try
            {
                var client = getInstanceAsync();
                var response = await client.RevokeAsync(Username, Password, nationalCode, true);

                if (response.Body.RevokeResult.ToLower() == "false".ToLower())
                    return new PublicReturnType { Result = false, Message = "خطایی در سمت سرور کمیته رخ داده است" };

                return new PublicReturnType { Result = true, Token = response.Body.RevokeResult };
            }
            catch (Exception)
            {
                return new PublicReturnType { Result = false, Message = "خطایی در سمت سرور کمیته رخ داده است" };
            }
        }

        /// <summary>
        /// انصراف از لغو قرار داد بازاریاب
        /// </summary>
        public async Task<PublicReturnType> ReActiveAsync(string nationalCode)
        {
            try
            {
                var client1 = getInstanceAsync();
                var response = await client1.ReActiveAsync(Username, Password, nationalCode, true);
                if (response.Body.ReActiveResult.ToLower() != "true")
                    return new PublicReturnType { Result = false, Message = $"سامانه ثبت امکان این کار را ندارد. {response.Body.ReActiveResult}" };

                return new PublicReturnType { Result = true };
            }
            catch (Exception)
            {
                return new PublicReturnType { Result = false, Message = "خطایی در سمت سرور کمیته رخ داده است" };
            }
        }

        #endregion
    }
}