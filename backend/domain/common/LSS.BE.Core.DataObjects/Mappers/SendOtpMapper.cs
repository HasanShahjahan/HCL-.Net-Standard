using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Mappers
{
    public static class SendOtpMapper
    {
        public static SendOtpDto ToObject(this SendOtpResponse model)
        {
            return new SendOtpDto()
            {
                IsRequestSuccess = model.IsRequestSuccess,
                RefCode = model.RefCode,
                AuthenticationError = new BaseDtos.AuthenticatedErrorDto(model.Success, model.Status, model.Message),
                ValidationError = new BaseDtos.ValidationErrorDto(model.ErrorMessage, model.ErrorDetails.LockerStationId, model.ErrorDetails.LspId, model.ErrorDetails.BookingId)
            };
        }
    }
}
