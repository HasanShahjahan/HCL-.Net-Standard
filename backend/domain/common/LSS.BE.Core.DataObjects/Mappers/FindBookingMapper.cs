using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Courier;

namespace LSS.BE.Core.DataObjects.Mappers
{
    public static class FindBookingMapper
    {
        public static FindBookingDto ToObject(this FindBookingResponse model)
        {
            return new FindBookingDto()
            {
                IsRequestSuccess = model.IsRequestSuccess,
                AuthenticationError = new BaseDtos.AuthenticatedErrorDto(model.Success, model.Status, model.Message),
                ValidationError = new BaseDtos.ValidationErrorDto(model.ErrorMessage, model.ErrorDetails.LockerStationId, model.ErrorDetails.LspId, model.ErrorDetails.BookingId)
            };
        }
    }
}
