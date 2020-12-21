using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Entities.Courier;
using Newtonsoft.Json;
using System;

namespace LSS.BE.Core.Domain.Services
{
    public class CourierDropOffService
    {
        private readonly TokenResponse _tokenResponse;
        private readonly string _uriString, _version, _clientId, _clientSecret;
        private readonly DateTime _dateTime;

        public CourierDropOffService(string uriString, string version, string clientId, string clientSecret)
        {
            _tokenResponse = HttpHandlerHelper.GetToken(uriString, version, clientId, clientSecret);
            _uriString = uriString;
            _version = version;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _dateTime = DateTime.Now;
        }

        public LspUserAccessResponse LspVerification(LspUserAccess model)
        {
            var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
            var response = HttpHandlerHelper.PostRequestResolver(request, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.CheckAccess, _tokenResponse.AccessToken, _dateTime);

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<LspUserAccessResponse>(response, settings);
            return result;
        }

        public LockerStationDetailsResponse LockerStationDetails(string lockerStationId)
        {
            return new LockerStationDetailsResponse();
        }

        public VerifyOtpResponse VerifyOtp(VerifyOtp model)
        {
            var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
            var response = HttpHandlerHelper.PostRequestResolver(request, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.VerifyOtp, _tokenResponse.AccessToken, _dateTime);

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<VerifyOtpResponse>(response, settings);
            return result;
        }

        public LockerResponse FindBooking(string trackingNumber, string lockerStationId, string lspId) 
        {
            return new LockerResponse();
        }

        public AssignSimilarSizeLockerResponse AssignSimilarSizeLocker(string lockerStationId, int bookingId, string reason) 
        {
            return new AssignSimilarSizeLockerResponse();
        }

        public AvailableSizesResponse GetAvailableSizes(string lockerStationId, int bookingId)
        {
            return new AvailableSizesResponse();
        }

        public ChangeLockerSizeResponse ChangeLockerSize(ChangeLockerSize model)
        {
            return new ChangeLockerSizeResponse();
        }

        public BookingStatusResponse UpdateBookingStatus(BookingStatus bookingStatus) 
        {
            return new BookingStatusResponse();
        }
    }
}
