using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class CompartmentDetailDto
    {
        public string Type { get; set; }
        public int Size { get; set; }
        public int Number { get; set; }
        public string DisplayName { get; set; }
        public string ModuleDoor { get; set; }
    }
}
