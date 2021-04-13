using System;
using System.ServiceModel;
using System.Threading.Tasks;
using AsnafBranches;
using Infrastructure.ApiClients.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.ApiClients
{
    public class AsnafBranchesApiClient : IAsnafBranchesApiClient
    {

        #region Fields

        private readonly WebPortTypeClient _client;
        private readonly IConfiguration _configuration;

        #endregion

        #region Constructors

        public AsnafBranchesApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new WebPortTypeClient(
                new BasicHttpBinding(BasicHttpSecurityMode.Transport)
                {
                    OpenTimeout = TimeSpan.MaxValue,
                    CloseTimeout = TimeSpan.MaxValue,
                    ReceiveTimeout = TimeSpan.MaxValue,
                    SendTimeout = TimeSpan.MaxValue,
                    MaxReceivedMessageSize = int.MaxValue
                },
                new EndpointAddress(
                    "https://easnaf.mimt.gov.ir/fa/index.php?module=cdk&func=loadmodule&system=cdk&sismodule=user/call_function.php&ctp_id=62&func_name=wsrvTypeServerFunction&type_name=mim_branch_issuance_change"));
        }

        #endregion

        #region Public Methods

        public async Task<GetProvinceCityResponse> GetProvinceCityListAsync()
        {
            await _client.OpenAsync();
            var response = await _client.getProvinceCityListAsync(_configuration["Asnaf:Password"]); //TODO: SetAppSetting
            await _client.CloseAsync();
            return JsonConvert.DeserializeObject<GetProvinceCityResponse>(response);
        }

        public async Task<IssuanceResponse> BranchIssuanceAsync(BranchIssuanceRequest request)
        {
            await _client.OpenAsync();

            var response = await _client.branchIssuanceAsync(request.Password, request.CompanyId, request.Province,
                request.City, request.ManagerName, request.Mobile, request.NationalId, request.PostalCode,
                request.Address, request.NationalCardCopyFileName, request.IdentificationCopyFileName,
                request.ObligationFormFileName, request.EstablishFormFileName, request.RentalContractFileName,
                request.OfficialNewspaperFileName, request.NationalCardCopy, request.IdentificationCopy,
                request.ObligationForm, request.EstablishForm, request.RentalContract, request.OfficialNewspaper);

            await _client.CloseAsync();
            return JsonConvert.DeserializeObject<IssuanceResponse>(response);
        }

        public async Task<ChangeManagerResponse> ChangeManagerAsync(ChangeManagerRequest request)
        {
            await _client.OpenAsync();
            var response = await _client.changeManagerAsync(request.Password, request.CompanyId, request.BranchCode,
                request.ManagerName, request.ManagerNationalCode, request.ManagerMobile,
                request.NationalCardCopyFileName, request.IdentificationCopyFileName, request.EstablishFormFileName,
                request.NationalCardCopy, request.IdentificationCopy, request.EstablishForm);
            await _client.CloseAsync();
            return JsonConvert.DeserializeObject<ChangeManagerResponse>(response);
        }

        public async Task<ChangeAddressResponse> ChangeAddressAsync(ChangeAddressRequest request)
        {
            await _client.OpenAsync();
            var response = await _client.changeAddressAsync(request.Password, request.CompanyId, request.BranchCode,
                request.PostalCode, request.Address, request.ChangeAddressFormFileName, request.RentalContractFileName,
                request.OfficialNewspaperFileName, request.ChangeAddressForm, request.RentalContract,
                request.OfficialNewspaper);
            await _client.CloseAsync();
            return JsonConvert.DeserializeObject<ChangeAddressResponse>(response);
        }

        public async Task<CancelResponse> CancelBranchAsync(CancelBranchRequest request)
        {
            await _client.OpenAsync();
            var response = await _client.cancelBranchAsync(request.Password, request.CompanyId, request.BranchCode,
                request.CancelFormFileName, request.CancelForm);
            await _client.CloseAsync();
            return JsonConvert.DeserializeObject<CancelResponse>(response);
        }

        #endregion
    }
}