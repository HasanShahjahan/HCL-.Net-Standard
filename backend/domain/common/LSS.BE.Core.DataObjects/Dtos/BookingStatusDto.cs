using LSS.BE.Core.DataObjects.BaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class BookingStatusDto
    {
        public bool IsRequestSuccess { get; set; }
        public int BookingId { get; set; }
        public int Status { get; set; }
        public AuthenticatedErrorDto AuthenticationError { get; set; }
        public ValidationErrorDto ValidationError { get; set; }
    }
}
