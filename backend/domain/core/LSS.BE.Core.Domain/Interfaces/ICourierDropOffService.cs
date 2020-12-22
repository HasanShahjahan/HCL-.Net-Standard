using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Courier;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Domain.Interfaces
{
    public interface ICourierDropOffService
    {
        LockerStationDetailsResponse LockerStationDetails(string lockerStationId);
        FindBookingDto FindBooking(string trackingNumber, string lockerStationId, string lspId);
        AssignSimilarSizeLockerDto AssignSimilarSizeLocker(AssignSimilarSizeLocker model);
        AvailableSizesDto GetAvailableSizes(string lockerStationId, int bookingId);
        ChangeLockerSizeDto ChangeLockerSize(ChangeLockerSize model);
        BookingStatusDto UpdateBookingStatus(BookingStatus model);
    }
}
