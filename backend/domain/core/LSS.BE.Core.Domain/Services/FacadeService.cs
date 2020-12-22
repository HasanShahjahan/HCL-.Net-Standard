using LSS.BE.Core.DataObjects.Dtos;
using LSS.BE.Core.Entities.Courier;

namespace LSS.BE.Core.Domain.Services
{
    public abstract class FacadeService
    {
        public abstract LspUserAccessDto LspVerification(LspUserAccess model);
        public abstract VerifyOtpDto VerifyOtp(VerifyOtp model);
    }
}
