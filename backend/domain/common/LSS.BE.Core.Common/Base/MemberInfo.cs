using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Common.Base
{
    public class MemberInfo
    {
        public string UriString { get; set; }
        public string Version { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ConfigurationPath { get; set; }
    }
}
