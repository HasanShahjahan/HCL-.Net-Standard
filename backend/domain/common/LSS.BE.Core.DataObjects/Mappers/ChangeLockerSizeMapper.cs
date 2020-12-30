using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Mappers
{
    public static class ChangeLockerSizeMapper
    {
        public static ChangeLockerSizeDto ToObject(this ChangeLockerSizeResponse model)
        {
            return new ChangeLockerSizeDto()
            {
                IsRequestSuccess = model.IsRequestSuccess,
                AuthenticationError = new BaseDtos.AuthenticatedErrorDto(model.Success, model.Status, model.Message),
                ValidationError = new BaseDtos.ValidationErrorDto(model.ErrorMessage, model.ErrorDetails.LockerStationId, model.ErrorDetails.LspId, model.ErrorDetails.BookingId)
            };
        }
    }
}
