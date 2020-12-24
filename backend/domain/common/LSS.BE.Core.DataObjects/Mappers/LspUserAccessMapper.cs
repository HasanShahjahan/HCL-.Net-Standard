using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Models;

namespace LSS.BE.Core.DataObjects.Mappers
{
    public static class LspUserAccessMapper
    {
        public static LspUserAccessDto ToObject(this LspUserAccessResponse model)
        {
            return new LspUserAccessDto()
            {
                IsRequestSuccess = model.IsRequestSuccess,
                LspId = model.LspId,
                LspUserId = model.LspUserId,
                RefCode = model.RefCode,
                ExpiredAt = model.ExpiredAt,
                AuthenticationError = new BaseDtos.AuthenticatedErrorDto(model.Success, model.Status, model.Message),
                ValidationError = new BaseDtos.ValidationErrorDto(model.ErrorMessage, model.ErrorDetails.LockerStationId, model.ErrorDetails.LspId, model.ErrorDetails.BookingId)
            };
        }
        
    }
}
