using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.DataObjects.Mappers;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Entities.Courier;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

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

        public LspUserAccessDto LspVerification(LspUserAccess model)
        {
            var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
            var response = HttpHandlerHelper.PostRequestResolver(request, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.CheckAccess, _tokenResponse.AccessToken, _dateTime);

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<LspUserAccessResponse>(response, settings);
            return LspUserAccessMapper.ToObject(result);
        }

        public VerifyOtpDto VerifyOtp(VerifyOtp model)
        {
            var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
            var response = HttpHandlerHelper.PostRequestResolver(request, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.VerifyOtp, _tokenResponse.AccessToken, _dateTime);

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<VerifyOtpResponse>(response, settings);
            return new VerifyOtpDto();
        }

        public LockerStationDetailsResponse LockerStationDetails(string lockerStationId)
        {
            return new LockerStationDetailsResponse();
        }

        public FindBookingDto FindBooking(string trackingNumber, string lockerStationId, string lspId)
        {
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId },
                { "tracking_number", trackingNumber }
            };
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId, _clientSecret, UriAbsolutePath.FindBooking, _tokenResponse.AccessToken, _dateTime);
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<FindBookingResponse>(response, settings);
            return FindBookingMapper.ToObject(result);
        }

        public AssignSimilarSizeLockerDto AssignSimilarSizeLocker(AssignSimilarSizeLocker model)
        {
            var request = SerializerHelper<AssignSimilarSizeLocker>.SerializeObject(model);
            var response = HttpHandlerHelper.PostRequestResolver(request, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.AssignSimilarSizeLocker, _tokenResponse.AccessToken, _dateTime);

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<AssignSimilarSizeLockerResponse>(response, settings);
            return new AssignSimilarSizeLockerDto();
        }

        public AvailableSizesDto GetAvailableSizes(string lockerStationId, int bookingId)
        {
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId, _clientSecret, UriAbsolutePath.AvailableSizes, _tokenResponse.AccessToken, _dateTime);
            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<AvailableSizesResponse>(response, settings);
            return AvailableSizesMapper.ToObject(result);
        }

        public ChangeLockerSizeResponse ChangeLockerSize(ChangeLockerSize model)
        {
            var request = SerializerHelper<ChangeLockerSize>.SerializeObject(model);
            var response = HttpHandlerHelper.PostRequestResolver(request, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.ChangeLockerSize, _tokenResponse.AccessToken, _dateTime);

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<ChangeLockerSizeResponse>(response, settings);
            return result;
        }

        public BookingStatusResponse UpdateBookingStatus(BookingStatus model)
        {
            var request = SerializerHelper<BookingStatus>.SerializeObject(model);
            var response = HttpHandlerHelper.PostRequestResolver(request, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.UpdateBookingStatus, _tokenResponse.AccessToken, _dateTime);

            var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            var result = JsonConvert.DeserializeObject<BookingStatusResponse>(response, settings);
            return result;
        }
    }
}
