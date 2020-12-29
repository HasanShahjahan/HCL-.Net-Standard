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
    /// <summary>
    ///   Represents gateway serive as a sequence of communication with other parties.
    ///</summary>
    public class TestGatewayService 
    {
        /// <summary>
        ///   Get token response initialization value.
        ///</summary>
        private readonly TokenResponse TokenResponse;

        /// <summary>
        ///   Set gateway initialization member value.
        ///</summary>
        private readonly MemberInfo MemberInfo;

        /// <summary>
        ///   Set locker manger initialization member value.
        ///</summary>
        private readonly LockerManager LockerManager;

        /// <summary>
        ///   Set http hander initialization member value.
        ///</summary>
        private readonly IHttpHandlerHelper HttpHandler;

        /// <summary>
        ///   Get scanner initialization value.
        ///</summary>
        public readonly bool ScannerInit;

        /// <summary>
        ///   Initialization information for gateway service.
        ///</summary>
        public TestGatewayService(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;
            HttpHandler = new HttpHandlerHelper(MemberInfo.UriString);
            TokenResponse = ServiceInvoke.InitAsync(MemberInfo, HttpHandler);
            LockerManager = new LockerManager(MemberInfo.ConfigurationPath);
            ScannerInit = ScannerServiceHelper.Start(LockerManager);
        }

        /// <summary>
        /// Sets the Lsp verification member by providing locker station id, key and pin.
        /// </summary>
        /// <returns>
        ///  Gets the Lsp Id, Lsp user Id, reference code and expiration date with sucess result. 
        /// </returns>
        public LspUserAccessDto LspVerification(LspUserAccess model)
        {
            var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
            Log.Information("[Lsp Verification][Req]" + "[" + request +"]");
            
            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CheckAccess,TokenResponse.AccessToken, TokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<LspUserAccessResponse>(response);
            Log.Information("[Lsp Verification][Res]" + "[" + response + "]");

            return LspUserAccessMapper.ToObject(result);
        }

        /// <summary>
        /// Sets the send otp member by providing locker station id, phone number, lsp id, booking id.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag and ref code.
        /// </returns>
        public SendOtpDto SendOtp(SendOtp model)
        {
            var request = SerializerHelper<SendOtp>.SerializeObject(model);
            Log.Information("[Send Otp][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.SendOtp, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Send Otp][Res]" + "[" + response + "]");

            var result = JsonConvert.DeserializeObject<SendOtpResponse>(response);

            return SendOtpMapper.ToObject(result);
        }

        /// <summary>
        /// Sets the verify otp member by providing locker station id, code, ref code, lsp id and phone number.
        /// </summary>
        /// <returns>
        ///  Gets the sucess result with bool type true. 
        /// </returns>
        public void VerifyOtp(VerifyOtp model)
        {
            var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
            Log.Information("[Verify Otp][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.VerifyOtp,TokenResponse.AccessToken, TokenResponse.DateTime);


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

        /// <summary>
        /// Sets the locker station details by locker station id.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, hardware details, opening hour details and languages details.
        /// </returns>
        public LockerStationDetailsDto LockerStationDetails(string lockerStationId)
        {
            Log.Information("[Locker Station Details][Req]" + "[Locker Station Id : " + lockerStationId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.LockerStationDetails, TokenResponse.AccessToken, TokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<LockerStationDetailsResponse>(response);
            Log.Information("[Locker Station Details][Res]" + "[" + response + "]");

            return LockerStationDetailsMapper.ToObject(result);
        }

        /// <summary>
        /// Sets the find booking details by locker station id, tracking number, lsp id.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, hardware door number, locker preview.
        /// </returns>
        public FindBookingDto FindBooking(string trackingNumber, string lockerStationId, string lspId)
        {
            Log.Information("[Find Booking][Req]" + "[Tracking Number : " + trackingNumber + "]" + "[Locker Station Id : " + lockerStationId + "]" + "[lsp Id : " + lspId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId },
                { "tracking_number", trackingNumber },
                { "lsp_id", lspId}
            };
            var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.FindBooking, TokenResponse.AccessToken, TokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<FindBookingResponse>(response);
            Log.Information("[Find Booking][Res]" + "[" + response + "]");

            return FindBookingMapper.ToObject(result);
        }

        /// <summary>
        /// Sets the assign similar size locker by locker station id, booking id and reason.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, hardware door number, assigned locker and locker preview.
        /// </returns>
        public LockerResponse AssignSimilarSizeLocker(AssignSimilarSizeLocker model)
        {
            var request = SerializerHelper<AssignSimilarSizeLocker>.SerializeObject(model);
            Log.Information("[Assign Similar Size Locker][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.AssignSimilarSizeLocker, TokenResponse.AccessToken, TokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<LockerResponse>(response);
            Log.Information("[Res]" + "[" + response + "]");
            return result;
        }

        /// <summary>
        /// Sets the get available size by locker station id.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag and available sizes with categories.
        /// </returns>
        public AvailableSizesDto GetAvailableSizes(string lockerStationId)
        {
            Log.Information("[Get Available Sizes][Req]" + "[Locker Station Id : " + lockerStationId + "]");
            var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
            var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.AvailableSizes, TokenResponse.AccessToken, TokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<AvailableSizesResponse>(response);

            Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");
            return AvailableSizesMapper.ToObject(result);
        }

        /// <summary>
        /// Sets the change locker size by locker station id, booking id and size.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, booking id, assigned lockers, hardware door number and locker preview.
        /// </returns>
        public LockerResponse ChangeLockerSize(ChangeLockerSize model)
        {
            var request = SerializerHelper<ChangeLockerSize>.SerializeObject(model);
            Log.Information("[Change Locker Size][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.ChangeLockerSize, TokenResponse.AccessToken, TokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<LockerResponse>(response);
            Log.Information("[Change Locker Size][Res]" + "[" + response + "]");

            return result;
        }

        /// <summary>
        /// Sets the update booking status by locker station id, booking id, status, mobile number and reason.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, booking id and status.
        /// </returns>
        public BookingStatusDto UpdateBookingStatus(BookingStatus model)
        {
            var request = SerializerHelper<BookingStatus>.SerializeObject(model);
            Log.Information("[Update Booking Status][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Put, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.UpdateBookingStatus, TokenResponse.AccessToken, TokenResponse.DateTime);
            var result = JsonConvert.DeserializeObject<BookingStatusResponse>(response);
            Log.Information("[Update Booking Status][Res]" + "[" + response + "]");

            return BookingStatusMapper.ToObject(result);
        }

        /// <summary>
        /// Sets the consumer pin verification by locker station id, booking id, status, mobile number and reason.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, booking id and status.
        /// </returns>
        public LockerResponse GetBookingByConsumerPin(ConsumerPin model)
        {
            var request = SerializerHelper<ConsumerPin>.SerializeObject(model);
            Log.Information("[Get Booking By Consumer Pin][Req]" + "[" + request + "]");

            var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CheckPin, TokenResponse.AccessToken, TokenResponse.DateTime);
            Log.Information("[Get Booking By Consumer Pin][Res]" + "[" + response + "]");

            var result = JsonConvert.DeserializeObject<LockerResponse>(response);
            return result;
        }

        /// <summary>
        /// Gets the open compartment parameters with Json web token credentials and MongoDB database credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment open status with object detection, LED status by requested compartment id's.
        /// </returns>
        public LockerDto OpenCompartment(Compartment model) 
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token);
            var result = LockerManager.OpenCompartment(compartment);
            return result;
        }

        /// <summary>
        /// Gets the compartment status with Json web token credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment status with object detection and LED status based requested compartment id's.
        /// </returns>
        public CompartmentStatusDto CompartmentStatus(Compartment model) 
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token);
            var result = LockerManager.CompartmentStatus(compartment);
            return result;
        }

        /// <summary>
        /// Gets the capture image parameters with Json web token credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  Byte array of image with transaction Id and image extension.
        /// </returns>
        public CaptureDto CaptureImage(Capture model)
        {
            var capture = new Capture(model.TransactionId, model.LockerId, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token);
            var result = LockerManager.CaptureImage(capture);
            return result;
        }
    }
}
