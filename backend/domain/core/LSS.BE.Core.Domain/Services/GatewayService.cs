using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Domain.Initialization;
using LSS.BE.Core.Domain.Interfaces;
using LSS.BE.Core.Entities.Models;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Domain.Interfaces;
using LSS.HCM.Core.Domain.Managers;
using LSS.HCM.Core.Domain.Services;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace LSS.BE.Core.Domain.Services
{
    public class GatewayService : ILmsGatewayService
    {
        private readonly TokenResponse _tokenResponse;
        private readonly MemberInfo _memberInfo;
        private readonly LockerManager _lockerManager;
        private readonly IHttpHandlerHelper _httpHandler;
        private readonly bool _scannerInit;

        public GatewayService(MemberInfo memberInfo)
        {
            _memberInfo = memberInfo;
            _httpHandler = new HttpHandlerHelper(_memberInfo.UriString);
            _tokenResponse = ServiceInvoke.InitAsync(_memberInfo, _httpHandler);
            _lockerManager = new LockerManager(_memberInfo.ConfigurationPath);
            _scannerInit = ScannerServiceHelper.Start(_lockerManager);
        }

        public JObject LspVerification(LspUserAccess model)
        {
            var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
            Log.Information("[Lsp Verification][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.CheckAccess, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Lsp Verification][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject VerifyOtp(VerifyOtp model)
        {
            var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
            Log.Information("[Verify Otp][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.VerifyOtp, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Verify Otp][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject SendOtp(SendOtp model)
        {
            var request = SerializerHelper<SendOtp>.SerializeObject(model);
            Log.Information("[Send Otp][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.SendOtp, _tokenResponse.AccessToken, _tokenResponse.DateTime);
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
            var response = _httpHandler.GetRequestResolver(queryString, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.LockerStationDetails, _tokenResponse.AccessToken, _tokenResponse.DateTime);
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
            var response = _httpHandler.GetRequestResolver(queryString, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.FindBooking, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Find Booking][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;

        }

        public JObject AssignSimilarSizeLocker(AssignSimilarSizeLocker model)
        {
            var request = SerializerHelper<AssignSimilarSizeLocker>.SerializeObject(model);
            Log.Information("Assign Similar Size Locker][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.AssignSimilarSizeLocker, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject GetAvailableSizes(string lockerStationId)
        {
            Log.Information("[Get Available Sizes][Req]" + "[Locker Station Id : " + lockerStationId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = _httpHandler.GetRequestResolver(queryString, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.AvailableSizes, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject ChangeLockerSize(ChangeLockerSize model)
        {
            var request = SerializerHelper<ChangeLockerSize>.SerializeObject(model);
            Log.Information("[Change Locker Size][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.ChangeLockerSize, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Change Locker Size][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject UpdateBookingStatus(BookingStatus model)
        {
            var request = SerializerHelper<BookingStatus>.SerializeObject(model);
            Log.Information("[Update Booking Status][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Put, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.UpdateBookingStatus, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Update Booking Status][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject GetBookingByConsumerPin(ConsumerPin model)
        {
            var request = SerializerHelper<ConsumerPin>.SerializeObject(model);
            Log.Information("[Get Booking By Consumer Pin][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.CheckPin, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Get Booking By Consumer Pin][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject ChangeSingleLockerStatus(ChangeLockerStatus model)
        {
            var request = SerializerHelper<ChangeLockerStatus>.SerializeObject(model);
            Log.Information("[Change Single Locker Status][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Put, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.ChangeSingleLockerStatus, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Change Single Locker Status][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject RetrieveLockersBelongsToCourier(string lockerStationId, string lspId, string trackingNumber, string status) 
        {
            Log.Information("[Retrieve Lockers Belongs To Courier][Req]" + "[Tracking Number : " + trackingNumber + "]" + "[Locker Station Id : " + lockerStationId + "]" + "[lsp Id : " + lspId + "]" + "[Status : " + status + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId },
                { "tracking_number", trackingNumber },
                { "lsp_id", lspId},
                { "status", status}
            };
            var response = _httpHandler.GetRequestResolver(queryString, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.RetrieveLockersBelongsCourier, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Retrieve Lockers Belongs To Courier][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject OpenCompartment(Compartment model)
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds,
                                              model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var response  = _lockerManager.OpenCompartment(compartment);
            var result = (JObject)JToken.FromObject(response);
            return result;
        }

        public JObject CompartmentStatus(Compartment model)
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds,
                                              model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var response = _lockerManager.CompartmentStatus(compartment);
            var result = (JObject)JToken.FromObject(response);
            return result;
        }

        public JObject CaptureImage(Capture model)
        {
            var capture = new Capture(model.TransactionId, model.LockerId, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var response = _lockerManager.CaptureImage(capture);
            var result = (JObject)JToken.FromObject(response);
            return result;
        }
    }
}
