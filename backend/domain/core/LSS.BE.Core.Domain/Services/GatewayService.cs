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
        public readonly TokenResponse TokenResponse;
        public readonly MemberInfo MemberInfo;
        public readonly LockerManager LockerManager;
        public readonly IHttpHandlerHelper HttpHandler;
        public readonly bool ScannerInit;

        public GatewayService(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;
            HttpHandler = new HttpHandlerHelper(MemberInfo.UriString);
            TokenResponse = ServiceInvoke.InitAsync(MemberInfo, HttpHandler);
            LockerManager = new LockerManager(MemberInfo.ConfigurationPath);
            ScannerInit = ScannerServiceHelper.Start(LockerManager);
        }

        public JObject LspVerification(LspUserAccess model)
        {
            var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
            Log.Information("[Lsp Verification][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CheckAccess, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Lsp Verification][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject VerifyOtp(VerifyOtp model)
        {
            var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
            Log.Information("[Verify Otp][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.VerifyOtp, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Verify Otp][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject SendOtp(SendOtp model)
        {
            var request = SerializerHelper<SendOtp>.SerializeObject(model);
            Log.Information("[Send Otp][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.SendOtp, TokenResponse.AccessToken, TokenResponse.DateTime);
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
            var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.LockerStationDetails, TokenResponse.AccessToken, TokenResponse.DateTime);
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
            var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.FindBooking, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Find Booking][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;

        }

        public JObject AssignSimilarSizeLocker(AssignSimilarSizeLocker model)
        {
            var request = SerializerHelper<AssignSimilarSizeLocker>.SerializeObject(model);
            Log.Information("Assign Similar Size Locker][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.AssignSimilarSizeLocker, TokenResponse.AccessToken, TokenResponse.DateTime);
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
            var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.AvailableSizes, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject ChangeLockerSize(ChangeLockerSize model)
        {
            var request = SerializerHelper<ChangeLockerSize>.SerializeObject(model);
            Log.Information("[Change Locker Size][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.ChangeLockerSize, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Change Locker Size][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject UpdateBookingStatus(BookingStatus model)
        {
            var request = SerializerHelper<BookingStatus>.SerializeObject(model);
            Log.Information("[Update Booking Status][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Put, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.UpdateBookingStatus, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Update Booking Status][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject GetBookingByConsumerPin(ConsumerPin model)
        {
            var request = SerializerHelper<ConsumerPin>.SerializeObject(model);
            Log.Information("[Get Booking By Consumer Pin][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CheckPin, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Get Booking By Consumer Pin][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject ChangeSingleLockerStatus(ChangeLockerStatus model)
        {
            var request = SerializerHelper<ChangeLockerStatus>.SerializeObject(model);
            Log.Information("[Change Single Locker Status][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Put, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.ChangeSingleLockerStatus, TokenResponse.AccessToken, TokenResponse.DateTime);
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
            var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.UriString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.RetrieveLockersBelongsCourier, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Retrieve Lockers Belongs To Courier][Res]" + "[" + response + "]");

            var result = JObject.Parse(response);
            return result;
        }

        public JObject OpenCompartment(Compartment model)
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds,
                                              model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var response  = LockerManager.OpenCompartment(compartment);
            var result = (JObject)JToken.FromObject(response);
            return result;
        }

        public JObject CompartmentStatus(Compartment model)
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds,
                                              model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var response = LockerManager.CompartmentStatus(compartment);
            var result = (JObject)JToken.FromObject(response);
            return result;
        }

        public JObject CaptureImage(Capture model)
        {
            var capture = new Capture(model.TransactionId, model.LockerId, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var response = LockerManager.CaptureImage(capture);
            var result = (JObject)JToken.FromObject(response);
            return result;
        }
    }
}
