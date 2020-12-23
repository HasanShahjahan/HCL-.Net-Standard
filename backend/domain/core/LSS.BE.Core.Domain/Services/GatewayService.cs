using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.DataObjects.Mappers;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Domain.Initialization;
using LSS.BE.Core.Entities.Courier;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Domain.Managers;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace LSS.BE.Core.Domain.Services
{
    public class GatewayService 
    {
        private readonly TokenResponse _tokenResponse;
        private readonly string _uriString, _version, _clientId, _clientSecret, _configurationPath;
        private readonly DateTime _dateTime;
        private readonly LockerManager _lockerManager;

        public GatewayService(string uriString, string version, string clientId, string clientSecret, string configurationPath)
        {
            _uriString = uriString;
            _version = version;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _dateTime = DateTime.Now;
            _configurationPath = configurationPath;
            _tokenResponse = ServiceInvoke.InitAsync(uriString, version, clientId, clientSecret, configurationPath);
            _lockerManager = new LockerManager(_configurationPath);
        }

        public LspUserAccessDto LspVerification(LspUserAccess model)
        {
            var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
            Log.Information("[Lsp Verification][Req]" + "[" + request +"]");
            
            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version,
                                                                 _clientId, _clientSecret, UriAbsolutePath.CheckAccess,
                                                                 _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<LspUserAccessResponse>(response);
            Log.Information("[Lsp Verification][Res]" + "[" + response + "]");

            return LspUserAccessMapper.ToObject(result);
        }

        public VerifyOtpDto VerifyOtp(VerifyOtp model)
        {
            var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
            Log.Information("[Verify Otp][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version,
                                                                 _clientId, _clientSecret, UriAbsolutePath.VerifyOtp,
                                                                 _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<VerifyOtpResponse>(response);
            Log.Information("[Verify Otp][Res]" + "[" + response + "]");

            return VerifyOtpMapper.ToObject(result);
        }
        
        public LockerStationDetailsDto LockerStationDetails(string lockerStationId)
        {
            Log.Information("[Locker Station Details][Req]" + "[Locker Station Id : " + lockerStationId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId,
                                                                _clientSecret, UriAbsolutePath.LockerStationDetails,
                                                                _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<LockerStationDetailsResponse>(response);
            Log.Information("[Locker Station Details][Res]" + "[" + response + "]");

            return LockerStationDetailsMapper.ToObject(result);
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
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId,
                _clientSecret, UriAbsolutePath.FindBooking, _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<FindBookingResponse>(response);
            Log.Information("[Find Booking][Res]" + "[" + response + "]");

            return FindBookingMapper.ToObject(result);
        }

        public AssignSimilarSizeLockerDto AssignSimilarSizeLocker(AssignSimilarSizeLocker model)
        {
            var request = SerializerHelper<AssignSimilarSizeLocker>.SerializeObject(model);
            Log.Information("[Assign Similar Size Locker][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version,
                                                                 _clientId, _clientSecret,
                                                                 UriAbsolutePath.AssignSimilarSizeLocker,
                                                                 _tokenResponse.AccessToken, _dateTime);
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
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId,
                                                                _clientSecret, UriAbsolutePath.AvailableSizes,
                                                                _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<AvailableSizesResponse>(response);

            Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");
            return AvailableSizesMapper.ToObject(result);
        }

        public string GetAvailableSizes(string lockerStationId, int bookingId, string value, string TestValue)
        {
            Log.Information("[Get Available Sizes][Req]" + "[Locker Station Id : " + lockerStationId + "]" + "[Booking Id : " + bookingId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId,
                                                                _clientSecret, UriAbsolutePath.AvailableSizes,
                                                                _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");

            var result = JsonConvert.SerializeObject(Newtonsoft.Json.Linq.JObject.Parse(response), Formatting.Indented);
            return result;
        }

        public ChangeLockerSizeDto ChangeLockerSize(ChangeLockerSize model)
        {
            var request = SerializerHelper<ChangeLockerSize>.SerializeObject(model);
            Log.Information("[Change Locker Size][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version,
                                                                 _clientId, _clientSecret,
                                                                 UriAbsolutePath.ChangeLockerSize,
                                                                 _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<ChangeLockerSizeResponse>(response);
            Log.Information("[Change Locker Size][Res]" + "[" + response + "]");

            return ChangeLockerSizeMapper.ToObject(result);
        }

        public BookingStatusDto UpdateBookingStatus(BookingStatus model)
        {
            var request = SerializerHelper<BookingStatus>.SerializeObject(model);
            Log.Information("[Update Booking Status][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Put, _uriString, _version,
                                                                 _clientId, _clientSecret,
                                                                 UriAbsolutePath.UpdateBookingStatus,
                                                                 _tokenResponse.AccessToken, _dateTime);
            var result = JsonConvert.DeserializeObject<BookingStatusResponse>(response);
            Log.Information("[Update Booking Status][Res]" + "[" + response + "]");

            return BookingStatusMapper.ToObject(result);
        }

        public LockerDto OpenCompartment(Compartment model) 
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds,
                                              model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var result = _lockerManager.OpenCompartment(compartment);
            return result;
        }

        public CompartmentStatusDto CompartmentStatus(Compartment model) 
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds,
                                              model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var result = _lockerManager.CompartmentStatus(compartment);
            return result;
        }

        public CaptureDto CaptureImage(Capture model)
        {
            var capture = new Capture(model.TransactionId, model.LockerId, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var result = _lockerManager.CaptureImage(capture);
            return result;
        }
    }
}
