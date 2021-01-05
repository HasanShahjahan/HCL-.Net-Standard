using LSS.BE.Core.Common.Base;
using LSS.BE.Core.Common.Exceptions;
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
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.InteropServices;

namespace LSS.BE.Core.Domain.Services
{
    /// <summary>
    ///   Represents gateway serive as a sequence of communication with other parties.
    ///</summary>
    public class GatewayService : ILmsGatewayService, IDisposable
    {
        /// <summary>
        ///   To detect redundant calls.
        ///</summary>
        private bool _disposed = false;

        /// <summary>
        ///   Instantiate a SafeHandle instance.
        ///</summary>
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        ///   Get token response initialization value.
        ///</summary>
        public readonly TokenResponse TokenResponse;

        /// <summary>
        ///   Set gateway initialization member value.
        ///</summary>
        public readonly MemberInfo MemberInfo;

        /// <summary>
        ///   Set locker manger initialization member value.
        ///</summary>
        public readonly LockerManager LockerManager;

        /// <summary>
        ///   Set http hander initialization member value.
        ///</summary>
        public readonly HttpHandlerHelper HttpHandler;

        private readonly ServiceInvoke _serviceInvoke;

        /// <summary>
        ///   Get scanner initialization value.
        ///</summary>
        public readonly bool ScannerInit;

        /// <summary>
        ///   Initialization information for gateway service.
        ///</summary>
        public GatewayService(MemberInfo memberInfo)
        {
            MemberInfo = memberInfo;
            HttpHandler = new HttpHandlerHelper(MemberInfo.UriString);
            _serviceInvoke = new ServiceInvoke(MemberInfo, HttpHandler);
            TokenResponse = _serviceInvoke.InitAsync();
            if (TokenResponse.StatusCode == 200) LockerManager = new LockerManager(MemberInfo.ConfigurationPath);
            if (LockerManager != null) ScannerInit = ScannerServiceHelper.Start(LockerManager);
        }

        /// <summary>
        /// Sets the health check by providing token.
        /// </summary>
        /// <returns>
        ///  Gets health check status true. 
        /// </returns>
        public JObject HealthCheck()
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                var response = HttpHandler.PostRequestResolver(string.Empty, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.HealthCheck, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Health Check][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));
        }

        /// <summary>
        /// Sets the Lsp verification member by providing locker station id, key and pin.
        /// </summary>
        /// <returns>
        ///  Gets the Lsp Id, Lsp user Id, reference code and expiration date with sucess result. 
        /// </returns>
        public JObject LspVerification(LspUserAccess model)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                var request = SerializerHelper<LspUserAccess>.SerializeObject(model);
                Log.Information("[Lsp Verification][Req]" + "[" + request + "]");

                var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CheckAccess, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Lsp Verification][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));
        }

        /// <summary>
        /// Sets the verify otp member by providing locker station id, code, ref code, lsp id and phone number.
        /// </summary>
        /// <returns>
        ///  Gets the sucess result with bool type true. 
        /// </returns>
        public JObject VerifyOtp(VerifyOtp model)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                var request = SerializerHelper<VerifyOtp>.SerializeObject(model);
                Log.Information("[Verify Otp][Req]" + "[" + request + "]");

                var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.VerifyOtp, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Verify Otp][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }

            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));
        }

        /// <summary>
        /// Verify 3rd party by lsp id, lsp user id, locker station id
        /// </summary>
        /// <returns>
        ///  Gets the sucess result with bool type true. 
        /// </returns>
        public JObject Verify3rdParty(string lockerStationId, string lspId, string lspUserId)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                Log.Information("[Verify 3rd Party][Req]" + "[Locker Station Id : " + lockerStationId + "]" + "[lsp Id : " + lspId + "]" + "[Lsp User Id : " + lspUserId + "]");
                var queryString = new Dictionary<string, string>()
                {
                    { "locker_station_id", lockerStationId },
                    { "lsp_id", lspId},
                    { "lsp_user_id", lspUserId}
                };
                var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.Verify3rdParty, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Verify 3rd Party][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));
        }

        /// <summary>
        /// Sets the send otp member by providing locker station id, phone number, lsp id, booking id.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag and ref code.
        /// </returns>
        public JObject SendOtp(SendOtp model)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                var request = SerializerHelper<SendOtp>.SerializeObject(model);
                Log.Information("[Send Otp][Req]" + "[" + request + "]");

                var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.SendOtp, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Send Otp][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));
        }

        /// <summary>
        /// Get courier list
        /// </summary>
        /// <returns>
        ///  List of courier.
        /// </returns>
        public JObject CourierList()
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                Log.Information("[Courier List][Req]");
                var queryString = new Dictionary<string, string>();
                var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CourierList, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Courier List][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));
        }

        /// <summary>
        /// Sets the locker station details by locker station id.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, hardware details, opening hour details and languages details.
        /// </returns>
        public JObject LockerStationDetails(string lockerStationId)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                Log.Information("[Locker Station Details][Req]" + "[Locker Station Id : " + lockerStationId + "]");
                var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
                var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.LockerStationDetails, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));


        }

        /// <summary>
        /// Sets the find booking details by locker station id, tracking number, lsp id.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, hardware door number, locker preview.
        /// </returns>
        public JObject FindBooking(string trackingNumber, string lockerStationId, string lspId)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                Log.Information("[Find Booking][Req]" + "[Tracking Number : " + trackingNumber + "]" + "[Locker Station Id : " + lockerStationId + "]" + "[lsp Id : " + lspId + "]");
                var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId },
                { "tracking_number", trackingNumber },
                { "lsp_id", lspId}
            };
                var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.FindBooking, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Find Booking][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));


        }

        /// <summary>
        /// Sets the find booking details by locker station id, tracking number, lsp id.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, hardware door number, locker preview.
        /// </returns>
        public JObject FindBooking(string trackingNumber, string lockerStationId, string bookingId, string action)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                Log.Information("[Find Booking][Req]" + "[Tracking Number : " + trackingNumber + "]" + "[Locker Station Id : " + lockerStationId + "]" + "[action : " + action + "]");
                var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId },
                { "tracking_number", trackingNumber },
                { "booking_id", bookingId},
                { "action", action}
            };
                var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.FindBooking, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Find Booking][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));


        }

        /// <summary>
        /// Sets the assign similar size locker by locker station id, booking id and reason.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, hardware door number, assigned locker and locker preview.
        /// </returns>
        public JObject AssignSimilarSizeLocker(AssignSimilarSizeLocker model)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                var request = SerializerHelper<AssignSimilarSizeLocker>.SerializeObject(model);
                Log.Information("Assign Similar Size Locker][Req]" + "[" + request + "]");

                var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.AssignSimilarSizeLocker, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));

        }

        /// <summary>
        /// Sets the get available size by locker station id.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag and available sizes with categories.
        /// </returns>
        public JObject GetAvailableSizes(string lockerStationId)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                Log.Information("[Get Available Sizes][Req]" + "[Locker Station Id : " + lockerStationId + "]");
                var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId }
            };
                var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.AvailableSizes, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Get Available Sizes][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));

        }

        /// <summary>
        /// Sets the change locker size by locker station id, booking id and size.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, booking id, assigned lockers, hardware door number and locker preview.
        /// </returns>
        public JObject ChangeLockerSize(ChangeLockerSize model)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                var request = SerializerHelper<ChangeLockerSize>.SerializeObject(model);
                Log.Information("[Change Locker Size][Req]" + "[" + request + "]");

                var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.ChangeLockerSize, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Change Locker Size][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));

        }

        /// <summary>
        /// Sets the update booking status by locker station id, booking id, status, mobile number and reason.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, booking id and status.
        /// </returns>
        public JObject UpdateBookingStatus(BookingStatus model)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                var request = SerializerHelper<BookingStatus>.SerializeObject(model);
                Log.Information("[Update Booking Status][Req]" + "[" + request + "]");

                var response = HttpHandler.PostRequestResolver(request, HttpMethod.Put, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.UpdateBookingStatus, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Update Booking Status][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));

        }

        /// <summary>
        /// Sets the consumer pin verification by locker station id, booking id, status, mobile number and reason.
        /// </summary>
        /// <returns>
        ///  Gets the success bool flag, booking id and status.
        /// </returns>
        public JObject GetBookingByConsumerPin(ConsumerPin model)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                var request = SerializerHelper<ConsumerPin>.SerializeObject(model);
                Log.Information("[Get Booking By Consumer Pin][Req]" + "[" + request + "]");

                var response = HttpHandler.PostRequestResolver(request, HttpMethod.Post, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CheckPin, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Get Booking By Consumer Pin][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));

        }

        /// <summary>
        /// Sets the change locker status by locker station id, hwapi id and status.
        /// </summary>
        public JObject ChangeSingleLockerStatus(ChangeLockerStatus model)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                var request = SerializerHelper<ChangeLockerStatus>.SerializeObject(model);
                Log.Information("[Change Single Locker Status][Req]" + "[" + request + "]");

                var response = HttpHandler.PostRequestResolver(request, HttpMethod.Put, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.ChangeSingleLockerStatus, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Change Single Locker Status][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));

        }

        /// <summary>
        /// Sets the single retrieve locker belongs to courier by locker station id, lsp id, tracking number and status.
        /// </summary>
        public JObject CourierBookingAll(string trackingNumber, string lockerStationId, string lspId, string lspUserId, string lspIdToCollect, string status)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                Log.Information("[Courier Booking All][Req]" + "[Tracking Number : " + trackingNumber + "]" + "[Locker Station Id : " + lockerStationId + "]" + "[lsp Id : " + lspId + "]" + "[Lsp User Id : " + lspUserId + "]" + "[Lsp Id To Collect : " + lspIdToCollect + "]" + "[Status : " + status + "]");
                var queryString = new Dictionary<string, string>()
                {
                    { "locker_station_id", lockerStationId },
                    { "tracking_number", trackingNumber },
                    { "lsp_id", lspId},
                    { "lsp_user_id", lspUserId},
                    { "lsp_id_to_collect", lspIdToCollect},
                    { "status", status}
                };
                var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CourierBookingAll, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Courier Booking All][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));

        }

        /// <summary>
        /// Sets the bulk retrieve locker belongs to courier by locker station id, lsp id, tracking number and status.
        /// </summary>
        public JObject CourierBookingAll(string lockerStationId, string lspId, string lspUserId, string lspIdToCollect, string status)
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                Log.Information("[Courier Booking All][Req]" + "[Locker Station Id : " + lockerStationId + "]" + "[lsp Id : " + lspId + "]" + "[ls pUser Id : " + lspUserId + "]" + "[Lsp Id To Collect : " + lspIdToCollect + "]" + "[Status : " + status + "]");
                var queryString = new Dictionary<string, string>()
            {
                { "locker_station_id", lockerStationId },
                { "lsp_id", lspId},
                { "lsp_user_id", lspUserId},
                { "lsp_id_to_collect", lspIdToCollect},
                { "status", status}
            };
                var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CourierBookingAll, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Courier Booking All][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));

        }

        /// <summary>
        /// Sets the bulk retrieve locker belongs to courier 3rd party by locker station id, lsp id, lsp user id, lsp Id To Collect
        /// </summary>
        public JObject CourierBooking3rdParty(string lockerStationId, string lspId, string lspUserId, string lspIdToCollect) 
        {
            JObject result;
            if (TokenResponse.StatusCode == 200)
            {
                Log.Information("[Courier Booking 3rd Party][Req]" + "[Locker Station Id : " + lockerStationId + "]" + "[lsp Id : " + lspId + "]" + "[ls pUser Id : " + lspUserId + "]" + "[Lsp Id To Collect : " + lspIdToCollect + "]");
                var queryString = new Dictionary<string, string>()
                {
                    { "locker_station_id", lockerStationId },
                    { "lsp_id", lspId},
                    { "lsp_user_id", lspUserId},
                    { "lsp_id_to_collect", lspIdToCollect}
                };
                var response = HttpHandler.GetRequestResolver(queryString, MemberInfo.Version, MemberInfo.ClientId, MemberInfo.ClientSecret, UriAbsolutePath.CourierBooking3rdParty, TokenResponse.AccessToken, TokenResponse.DateTime);
                Log.Information("[Courier Booking 3rd Party][Res]" + "[" + response + "]");

                result = JObject.Parse(response);
                return result;
            }
            return JObject.Parse(SerializerHelper<AuthenticationError>.SerializeObject(new AuthenticationError(false, "401", "Unauthenticated")));
        }

        /// <summary>
        /// Gets the open compartment parameters with Json web token credentials and MongoDB database credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment open status with object detection, LED status by requested compartment id's.
        /// </returns>
        public JObject OpenCompartment(Compartment model)
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds,
                                              model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var response = LockerManager.OpenCompartment(compartment);
            var result = (JObject)JToken.FromObject(response);
            return result;
        }

        /// <summary>
        /// Gets the compartment status with Json web token credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment status with object detection and LED status based requested compartment id's.
        /// </returns>
        public JObject CompartmentStatus(Compartment model)
        {
            var compartment = new Compartment(model.TransactionId, model.LockerId, model.CompartmentIds,
                                              model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var response = LockerManager.CompartmentStatus(compartment);
            var result = (JObject)JToken.FromObject(response);
            return result;
        }

        /// <summary>
        /// Gets the capture image parameters with Json web token credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  Byte array of image with transaction Id and image extension.
        /// </returns>
        public JObject CaptureImage(Capture model)
        {
            var capture = new Capture(model.TransactionId, model.LockerId, model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret,
                                              model.JwtCredentials.Token);
            var response = LockerManager.CaptureImage(capture);
            var result = (JObject)JToken.FromObject(response);
            return result;
        }

        /// <summary>
        ///   Public implementation of Dispose pattern callable by consumers.
        ///</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Protected implementation of Dispose pattern.
        ///</summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _safeHandle?.Dispose();
                HttpHandler?.Dispose();
                _serviceInvoke?.Dispose();
                LockerManager?.Dispose();
            }
            _disposed = true;
        }
    }
}
