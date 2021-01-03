using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    //[Serializable]
    public class SerialInterface
    {
        public string Name { get; set; }
        public string Port { get; set; }
        public int Baudrate { get; set; }
        public int DataBits { get; set; }
    }
}
