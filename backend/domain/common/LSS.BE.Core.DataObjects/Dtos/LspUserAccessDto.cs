using LSS.BE.Core.DataObjects.BaseDtos;

namespace LSS.BE.Core.DataObjects.Dtos
{
    public class LspUserAccessDto
    {
        public bool IsRequestSuccess { get; set; }
        public string LspId { get; set; }
        public string LspUserId { get; set; }
        public string RefCode { get; set; }
        public string ExpiredAt { get; set; }
        public AuthenticatedErrorDto AuthenticationError { get; set; }
        public ValidationErrorDto ValidationError { get; set; }
    }
}
