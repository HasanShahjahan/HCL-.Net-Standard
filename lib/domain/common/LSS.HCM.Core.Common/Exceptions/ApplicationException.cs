using System.Globalization;

namespace LSS.HCM.Core.Common.Exceptions
{
    public class ApplicationException 
    {
        public ApplicationException() 
        {
            Code = string.Empty;
            Message = string.Empty;
        }
        public ApplicationException(string code, string message)
        {
            Code = code;
            Message = message;
        }
        public string Code { get; set; } 

        public string Message{ get; set; }
        
    }
}