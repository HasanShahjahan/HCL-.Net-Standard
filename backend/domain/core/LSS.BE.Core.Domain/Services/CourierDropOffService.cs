using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.DataObjects.Mappers;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Domain.Initialization;
using LSS.BE.Core.Domain.Interfaces;
using LSS.BE.Core.Entities.Courier;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace LSS.BE.Core.Domain.Services
{
    public class CourierDropOffService : FacadeService, ICourierDropOffService
    {
        private readonly TokenResponse _tokenResponse;
        private readonly string _uriString, _version, _clientId, _clientSecret;
        private readonly DateTime _dateTime;

        public CourierDropOffService(string uriString, string version, string clientId, string clientSecret, string loggerConfigurationPath)
        {
            _uriString = uriString;
            _version = version;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _dateTime = DateTime.Now;
            _tokenResponse = ServiceInvoke.InitAsync(uriString, version, clientId, clientSecret, loggerConfigurationPath);
        }

        public override  LspUserAccessDto LspVerification(LspUserAccess model)
        {
            var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
            Log.Information("[Lsp Verification][Req]" + "[" + request +"]");
            
            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.CheckAccess, _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<LspUserAccessResponse>(response);
            Log.Information("[Lsp Verification][Res]" + "[" + response + "]");

            return LspUserAccessMapper.ToObject(result);
        }

        public override  VerifyOtpDto VerifyOtp(VerifyOtp model)
        {
            var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
            Log.Information("[Verify Otp][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.VerifyOtp, _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<VerifyOtpResponse>(response);
            Log.Information("[Verify Otp][Res]" + "[" + response + "]");

            return new VerifyOtpDto();
        }

        public LockerStationDetailsResponse LockerStationDetails(string lockerStationId)
        {
            return new LockerStationDetailsResponse();
        }

        public FindBookingDto FindBooking(string trackingNumber, string lockerStationId, string lspId)
        {
            Log.Information("[Find Booking][Req]" + "[Tracking Number : " + trackingNumber + "]" + "[Locker Station Id : " + lockerStationId + "]" + "[lsp Id : " + lspId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId },
                { "tracking_number", trackingNumber },
                { "lsp_id", lspId}
            };
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId, _clientSecret, UriAbsolutePath.FindBooking, _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<FindBookingResponse>(response);
            Log.Information("[Find Booking][Res]" + "[" + response + "]");

            return FindBookingMapper.ToObject(result);
        }

        public AssignSimilarSizeLockerDto AssignSimilarSizeLocker(AssignSimilarSizeLocker model)
        {
            var request = SerializerHelper<AssignSimilarSizeLocker>.SerializeObject(model);
            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.AssignSimilarSizeLocker, _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<AssignSimilarSizeLockerResponse>(response);
            Log.Information("[Res]" + "[" + response + "]");

            return new AssignSimilarSizeLockerDto();
        }

        public AvailableSizesDto GetAvailableSizes(string lockerStationId, int bookingId)
        {
            Log.Information("[Get Available Sizes][Req]" + "[Locker Station Id : " + lockerStationId + "]" + "[Booking Id : " + bookingId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId, _clientSecret, UriAbsolutePath.AvailableSizes, _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<AvailableSizesResponse>(response);

            Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");
            return AvailableSizesMapper.ToObject(result);
        }

        public ChangeLockerSizeDto ChangeLockerSize(ChangeLockerSize model)
        {
            var request = SerializerHelper<ChangeLockerSize>.SerializeObject(model);
            Log.Information("[Change Locker Size][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.ChangeLockerSize, _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<ChangeLockerSizeResponse>(response);
            Log.Information("[Change Locker Size][Res]" + "[" + response + "]");

            return ChangeLockerSizeMapper.ToObject(result);
        }

        public BookingStatusDto UpdateBookingStatus(BookingStatus model)
        {
            var request = SerializerHelper<BookingStatus>.SerializeObject(model);
            Log.Information("[Update Booking Status][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Put, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.UpdateBookingStatus, _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<BookingStatusResponse>(response);
            Log.Information("[Update Booking Status][Res]" + "[" + response + "]");

            return BookingStatusMapper.ToObject(result);
        }
    }
}
