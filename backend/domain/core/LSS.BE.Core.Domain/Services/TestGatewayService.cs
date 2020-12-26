using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.UriPath;
using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.DataObjects.Mappers;
using LSS.BE.Core.Domain.Helpers;
using LSS.BE.Core.Domain.Initialization;
using LSS.BE.Core.Domain.Interfaces;
using LSS.BE.Core.Entities.Models;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Domain.Interfaces;
using LSS.HCM.Core.Domain.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace LSS.BE.Core.Domain.Services
{
    public class TestGatewayService 
    {
        private readonly TokenResponse _tokenResponse;
        private readonly MemberInfo _memberInfo;
        private readonly ILockerManager _lockerManager;
        private readonly IHttpHandlerHelper _httpHandler;

        public TestGatewayService(MemberInfo memberInfo)
        {
            _memberInfo = memberInfo;
            _httpHandler = new HttpHandlerHelper(_memberInfo.UriString);
            _tokenResponse = ServiceInvoke.InitAsync(_memberInfo, _httpHandler);
            _lockerManager = new LockerManager(_memberInfo.ConfigurationPath);
        }

        public LspUserAccessDto LspVerification(LspUserAccess model)
        {
            var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
            Log.Information("[Lsp Verification][Req]" + "[" + request +"]");
            
            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.CheckAccess,_tokenResponse.AccessToken, _tokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<LspUserAccessResponse>(response);
            Log.Information("[Lsp Verification][Res]" + "[" + response + "]");

            return LspUserAccessMapper.ToObject(result);
        }

        public SendOtpDto SendOtp(SendOtp model)
        {
            var request = SerializerHelper<SendOtp>.SerializeObject(model);
            Log.Information("[Send Otp][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.SendOtp, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Send Otp][Res]" + "[" + response + "]");

            var result = JsonConvert.DeserializeObject<SendOtpResponse>(response);

            return SendOtpMapper.ToObject(result);
        }

        public void VerifyOtp(VerifyOtp model)
        {
            var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
            Log.Information("[Verify Otp][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.VerifyOtp,_tokenResponse.AccessToken, _tokenResponse.DateTime);


            var result = JsonConvert.DeserializeObject(response);

            response.Contains("is_request_success");
            JObject jObj = JObject.Parse(response);
            Console.WriteLine(jObj);
            Console.WriteLine(jObj["error_details"]);
            if( jObj["error_details"].Type == JTokenType.Array)
                Console.WriteLine("Array!!");

            //jObject["user"][user]["structures"][0]
            //Console.WriteLine(result["error_details"][error_details]);
            //var jObjtest = result.ToJson();
            //Console.WriteLine(jObjtest]);

            Log.Information("[Verify Otp][Res]" + "[" + response + "]");

            //return VerifyOtpMapper.ToObject(result);
        }
        
        public LockerStationDetailsDto LockerStationDetails(string lockerStationId)
        {
            Log.Information("[Locker Station Details][Req]" + "[Locker Station Id : " + lockerStationId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = _httpHandler.GetRequestResolver(queryString, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.LockerStationDetails, _tokenResponse.AccessToken, _tokenResponse.DateTime);
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
            var response = _httpHandler.GetRequestResolver(queryString, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.FindBooking, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<FindBookingResponse>(response);
            Log.Information("[Find Booking][Res]" + "[" + response + "]");

            return FindBookingMapper.ToObject(result);
        }

        public LockerResponse AssignSimilarSizeLocker(AssignSimilarSizeLocker model)
        {
            var request = SerializerHelper<AssignSimilarSizeLocker>.SerializeObject(model);
            Log.Information("[Assign Similar Size Locker][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.AssignSimilarSizeLocker, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<LockerResponse>(response);
            Log.Information("[Res]" + "[" + response + "]");
            return result;
        }

        public AvailableSizesDto GetAvailableSizes(string lockerStationId)
        {
            Log.Information("[Get Available Sizes][Req]" + "[Locker Station Id : " + lockerStationId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = _httpHandler.GetRequestResolver(queryString, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.AvailableSizes, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<AvailableSizesResponse>(response);

            Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");
            return AvailableSizesMapper.ToObject(result);
        }

        public LockerResponse ChangeLockerSize(ChangeLockerSize model)
        {
            var request = SerializerHelper<ChangeLockerSize>.SerializeObject(model);
            Log.Information("[Change Locker Size][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.ChangeLockerSize, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<LockerResponse>(response);
            Log.Information("[Change Locker Size][Res]" + "[" + response + "]");

            return result;
        }

        public BookingStatusDto UpdateBookingStatus(BookingStatus model)
        {
            var request = SerializerHelper<BookingStatus>.SerializeObject(model);
            Log.Information("[Update Booking Status][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Put, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.UpdateBookingStatus, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<BookingStatusResponse>(response);
            Log.Information("[Update Booking Status][Res]" + "[" + response + "]");

            return BookingStatusMapper.ToObject(result);
        }

        public LockerResponse GetBookingByConsumerPin(ConsumerPin model)
        {
            var request = SerializerHelper<ConsumerPin>.SerializeObject(model);
            Log.Information("[Get Booking By Consumer Pin][Req]" + "[" + request + "]");

            var response = _httpHandler.PostRequestResolver(request, HttpMethod.Post, _memberInfo.UriString, _memberInfo.Version, _memberInfo.ClientId, _memberInfo.ClientSecret, UriAbsolutePath.CheckPin, _tokenResponse.AccessToken, _tokenResponse.DateTime);
            Log.Information("[Get Booking By Consumer Pin][Res]" + "[" + response + "]");

            var result = JsonConvert.DeserializeObject<LockerResponse>(response);
            return result;
        }

        public LockerDto OpenCompartment(Compartment model) 
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token);
            var result = _lockerManager.OpenCompartment(compartment);
            return result;
        }

        public CompartmentStatusDto CompartmentStatus(Compartment model) 
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token);
            var result = _lockerManager.CompartmentStatus(compartment);
            return result;
        }

        public CaptureDto CaptureImage(Capture model)
        {
            var capture = new Capture(model.TransactionId, model.LockerId, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token);
            var result = _lockerManager.CaptureImage(capture);
            return result;
        }
    }
}
