using LSS.BE.Core.Domain.Services;
using LSS.BE.Core.Entities.Courier;
using Newtonsoft.Json;
using System;
using System.Globalization;

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

            string trackingNumber = "STG023";
            string lockerStationId = "c17fb923-70f9-4d3c-b081-4226096d6905"; 
            string lspId = "0add4ba4-2e62-417b-984f-183f3d11baf7";

            string mobileNumber = "+6597398077"; 


            var courierDropOffService = new CourierDropOffService(uriString, version, clientId, clientSecret);

            //LSP Verification
            var model = new LspUserAccess
            {
                LockerStationid = "c17fb923-70f9-4d3c-b081-4226096d6905",
                Key = "865054858188",
                Pin = "123456"
            };
            var result = courierDropOffService.LspVerification(model);


            ////Verify Otp
            //var model = new VerifyOtp();
            //model.LockerStationid = "c17fb923-70f9-4d3c-b081-4226096d6905";
            //model.LspId = "0add4ba4-2e62-417b-984f-183f3d11baf7";
            //model.RefCode = "GBDY";
            //model.PhoneNumber = "+6597398077";
            //model.Code = "041052";
            //var result = courierDropOffService.VerifyOtp(model);

            ////Finiding Booking 
            //var result = courierDropOffService.FindBooking(trackingNumber, lockerStationId, lspId);

            ////Available Size
            //var result = courierDropOffService.GetAvailableSizes(lockerStationId, 0);

            ////update booking
            //var model = new bookingstatus
            //{
            //    bookingid = 14,
            //    lockerstationid = lockerstationid,
            //    lspid = lspid,
            //    mobilenumber = mobilenumber,
            //    reason = "",
            //    status = "courier_deposited"
            //};
            //var result = courierdropoffservice.updatebookingstatus(model);


            Console.WriteLine(JsonConvert.SerializeObject(result,Formatting.Indented));
            Console.ReadKey();
        }
    }
}
