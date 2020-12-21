using LSS.BE.Core.Domain.Services;
using LSS.BE.Core.Entities.Courier;
using System;

namespace LSS.BE.Core.TestCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            string clientId = "ef3350f9-ace2-4900-9da0-bba80402535a";
            string clientSecret = "FA1s0QmZFxXh44QUkVOcEj19hvhjWTsfl1sslwGO";
            string uriString = "http://18.138.61.187";
            string version = "v1";

            var courierDropOffService = new CourierDropOffService(uriString, version, clientId, clientSecret);

            //var model = new VerifyOtp
            //{
            //    LockerStationid = "87471edc-37d6-41ef-8521-b96116e707a5",
            //    Code = "123456",
            //    LspId = "123456",
            //    RefCode = "123456",
            //    PhoneNumber = "123456"
            //};

            var model = new LspUserAccess();
            model.LockerStationid = "87471edc-37d6-41ef-8521-b96116e707a5";
            model.Key = "865054858188";
            model.Pin = "123456";
            courierDropOffService.LspVerification(model);

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
