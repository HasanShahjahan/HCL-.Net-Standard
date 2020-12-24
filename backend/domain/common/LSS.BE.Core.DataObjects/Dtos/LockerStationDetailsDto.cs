using LSS.BE.Core.DataObjects.BaseDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class LockerStationDetailsDto
    {
        public bool IsRequestSuccess { get; set; }
        public string LockerStationId { get; set; }
        public bool OtpCollect { get; set; }
        public bool OtpReturn{ get; set; }
        public bool IsMrt { get; set; }
        public LanguagesDto Languages { get; set; }
        public OpeningHoursDto OpeningHours { get; set; }
        public HardwareDto Hardware { get; set; }
        public AuthenticatedErrorDto AuthenticationError { get; set; }
        public ValidationErrorDto ValidationError { get; set; }
    }
}
