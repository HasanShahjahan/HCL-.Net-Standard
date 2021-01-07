using LSS.BE.Core.Domain.Services;
using LSS.BE.Core.Entities.Models;
using LSS.BE.Core.TestCaller.Models;
using LSS.HCM.Core.DataObjects.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using SendOtpResponse = LSS.BE.Core.TestCaller.Models.SendOtpResponse;

namespace LSS.BE.Core.TestCaller
{
    public class GetewayServiceClient
    {
        public static (string, GatewayService) Init()
        {
            string uriString = string.Empty, lockerStationId = string.Empty, version = string.Empty, clientId = string.Empty, clientSecret = string.Empty;
            
            Console.Write("Gateway Service Initialization\n");
            Console.WriteLine("Available environments : \n 1.SIT \n 2.UAT");
            Console.Write("Please select your environment :");
            string environmentType = Console.ReadLine();

            if (environmentType == "SIT")
            {

                uriString = "http://18.138.61.187";
                lockerStationId = "c17fb923-70f9-4d3c-b081-4226096d6905";
                version = "v1";
                clientId = "ef3350f9-ace2-4900-9da0-bba80402535a";
                clientSecret = "FA1s0QmZFxXh44QUkVOcEj19hvhjWTsfl1sslwGO";
            }
            else if (environmentType == "UAT")
            {
                uriString = "https://uat-p.picknetwork.com/";
                lockerStationId = "3b142420-8dc0-4996-a5fd-9777bd06b95c";
                version = "v1";
                clientId = "aaa85d8e-2776-4e76-91ea-d7068ab6a18a";
                clientSecret = "NRW672B1LQR7u9eyF95qercNRDd345qTuMP8Eyev";
            }
            else 
            {
                Console.WriteLine("Invalid environment type.");
            }

            Console.Write("Configuration Path : ");
            string configurationPath = Console.ReadLine();

            var memberInfo = new Common.Base.MemberInfo
            {
                UriString = uriString,
                Version = version,
                ClientId = clientId,
                ClientSecret = clientSecret,
                ConfigurationPath = configurationPath
            };
            var gatewayService = new GatewayService(memberInfo);
            return (lockerStationId, gatewayService);
        }

        public static void CourierDropOff(string lockerStationId, GatewayService gatewayService)
        {
            #region Health Check

            var healthCheckResult = gatewayService.HealthCheck();
            Console.WriteLine("[Health Check][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(healthCheckResult, Formatting.Indented));

            #endregion

            #region Lsp Verification

            Console.Write("-----------------------------------------------------------------------------\n");
            Console.Write("[Lsp Verification][Req]\n");

            Console.Write("Key: ");
            string key = Console.ReadLine();

            Console.Write("Pin: ");
            string pin = Console.ReadLine();


            var lspVerification = new LspUserAccess
            {
                LockerStationid = lockerStationId,
                Key = key,
                Pin = pin
            };
            var lspVerificationResult = gatewayService.LspVerification(lspVerification);

            Console.WriteLine("[Lsp Verification][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(lspVerificationResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            var lspVerificationResponse = JsonConvert.DeserializeObject<LspVerificationResponse>(lspVerificationResult.ToString());

            #endregion

            #region Verify Otp

            Console.WriteLine("[Verify Otp][Req]");

            Console.Write("Code: ");
            string code = Console.ReadLine();

            Console.Write("Phone Number: ");
            string phoneNumber = Console.ReadLine();


            var verifyOtpModel = new VerifyOtp
            {
                LockerStationId = lockerStationId,
                LspId = lspVerificationResponse.LspId,
                Code = code,
                PhoneNumber = phoneNumber,
                RefCode = lspVerificationResponse.RefCode
            };

            var verifyOtpResult = gatewayService.VerifyOtp(verifyOtpModel);

            Console.WriteLine("[Verify Otp][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(verifyOtpResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Receive Locker Station Details
            Console.WriteLine("[Receive Locker Station Details][Req]");
            var lockerStationDetailsResult = gatewayService.LockerStationDetails(lockerStationId);

            Console.WriteLine("[Receive Locker Station Details][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(lockerStationDetailsResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            var lockerStationDetailsResponse = JsonConvert.DeserializeObject<LspVerificationResponse>(lspVerificationResult.ToString());

            #endregion

            #region Find Booking By Tracking Number

            Console.WriteLine("[Find Booking By Tracking Number][Req]");

            Console.Write("Tracking Number: ");
            string trackingNumber = Console.ReadLine();

            if (lspVerificationResponse.LspId == null) lspVerificationResponse.LspId = string.Empty;
            var findBookingResult = gatewayService.FindBooking(trackingNumber, lockerStationId, lspVerificationResponse.LspId);

            Console.WriteLine("[Find Booking By Tracking Number][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(findBookingResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Get Available Sizes
            Console.WriteLine("[Get Available Sizes][Req]");
            Console.Write("Locker Station Id: c17fb923-70f9-4d3c-b081-4226096d6905\n");

            var getAvailableSizesResult = gatewayService.GetAvailableSizes(lockerStationId);
            Console.WriteLine("[Get Available Sizes][Res]");

            Console.WriteLine(JsonConvert.SerializeObject(getAvailableSizesResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");
            #endregion

            #region Change Locker Size
            Console.WriteLine("[Change Locker Size][Req]");

            Console.Write("Booking Id: ");
            string changeLockerSizeBookingId = Console.ReadLine();

            Console.Write("Size: ");
            string size = Console.ReadLine();

            var changeLockerSize = new ChangeLockerSize()
            {
                LockerStationId = lockerStationId,
                BookingId = changeLockerSizeBookingId,
                Size = size
            };

            var changeLockerSizeResult = gatewayService.ChangeLockerSize(changeLockerSize);
            Console.WriteLine("[Change Locker Size][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(changeLockerSizeResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");



            #endregion

            #region Open Compartment

            Console.WriteLine("[Open Compartment][Req]");
            string transactionId = Guid.NewGuid().ToString();

            Console.Write("Locker Id: ");
            string lockerId = Console.ReadLine();

            Console.Write("Compartment Id: ");
            string compartmentIds = Console.ReadLine();
            string[] compartmentId = compartmentIds.Split(',');

            var openCompartment = new HCM.Core.DataObjects.Models.Compartment(transactionId, lockerId, compartmentId, false, string.Empty, string.Empty);
            var openCompartmentResult = gatewayService.OpenCompartment(openCompartment);

            Console.WriteLine("[Open Compartment][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(openCompartmentResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Compartment Status

            Console.WriteLine("[Compartment Status][Req]");

            Console.Write("Locker Id: ");
            lockerId = Console.ReadLine();

            Console.Write("Compartment Id: ");
            compartmentIds = Console.ReadLine();
            compartmentId = compartmentIds.Split(',');

            var compartmentStatus = new HCM.Core.DataObjects.Models.Compartment(transactionId, lockerId, compartmentId, false, string.Empty, string.Empty);
            var compartmentStatusResult = gatewayService.CompartmentStatus(compartmentStatus);

            Console.WriteLine("[Compartment Status][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(compartmentStatusResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");
            #endregion

            #region Capture Image

            Console.WriteLine("[Capture Image][Req]");

            Console.Write("Locker Id: ");
            lockerId = Console.ReadLine();

            var captureImage = new Capture(transactionId, lockerId, false, string.Empty, string.Empty);
            var captureImageResult = gatewayService.CaptureImage(captureImage);

            Console.WriteLine("[Capture Image][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(captureImageResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");


            #endregion

            #region Update Booking Status

            Console.WriteLine("[Update Booking Status][Req]");

            Console.Write("Booking Id: ");
            string UpdateBookingStatusBookingId = Console.ReadLine();

            Console.Write("Status: ");
            string status = Console.ReadLine();

            Console.Write("MobileNumber: ");
            string mobileNumber = Console.ReadLine();

            Console.Write("Reason: ");
            string updateBookingReason = Console.ReadLine();

            var bookingStatusUpdate = new BookingStatus()
            {
                LockerStationId = lockerStationId,
                BookingId = Convert.ToInt32(UpdateBookingStatusBookingId),
                LspId = lspVerificationResponse.LspId,
                LspUserId = lspVerificationResponse.LspUserId,
                MobileNumber = mobileNumber,
                Status = status,
                Reason = updateBookingReason

            };

            var bookingStatusUpdateResult = gatewayService.UpdateBookingStatus(bookingStatusUpdate);
            Console.WriteLine("[Update Booking Status][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(bookingStatusUpdateResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");


            #endregion
        }

        public static void ConsumerCollect(string lockerStationId, GatewayService gatewayService) 
        {
            #region Health Check

            var healthCheckResult = gatewayService.HealthCheck();
            Console.WriteLine("[Health Check][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(healthCheckResult, Formatting.Indented));

            #endregion

            #region PIN Verification

            Console.Write("-----------------------------------------------------------------------------\n");
            Console.Write("[Consumer Collect PIN Verification][Req]\n");

            Console.Write("Pin: ");
            string consumerCollectPin = Console.ReadLine();

            Console.Write("Action: ");
            string action = Console.ReadLine();


            var consumerPin = new ConsumerPin
            {
                LockerStationId = lockerStationId,
                Pin = consumerCollectPin,
                Action = action
            };
            var consumerPinResult = gatewayService.GetBookingByConsumerPin(consumerPin);

            Console.WriteLine("[Consumer Collect PIN Verification][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(consumerPinResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Send OTP
            Console.WriteLine("[Send Otp][Req]\n");

            Console.Write("Mobile Number: ");
            string consumerCollectMobileNumber = Console.ReadLine();

            Console.Write("Booking Id: ");
            string consumerBookingId = Console.ReadLine();

            var sendOtp = new SendOtp()
            {
                LockerStationId = lockerStationId,
                PhoneNumber = consumerCollectMobileNumber,
                BookingId = consumerBookingId
            };
            var sendOtpResult = gatewayService.SendOtp(sendOtp);
            Console.WriteLine("[Send Otp][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(sendOtpResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Verify Consumer Otp

            Console.WriteLine("[Consumer Verify Otp][Req]");

            Console.Write("Code: ");
            string consumerCode = Console.ReadLine();

            Console.Write("Phone Number: ");
            string ConsumerPhoneNumber = Console.ReadLine();

            Console.Write("Booking Id: ");
            string consumerVerifyPinBookingId = Console.ReadLine();

            Console.Write("Ref Code: ");
            string consumerRefCode = Console.ReadLine();


            var consumerVerifyOtpModel = new VerifyOtp
            {
                LockerStationId = lockerStationId,
                Code = consumerCode,
                PhoneNumber = ConsumerPhoneNumber,
                RefCode = consumerRefCode,
                BookingId = consumerVerifyPinBookingId
            };

            var consumerVerifyOtpResult = gatewayService.VerifyOtp(consumerVerifyOtpModel);

            Console.WriteLine("[Consumer Verify Otp][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(consumerVerifyOtpResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Open Compartment

            Console.WriteLine("[Open Compartment][Req]");
            string ConsumerTransactionId = Guid.NewGuid().ToString();

            Console.Write("Locker Id: ");
            string ConsumerLockerId = Console.ReadLine();

            Console.Write("Compartment Id: ");
            string consumerCompartmentIds = Console.ReadLine();
            string[] consumerCompartmentId = consumerCompartmentIds.Split(',');

            var consumerOpenCompartment = new HCM.Core.DataObjects.Models.Compartment(ConsumerTransactionId, ConsumerLockerId, consumerCompartmentId, false, string.Empty, string.Empty);
            var consumerOpenCompartmentResult = gatewayService.OpenCompartment(consumerOpenCompartment);

            Console.WriteLine("[Open Compartment][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(consumerOpenCompartmentResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Update Booking Status

            Console.WriteLine("[Update Booking Status][Req]");

            Console.Write("Booking Id: ");
            string consumerUpdateBookingStatusBookingId = Console.ReadLine();

            Console.Write("Status: ");
            string consumerStatus = Console.ReadLine();

            Console.Write("MobileNumber: ");
            string consumerMobileNumber = Console.ReadLine();

            Console.Write("Reason: ");
            string consumerUpdateBookingReason = Console.ReadLine();

            var consumerBookingStatusUpdate = new BookingStatus()
            {
                LockerStationId = lockerStationId,
                BookingId = Convert.ToInt32(consumerUpdateBookingStatusBookingId),
                MobileNumber = consumerMobileNumber,
                Status = consumerStatus,
                Reason = consumerUpdateBookingReason

            };

            var consumerBookingStatusUpdateResult = gatewayService.UpdateBookingStatus(consumerBookingStatusUpdate);
            Console.WriteLine("[Update Booking Status][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(consumerBookingStatusUpdateResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");


            #endregion
        }

        public static void ConsumerReturn(string lockerStationId, GatewayService gatewayService)
        {
            #region Health Check

            var healthCheckResult = gatewayService.HealthCheck();
            Console.WriteLine("[Health Check][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(healthCheckResult, Formatting.Indented));

            #endregion

            #region PIN Verification

            Console.Write("-----------------------------------------------------------------------------\n");
            Console.Write("[Consumer Return PIN Verification][Req]\n");

            Console.Write("Pin: ");
            string consumerCollectPin = Console.ReadLine();

            Console.Write("Action: ");
            string action = Console.ReadLine();


            var consumerPin = new ConsumerPin
            {
                LockerStationId = lockerStationId,
                Pin = consumerCollectPin,
                Action = action
            };
            var consumerPinResult = gatewayService.GetBookingByConsumerPin(consumerPin);

            Console.WriteLine("[Consumer Return PIN Verification][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(consumerPinResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Find Booking By Tracking Number

            Console.WriteLine("[Find Booking By Tracking Number][Req]");

            Console.Write("Tracking Number: ");
            string trackingNumber = Console.ReadLine();

            Console.Write("Booking Id: ");
            string bookingId = Console.ReadLine();

            Console.Write("Action: ");
            string bookingAction = Console.ReadLine();


            var findBookingResult = gatewayService.FindBooking(trackingNumber, lockerStationId, bookingId, bookingAction);

            Console.WriteLine("[Find Booking By Tracking Number][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(findBookingResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Send OTP
            Console.WriteLine("[Send Otp][Req]\n");

            Console.Write("MobileNumber: ");
            string consumerCollectMobileNumber = Console.ReadLine();

            Console.Write("BookingId: ");
            string consumerBookingId = Console.ReadLine();

            var sendOtp = new SendOtp()
            {
                LockerStationId = lockerStationId,
                PhoneNumber = consumerCollectMobileNumber,
                BookingId = consumerBookingId
            };
            var sendOtpResult = gatewayService.SendOtp(sendOtp);

            Console.WriteLine("[Send Otp][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(sendOtpResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            var sendOtpResponse = JsonConvert.DeserializeObject<SendOtpResponse>(sendOtpResult.ToString());

            #endregion

            #region Verify Otp

            Console.WriteLine("[Verify Otp][Req]");

            Console.Write("Code: ");
            string code = Console.ReadLine();

            Console.Write("Booking Id: ");
            string consumerReturnBookingId = Console.ReadLine();

            var verifyOtpModel = new VerifyOtp
            {
                LockerStationId = lockerStationId,
                Code = code,
                RefCode = sendOtpResponse.RefCode,
                BookingId = consumerReturnBookingId
            };

            var verifyOtpResult = gatewayService.VerifyOtp(verifyOtpModel);

            Console.WriteLine("[Verify Otp][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(verifyOtpResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Open Compartment

            Console.WriteLine("[Open Compartment][Req]");
            string transactionId = Guid.NewGuid().ToString();

            Console.Write("Locker Id: ");
            string lockerId = Console.ReadLine();

            Console.Write("Compartment Id: ");
            string compartmentIds = Console.ReadLine();
            string[] compartmentId = compartmentIds.Split(',');

            var openCompartment = new HCM.Core.DataObjects.Models.Compartment(transactionId, lockerId, compartmentId, false, string.Empty, string.Empty);
            var openCompartmentResult = gatewayService.OpenCompartment(openCompartment);

            Console.WriteLine("[Open Compartment][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(openCompartmentResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Compartment Status

            Console.WriteLine("[Compartment Status][Req]");

            Console.Write("Locker Id: ");
            lockerId = Console.ReadLine();

            Console.Write("Compartment Id: ");
            compartmentIds = Console.ReadLine();
            compartmentId = compartmentIds.Split(',');

            var compartmentStatus = new HCM.Core.DataObjects.Models.Compartment(transactionId, lockerId, compartmentId, false, string.Empty, string.Empty);
            var compartmentStatusResult = gatewayService.CompartmentStatus(compartmentStatus);

            Console.WriteLine("[Compartment Status][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(compartmentStatusResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");
            #endregion

            #region Capture Image

            Console.WriteLine("[Capture Image][Req]");

            Console.Write("Locker Id: ");
            lockerId = Console.ReadLine();

            var captureImage = new Capture(transactionId, lockerId, false, string.Empty, string.Empty);
            var captureImageResult = gatewayService.CaptureImage(captureImage);

            Console.WriteLine("[Capture Image][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(captureImageResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");


            #endregion

            #region Update Booking Status

            Console.WriteLine("[Update Booking Status][Req]");

            Console.Write("Booking Id: ");
            string UpdateBookingStatusBookingId = Console.ReadLine();

            Console.Write("Status: ");
            string status = Console.ReadLine();

            Console.Write("MobileNumber: ");
            string mobileNumber = Console.ReadLine();

            Console.Write("Reason: ");
            string updateBookingReason = Console.ReadLine();

            var bookingStatusUpdate = new BookingStatus()
            {
                LockerStationId = lockerStationId,
                BookingId = Convert.ToInt32(UpdateBookingStatusBookingId),
                LspId = string.Empty,
                LspUserId = string.Empty,
                MobileNumber = mobileNumber,
                Status = status,
                Reason = updateBookingReason

            };

            var bookingStatusUpdateResult = gatewayService.UpdateBookingStatus(bookingStatusUpdate);
            Console.WriteLine("[Update Booking Status][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(bookingStatusUpdateResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");


            #endregion

        }

        public static void CourierRetrieve(string lockerStationId, GatewayService gatewayService)
        {
            #region Health Check

            var healthCheckResult = gatewayService.HealthCheck();
            Console.WriteLine("[Health Check][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(healthCheckResult, Formatting.Indented));

            #endregion

            #region Lsp Verification

            Console.Write("-----------------------------------------------------------------------------\n");
            Console.Write("[Lsp Verification][Req]\n");

            Console.Write("Key: ");
            string key = Console.ReadLine();

            Console.Write("Pin: ");
            string pin = Console.ReadLine();


            var lspVerification = new LspUserAccess
            {
                LockerStationid = lockerStationId,
                Key = key,
                Pin = pin
            };
            var lspVerificationResult = gatewayService.LspVerification(lspVerification);

            Console.WriteLine("[Lsp Verification][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(lspVerificationResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            var lspVerificationResponse = JsonConvert.DeserializeObject<LspVerificationResponse>(lspVerificationResult.ToString());

            #endregion

            #region Resend OTP
            //Console.Write("-----------------------------------------------------------------------------\n");
            //Console.Write("[Resend Otp][Req]\n");

            //Console.Write("Key: ");
            //string resendOtpKey = Console.ReadLine();

            //Console.Write("Pin: ");
            //string resendOtppin = Console.ReadLine();


            //var resendOtp = new LspUserAccess
            //{
            //    LockerStationid = lockerStationId,
            //    Key = resendOtpKey,
            //    Pin = resendOtppin
            //};
            //var resendOtpResult = gatewayService.LspVerification(lspVerification);

            //Console.WriteLine("[Resend Otp][Res]");
            //Console.WriteLine(JsonConvert.SerializeObject(resendOtpResult, Formatting.Indented));
            //Console.WriteLine("-----------------------------------------------------------------------------");

            //var resendOtpResponse = JsonConvert.DeserializeObject<LspVerificationResponse>(lspVerificationResult.ToString());
            #endregion

            #region Verify Otp

            Console.WriteLine("[Verify Otp][Req]");

            Console.Write("Code: ");
            string code = Console.ReadLine();

            var verifyOtpModel = new VerifyOtp
            {
                LockerStationId = lockerStationId,
                LspId = lspVerificationResponse.LspId,
                Code = code,
                RefCode = lspVerificationResponse.RefCode
            };

            var verifyOtpResult = gatewayService.VerifyOtp(verifyOtpModel);

            Console.WriteLine("[Verify Otp][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(verifyOtpResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Courier Booking All

            Console.WriteLine("[Courier Booking All][Req]");

            Console.Write("TrackingNumber: ");
            string trackingNumber = Console.ReadLine();

            Console.Write("status: ");
            string status = Console.ReadLine();

            JObject retrieveLockersBelongsToCourierResult;

            if (status == "individual") retrieveLockersBelongsToCourierResult = gatewayService.CourierBookingAll(trackingNumber, lockerStationId, lspVerificationResponse.LspId, lspVerificationResponse.LspUserId, string.Empty, status);
            else retrieveLockersBelongsToCourierResult = gatewayService.CourierBookingAll(lockerStationId, lspVerificationResponse.LspId, lspVerificationResponse.LspUserId, string.Empty, status);

            Console.WriteLine("[Courier Booking All][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(retrieveLockersBelongsToCourierResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Open Compartment

            Console.WriteLine("[Open Compartment][Req]");
            string transactionId = Guid.NewGuid().ToString();

            Console.Write("Locker Id: ");
            string lockerId = Console.ReadLine();

            Console.Write("Compartment Id: ");
            string compartmentIds = Console.ReadLine();
            string[] compartmentId = compartmentIds.Split(',');

            var openCompartment = new HCM.Core.DataObjects.Models.Compartment(transactionId, lockerId, compartmentId, false, string.Empty, string.Empty);
            var openCompartmentResult = gatewayService.OpenCompartment(openCompartment);

            Console.WriteLine("[Open Compartment][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(openCompartmentResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Compartment Status

            Console.WriteLine("[Compartment Status][Req]");

            Console.Write("Locker Id: ");
            lockerId = Console.ReadLine();

            Console.Write("Compartment Id: ");
            compartmentIds = Console.ReadLine();
            compartmentId = compartmentIds.Split(',');

            var compartmentStatus = new HCM.Core.DataObjects.Models.Compartment(transactionId, lockerId, compartmentId, false, string.Empty, string.Empty);
            var compartmentStatusResult = gatewayService.CompartmentStatus(compartmentStatus);

            Console.WriteLine("[Compartment Status][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(compartmentStatusResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");
            #endregion

            #region Capture Image

            Console.WriteLine("[Capture Image][Req]");

            Console.Write("Locker Id: ");
            lockerId = Console.ReadLine();

            var captureImage = new Capture(transactionId, lockerId, false, string.Empty, string.Empty);
            var captureImageResult = gatewayService.CaptureImage(captureImage);

            Console.WriteLine("[Capture Image][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(captureImageResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");


            #endregion

            #region Update Booking Status

            Console.WriteLine("[Update Booking Status][Req]");

            Console.Write("Booking Id: ");
            string UpdateBookingStatusBookingId = Console.ReadLine();

            Console.Write("Status: ");
            string updateBookingStatus = Console.ReadLine();

            Console.Write("MobileNumber: ");
            string mobileNumber = Console.ReadLine();

            Console.Write("Reason: ");
            string updateBookingReason = Console.ReadLine();

            var bookingStatusUpdate = new BookingStatus()
            {
                LockerStationId = lockerStationId,
                BookingId = Convert.ToInt32(UpdateBookingStatusBookingId),
                LspId = lspVerificationResponse.LspId,
                LspUserId = lspVerificationResponse.LspUserId,
                MobileNumber = mobileNumber,
                Status = updateBookingStatus,
                Reason = updateBookingReason

            };

            var bookingStatusUpdateResult = gatewayService.UpdateBookingStatus(bookingStatusUpdate);
            Console.WriteLine("[Update Booking Status][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(bookingStatusUpdateResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");


            #endregion

        }

        public static void Courier3rdParty(string lockerStationId, GatewayService gatewayService)
        {
            #region Health Check

            var healthCheckResult = gatewayService.HealthCheck();
            Console.WriteLine("[Health Check][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(healthCheckResult, Formatting.Indented));

            #endregion

            #region Lsp Verification

            Console.Write("-----------------------------------------------------------------------------\n");
            Console.Write("[Lsp Verification][Req]\n");

            Console.Write("Key: ");
            string key = Console.ReadLine();

            Console.Write("Pin: ");
            string pin = Console.ReadLine();


            var lspVerification = new LspUserAccess
            {
                LockerStationid = lockerStationId,
                Key = key,
                Pin = pin
            };
            var lspVerificationResult = gatewayService.LspVerification(lspVerification);

            Console.WriteLine("[Lsp Verification][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(lspVerificationResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            var lspVerificationResponse = JsonConvert.DeserializeObject<LspVerificationResponse>(lspVerificationResult.ToString());

            #endregion

            #region Verify Otp

            Console.WriteLine("[Verify Otp][Req]");

            Console.Write("Code: ");
            string code = Console.ReadLine();

            var verifyOtpModel = new VerifyOtp
            {
                LockerStationId = lockerStationId,
                LspId = lspVerificationResponse.LspId,
                Code = code,
                RefCode = lspVerificationResponse.RefCode
            };

            var verifyOtpResult = gatewayService.VerifyOtp(verifyOtpModel);

            Console.WriteLine("[Verify Otp][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(verifyOtpResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region 3rd Party Verify

            Console.WriteLine("[3rd Verify][Req]");
            var thirdPartyVerifyResult = gatewayService.Verify3rdParty(lockerStationId, lspVerificationResponse.LspId, lspVerificationResponse.LspUserId);

            Console.WriteLine("[3rd Verify][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(thirdPartyVerifyResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Courier List
            Console.WriteLine("[Courier List][Req]");
            var courierListResult = gatewayService.CourierList();

            Console.WriteLine("[Courier List][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(courierListResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");
            #endregion

            #region Courier 3rd Party
            Console.WriteLine("[Courier 3rd Party][Req]");

            Console.Write("Lsp Id To Collect: ");
            string lspIdToCollect = Console.ReadLine();

            var courier3rdPartyResult = gatewayService.CourierBooking3rdParty(lockerStationId, lspVerificationResponse.LspId, lspVerificationResponse.LspUserId, lspIdToCollect);

            Console.WriteLine("[Courier 3rd Party][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(courier3rdPartyResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");
            #endregion

            #region Courier All
            Console.WriteLine("[Courier All][Req]");

            Console.Write("Status : ");
            string status = Console.ReadLine();

            var courierAllResult = gatewayService.CourierBookingAll(lockerStationId, lspVerificationResponse.LspId, lspVerificationResponse.LspUserId, lspIdToCollect, status);

            Console.WriteLine("[Courier All][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(courierAllResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");
            #endregion

            #region Open Compartment

            Console.WriteLine("[Open Compartment][Req]");
            string transactionId = Guid.NewGuid().ToString();

            Console.Write("Locker Id: ");
            string lockerId = Console.ReadLine();

            Console.Write("Compartment Id: ");
            string compartmentIds = Console.ReadLine();
            string[] compartmentId = compartmentIds.Split(',');

            var openCompartment = new HCM.Core.DataObjects.Models.Compartment(transactionId, lockerId, compartmentId, false, string.Empty, string.Empty);
            var openCompartmentResult = gatewayService.OpenCompartment(openCompartment);

            Console.WriteLine("[Open Compartment][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(openCompartmentResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");

            #endregion

            #region Compartment Status

            Console.WriteLine("[Compartment Status][Req]");

            Console.Write("Locker Id: ");
            lockerId = Console.ReadLine();

            Console.Write("Compartment Id: ");
            compartmentIds = Console.ReadLine();
            compartmentId = compartmentIds.Split(',');

            var compartmentStatus = new HCM.Core.DataObjects.Models.Compartment(transactionId, lockerId, compartmentId, false, string.Empty, string.Empty);
            var compartmentStatusResult = gatewayService.CompartmentStatus(compartmentStatus);

            Console.WriteLine("[Compartment Status][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(compartmentStatusResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");
            #endregion

            #region Capture Image

            Console.WriteLine("[Capture Image][Req]");

            Console.Write("Locker Id: ");
            lockerId = Console.ReadLine();

            var captureImage = new Capture(transactionId, lockerId, false, string.Empty, string.Empty);
            var captureImageResult = gatewayService.CaptureImage(captureImage);

            Console.WriteLine("[Capture Image][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(captureImageResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");


            #endregion

            #region Update Booking Status

            Console.WriteLine("[Update Booking Status][Req]");

            Console.Write("Booking Id: ");
            string updateBookingStatusBookingId = Console.ReadLine();

            Console.Write("Status: ");
            string updateBookingStatus = Console.ReadLine();

            Console.Write("MobileNumber: ");
            string mobileNumber = Console.ReadLine();

            Console.Write("Reason: ");
            string updateBookingReason = Console.ReadLine();

            var bookingStatusUpdate = new BookingStatus()
            {
                LockerStationId = lockerStationId,
                BookingId = Convert.ToInt32(updateBookingStatusBookingId),
                LspId = lspVerificationResponse.LspId,
                LspUserId = lspVerificationResponse.LspUserId,
                MobileNumber = mobileNumber,
                Status = updateBookingStatus,
                Reason = updateBookingReason

            };

            var bookingStatusUpdateResult = gatewayService.UpdateBookingStatus(bookingStatusUpdate);
            Console.WriteLine("[Update Booking Status][Res]");
            Console.WriteLine(JsonConvert.SerializeObject(bookingStatusUpdateResult, Formatting.Indented));
            Console.WriteLine("-----------------------------------------------------------------------------");


            #endregion
        }
    }
}
