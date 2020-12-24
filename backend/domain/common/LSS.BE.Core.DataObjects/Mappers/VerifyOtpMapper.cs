using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Models;

namespace LSS.BE.Core.DataObjects.Mappers
{
    public static class VerifyOtpMapper
    {
        public static VerifyOtpDto ToObject(this VerifyOtpResponse model)
        {
            return new VerifyOtpDto()
            {
                IsRequestSuccess = model.IsRequestSuccess,
                AuthenticationError = new BaseDtos.AuthenticatedErrorDto(model.Success, model.Status, model.Message),
                ValidationError = new BaseDtos.ValidationErrorDto(model.ErrorMessage, model.ErrorDetails.LockerStationId, model.ErrorDetails.LspId, model.ErrorDetails.BookingId)
            };
        }
    }
}
