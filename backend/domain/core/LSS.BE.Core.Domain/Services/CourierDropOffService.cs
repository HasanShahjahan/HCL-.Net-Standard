using LSS.BE.Core.Entities.Courier;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Domain.Services
{
    public sealed class CourierDropOffService
    {
        public static LockerStationDetailsResponse LockerStationDetails(string lockerStationId)
        {
            return new LockerStationDetailsResponse();
        }

        public static LockerResponse FindBooking(string trackingNumber, string lockerStationId, string lspId) 
        {
            return new LockerResponse();
        }

        public static AssignSimilarSizeLockerResponse AssignSimilarSizeLocker(string lockerStationId, int bookingId, string reason) 
        {
            return new AssignSimilarSizeLockerResponse();
        }

        public static AvailableSizesResponse GetAvailableSizes(string lockerStationId, int bookingId)
        {
            return new AvailableSizesResponse();
        }

        public static ChangeLockerSizeResponse ChangeLockerSize(ChangeLockerSize changeLockerSize)
        {
            return new ChangeLockerSizeResponse();
        }

        public static BookingStatusResponse UpdateBookingStatus(BookingStatus bookingStatus) 
        {
            return new BookingStatusResponse();
        }
    }
}
