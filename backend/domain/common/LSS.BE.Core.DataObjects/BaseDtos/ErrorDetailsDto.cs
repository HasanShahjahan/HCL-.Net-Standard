using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.BaseDtos
{
    public class ErrorDetailsDto
    {
        public ErrorDetailsDto() 
        {

        }
        public ErrorDetailsDto(string[] lockerStationId, string[] lspId, string[] bookingId)
        {
            LockerStationId = lockerStationId;
            LspId = lspId;
            BookingId = bookingId;
        }
        public string[] LockerStationId { get; set; }
        public string[] LspId { get; set; }
        public string[] BookingId { get; set; }
    }
}
