using AsnafConference;
using Infrastructure.ApiClients.Contracts;
using Newtonsoft.Json;
using System;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Infrastructure.ApiClients
{
    public class AsnafConferenceApiClient : IAsnafConferenceApiClient
    {
        #region Fields

        private readonly WebPortTypeClient _client;

        #endregion

        #region Constructors

        public AsnafConferenceApiClient()
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
                    "https://easnaf.mimt.gov.ir/fa/index.php?module=cdk&func=loadmodule&system=cdk&sismodule=user/call_function.php&ctp_id=62&func_name=wsrvTypeServerFunction&type_name=mim_conference_licensing"));
        }

        #endregion

        #region Public Methods

        public async Task<ConferenceIssuanceResponse> ConferenceIssuanceAsync(ConferenceIssuanceRequest request)
        {
            await _client.OpenAsync();
            var response = await _client.conferenceIssuanceAsync(request.Password, request.CompanyId, request.Title,
                request.Date, request.Location, request.Text, request.Image, request.ImageFileName, request.VideoExist,
                request.CommitmentFileName, request.Commitment);
            await _client.CloseAsync();
            return JsonConvert.DeserializeObject<ConferenceIssuanceResponse>(response);
        }

        #endregion
    }
}