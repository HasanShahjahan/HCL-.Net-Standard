using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.DataObjects.Mappers;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Domain.Initialization;
using LSS.BE.Core.Entities.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace LSS.BE.Core.Domain.Services
{
    public class LmsGatewayService
    {
        private readonly TokenResponse _tokenResponse;
        private readonly string _uriString, _version, _clientId, _clientSecret;
        private readonly DateTime _dateTime;

        public LmsGatewayService(string uriString, string version, string clientId, string clientSecret, string loggerConfigurationPath)
        {
            _uriString = uriString;
            _version = version;
            _clientId = clientId;
            _clientSecret = clientSecret;
            _dateTime = DateTime.Now;
            _tokenResponse = ServiceInvoke.InitAsync(uriString, version, clientId, clientSecret, loggerConfigurationPath);
        }

        public JObject LspVerification(LspUserAccess model)
        {
            var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
            Log.Information("[Lsp Verification][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.CheckAccess, _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Lsp Verification][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject VerifyOtp(VerifyOtp model)
        {
            var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
            Log.Information("[Verify Otp][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.VerifyOtp, _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Verify Otp][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject SendOtp(SendOtp model)
        {
            var request = SerializerHelper<SendOtp>.SerializeObject(model);
            Log.Information("[Send Otp][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.SendOtp, _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Send Otp][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject LockerStationDetails(string lockerStationId)
        {
            Log.Information("[Locker Station Details][Req]" + "[Locker Station Id : " + lockerStationId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId, _clientSecret, UriAbsolutePath.LockerStationDetails, _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }
      
        public JObject FindBooking(string trackingNumber, string lockerStationId, string lspId)
        {
            Log.Information("[Find Booking][Req]" + "[Tracking Number : " + trackingNumber + "]" + "[Locker Station Id : " + lockerStationId + "]" + "[lsp Id : " + lspId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId },
                { "tracking_number", trackingNumber },
                { "lsp_id", lspId}
            };
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId, _clientSecret, UriAbsolutePath.FindBooking, _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Find Booking][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;

        }

        public JObject AssignSimilarSizeLocker(AssignSimilarSizeLocker model)
        {
            
            var request = SerializerHelper<AssignSimilarSizeLocker>.SerializeObject(model);
            Log.Information("Assign Similar Size Locker][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.AssignSimilarSizeLocker, _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject GetAvailableSizes(string lockerStationId, int bookingId)
        {
            Log.Information("[Get Available Sizes][Req]" + "[Locker Station Id : " + lockerStationId + "]" + "[Booking Id : " + bookingId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = HttpHandlerHelper.GetRequestResolver(_uriString, queryString, _version, _clientId, _clientSecret, UriAbsolutePath.AvailableSizes, _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject ChangeLockerSize(ChangeLockerSize model)
        {
            var request = SerializerHelper<ChangeLockerSize>.SerializeObject(model);
            Log.Information("[Change Locker Size][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Post, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.ChangeLockerSize, _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Change Locker Size][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject UpdateBookingStatus(BookingStatus model)
        {
            var request = SerializerHelper<BookingStatus>.SerializeObject(model);
            Log.Information("[Update Booking Status][Req]" + "[" + request + "]");

            var response = HttpHandlerHelper.PostRequestResolver(request, HttpMethod.Put, _uriString, _version, _clientId, _clientSecret, UriAbsolutePath.UpdateBookingStatus, _tokenResponse.AccessToken, _dateTime);
            Log.Information("[Update Booking Status][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }
    }
}
