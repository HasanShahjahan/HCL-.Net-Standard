using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Common.UriPath
{
    public static class UriAbsolutePath
    {
        public const string GetToken = "token";
        public const string VerifyOtp = "otp/verify";
        public const string ChangeLockerSize = "booking/locker/change";
        public const string CheckAccess = "courier/check-access";
        public const string AssignSimilarSizeLocker = "booking/locker/assign-similar";
        public const string FindBooking = "booking/tracking-number";
        public const string AvailableSizes = "lockers/available-sizes"; 
        public const string UpdateBookingStatus = "booking/status";
        public const string LockerStationDetails = "lockers";
    }
}
