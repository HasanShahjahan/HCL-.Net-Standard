using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Mappers
{
    public static class LockerStationDetailsMapper
    {
        public static LockerStationDetailsDto ToObject(this LockerStationDetailsResponse model)
        {
            return new LockerStationDetailsDto()
            {
                IsRequestSuccess = model.IsRequestSuccess,
                OtpCollect = model.OtpCollect,
                OtpReturn = model.OtpReturn,
                IsMrt = model.IsMrt,
                LockerStationId = model.LockerStationId,
                OpeningHours = new OpeningHoursDto(model.OpeningHours.Open, model.OpeningHours.Close),
                Languages = new LanguagesDto(model.Languages.English, model.Languages.Melaya, model.Languages.中文, model.Languages.தமிழ்), 
                Hardware = new LSS.BE.Core.DataObjects.Dtos.HardwareDto(model.Hardware.MqttHost, model.Hardware.Address, model.Hardware.LockerId, model.Hardware.Token, model.Hardware.TokenExp),
                AuthenticationError = new BaseDtos.AuthenticatedErrorDto(model.Success, model.Status, model.Message),
                ValidationError = new BaseDtos.ValidationErrorDto(model.ErrorMessage, model.ErrorDetails.LockerStationId, model.ErrorDetails.LspId, model.ErrorDetails.BookingId)
            };
        }
    }
}
