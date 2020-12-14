namespace LSS.HCM.Core.Common.Exceptions
{
    public class ApplicationError
    {
        public int StatusCode { get; set; }
        public ApplicationException Error { get; set; }
    }
}
