using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Models;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Models;
using Newtonsoft.Json.Linq;

namespace LSS.BE.Core.Domain.Interfaces
{
    /// <summary>
    ///   Represents gateway service as a sequence of use cases.
    ///</summary>
    public interface ILmsGatewayService
    {
        /// <summary>
        /// Sets the Lsp verificationopen member by providing locker station id, key and pin.
        /// </summary>
        /// <returns>
        ///  Gets the Lsp Id, Lsp user Id, reference code and expiration date with sucess result. 
        /// </returns>
        JObject LspVerification(LspUserAccess model);

        /// <summary>
        /// Sets the Lsp verificationopen member by providing locker station id, key and pin.
        /// </summary>
        /// <returns>
        ///  Gets the Lsp Id, Lsp user Id, reference code and expiration date with sucess result. 
        /// </returns>
        JObject VerifyOtp(VerifyOtp model);
        JObject SendOtp(SendOtp model);
        JObject LockerStationDetails(string lockerStationId);
        JObject FindBooking(string trackingNumber, string lockerStationId, string lspId);
        JObject AssignSimilarSizeLocker(AssignSimilarSizeLocker model);
        JObject GetAvailableSizes(string lockerStationId);
        JObject ChangeLockerSize(ChangeLockerSize model);
        JObject UpdateBookingStatus(BookingStatus model);
        JObject GetBookingByConsumerPin(ConsumerPin model);
        JObject ChangeSingleLockerStatus(ChangeLockerStatus model);
        JObject RetrieveLockersBelongsToCourier(string lockerStationId, string lspId, string trackingNumber, string status);
        JObject OpenCompartment(Compartment model);
        JObject CompartmentStatus(Compartment model);
        JObject CaptureImage(Capture model);
    }
}
