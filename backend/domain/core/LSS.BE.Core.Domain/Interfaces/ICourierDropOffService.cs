using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Courier;
using Newtonsoft.Json.Linq;

namespace LSS.BE.Core.Domain.Interfaces
{
    public interface ICourierDropOffService
    {
        JObject LockerStationDetails(string lockerStationId);
        JObject FindBooking(string trackingNumber, string lockerStationId, string lspId);
        AssignSimilarSizeLockerDto AssignSimilarSizeLocker(AssignSimilarSizeLocker model);
        AvailableSizesDto GetAvailableSizes(string lockerStationId, int bookingId);
        ChangeLockerSizeDto ChangeLockerSize(ChangeLockerSize model);
        BookingStatusDto UpdateBookingStatus(BookingStatus model);
    }
}
