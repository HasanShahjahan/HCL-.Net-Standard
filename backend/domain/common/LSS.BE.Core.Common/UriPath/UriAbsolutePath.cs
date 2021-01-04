using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Common.UriPath
{
    public static class UriAbsolutePath
    {
        public const string HealthCheck = "me/health-check";
        public const string GetToken = "token";
        public const string VerifyOtp = "otp/verify";
        public const string Verify3rdParty = "courier/booking/verify-3rd-party";
        public const string SendOtp = "otp/send";
        public const string ChangeLockerSize = "booking/locker/change";
        public const string CheckAccess = "courier/check-access";
        public const string AssignSimilarSizeLocker = "booking/locker/assign-similar";
        public const string FindBooking = "booking/tracking-number";
        public const string AvailableSizes = "lockers/available-sizes"; 
        public const string UpdateBookingStatus = "booking/status";
        public const string LockerStationDetails = "lockers";
        public const string CheckPin = "consumer/check-pin";
        public const string ChangeSingleLockerStatus = "lockers/status";
        public const string CourierBookingAll = "courier/booking/all";
        public const string CourierBooking3rdParty = "courier/booking/3rd-party";
        public const string CourierList = "courier/list";
    }
}
