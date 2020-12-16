using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    [Serializable]
    public class CommandProtocol
    {
        public string Code { get; set; }
        public string Length { get; set; }
        public int ResLen { get; set; }
    }
}
