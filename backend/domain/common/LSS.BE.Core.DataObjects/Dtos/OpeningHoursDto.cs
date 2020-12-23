using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class OpeningHoursDto
    {
        public OpeningHoursDto(string open, string close)
        {
            Open = open;
            Close = close;
        }
        public string Open { get; set; }
        public string Close { get; set; }
    }
}
