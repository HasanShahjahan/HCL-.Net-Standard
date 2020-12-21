using LSS.BE.Core.Domain.Services;
using LSS.BE.Core.Entities.Courier;
using Newtonsoft.Json;
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

            var model = new LspUserAccess
            {
                LockerStationid = "c17fb923-70f9-4d3c-b081-4226096d6905",
                Key = "865054858188",
                Pin = "123456"
            };
            var result = courierDropOffService.LspVerification(model);
            Console.WriteLine(JsonConvert.SerializeObject(result,Formatting.Indented));
            Console.ReadKey();
        }
    }
}
