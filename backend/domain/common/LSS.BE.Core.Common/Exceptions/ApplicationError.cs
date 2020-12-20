using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Common.Exceptions
{
    public class ApplicationError
    {
        public int StatusCode { get; set; }
        public ApplicationException Error { get; set; }
    }
}
