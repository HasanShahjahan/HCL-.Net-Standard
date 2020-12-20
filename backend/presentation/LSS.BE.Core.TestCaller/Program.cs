using LSS.BE.Core.Domain.Services;
using LSS.BE.Core.Entities.Courier;
using System;

namespace LSS.BE.Core.TestCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientId = "d5cd3087-749c-4efe-baf7-b38126a69b8a";
            string clientSecret = "sgKEx9BXzV76oSXLt7NEv4HOF4g594IzYW9ffNU3";
            string uriString = "http://18.138.61.187";
            string version = "v1";

            var courierDropOffService = new CourierDropOffService(uriString, version, clientId, clientSecret);

            var model = new VerifyOtp
            {
                LockerStationid = "Hasan",
                LspId = "Hasan",
                PhoneNumber = "Hasan",
                RefCode = "Hasan",
                Code = "Hasan"
            };

            courierDropOffService.VerifyOtp(model);

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
