using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.BaseDtos
{
    public class ValidationErrorDto
    {
        public ValidationErrorDto() 
        {
            ErrorMessage = string.Empty;
            ErrorDetails = new ErrorDetailsDto();
        }
        public ValidationErrorDto(string errorMessage, string[] lockerStationId, string[] lspId, string[] bookingId)
        {
            ErrorMessage = errorMessage;
            ErrorDetails = new ErrorDetailsDto(lockerStationId, lspId, bookingId);
        }

        public string ErrorMessage { get; set; }
        public ErrorDetailsDto ErrorDetails { get; set; }
    }
}
