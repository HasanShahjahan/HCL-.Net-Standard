using LSS.BE.Core.DataObjects.BaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class SendOtpDto
    {
        public bool IsRequestSuccess { get; set; }
        public string RefCode { get; set; }
        public AuthenticatedErrorDto AuthenticationError { get; set; }
        public ValidationErrorDto ValidationError { get; set; }
    }
}
