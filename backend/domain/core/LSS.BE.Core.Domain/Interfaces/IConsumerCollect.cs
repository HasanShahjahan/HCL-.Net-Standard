using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.BE.Core.Domain.Interfaces
{
    public interface IConsumerCollect
    {
        void sendOtp();
        void VerifyOtp();
        void GetBookingConsumerByPin();
        void UpdateBookingStatus();

    }
}
