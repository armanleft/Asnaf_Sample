using System;
using System.ServiceModel;
using System.Threading.Tasks;
using AsnafProducts;
using Infrastructure.ApiClients.Contracts;
using Newtonsoft.Json;

namespace Infrastructure.ApiClients
{
    public class AsnafProductsApiClient : IAsnafProductsApiClient
    {
        #region Fields

        private readonly WebPortTypeClient _client;

        #endregion

        #region Constructors

        public AsnafProductsApiClient()
        {
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
                    "https://easnaf.mimt.gov.ir/fa/index.php?module=cdk&func=loadmodule&system=cdk&sis" +
                    "module=user/call_function.php&ctp_id=62&func_name=wsrvTypeServerFunction&type_na" +
                    "me=mim_product"));
        }

        #endregion

        #region Public Methods

        public async Task<ProductResponse> ProductAsync(ProductRequest request)
        {
            await _client.OpenAsync();
            var response = await _client.productAsync(
                request.Password,
                request.CompanyId,
                request.Title,
                request.IranCode,
                request.UserName,
                request.UserEmail,
                request.RegistrationDate,
                request.BrandOwnerdocFileName,
                request.BusinessLicenseFileName,
                request.HealthLicensingFileName,
                request.CostEstimationFileName,
                request.CommitmentFileName,
                request.CatalogueFileName,
                request.FactorFileName,
                request.OtherDocumentFileName,
                request.BrandOwnerdoc,
                request.BusinessLicense,
                request.HealthLicensing,
                request.CostEstimation,
                request.Commitment,
                request.Catalogue,
                request.Factor,
                request.OtherDocument);
            await _client.CloseAsync();
            return JsonConvert.DeserializeObject<ProductResponse>(response);
        }

        #endregion
    }
}