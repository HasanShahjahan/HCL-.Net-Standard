using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.BaseDtos
{
    public class AuthenticatedErrorDto
    {
        public AuthenticatedErrorDto()
        {
            Success = true;
            Status = string.Empty;
            Message = string.Empty;
        }
        public AuthenticatedErrorDto(bool success, string status, string message)
        {
            Success = success;
            Status = status;
            Message = message;
        }

        public bool Success { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
    }
}
