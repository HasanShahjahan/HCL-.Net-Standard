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


            string configurationPath = @"C:\Box24\Project Execution\config.json";

            var courierDropOffService = new CourierDropOffService(uriString, version, clientId, clientSecret, configurationPath);

            //LSP Verification
            var model = new LspUserAccess
            {
                LockerStationid = lockerStationId,
                Key = "865054858188",
                Pin = "123456"
            };
            var lspVerification = courierDropOffService.LspVerification(model);


            //Verify Otp
            var verifyOtp = new VerifyOtp();
            verifyOtp.LockerStationId = "c17fb923-70f9-4d3c-b081-4226096d6905";
            verifyOtp.LspId = "0add4ba4-2e62-417b-984f-183f3d11baf7";
            verifyOtp.RefCode = "GBDY";
            verifyOtp.PhoneNumber = "+6597398077";
            verifyOtp.Code = "041052";
            var verifyOtpResult = courierDropOffService.VerifyOtp(verifyOtp);

            //Finiding Booking 
            var findingBookingResult = courierDropOffService.FindBooking(trackingNumber, lockerStationId, lspId);



            //update booking
            var bookingStatus = new BookingStatus
            {
                BookingId = 14,
                LockerStationId = lockerStationId,
                LspId = lspId,
                MobileNumber = mobileNumber,
                Reason = "",
                Status = "courier_deposited"
            };
            var bookingStatusResult = courierDropOffService.UpdateBookingStatus(bookingStatus);

            //Locker Station Details 
            var lockerDetails = courierDropOffService.LockerStationDetails(lockerStationId);

            //Available Size
            var result = courierDropOffService.GetAvailableSizes(lockerStationId, 0);
            var jObject = courierDropOffService.GetAvailableSizes(lockerStationId, 0, string.Empty);
            var formattedString = courierDropOffService.GetAvailableSizes(lockerStationId, 0, string.Empty);

            Console.WriteLine("C# standard mapped result: ");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(result,Formatting.Indented));
            Console.WriteLine("------------------------------------------");

            Console.WriteLine("Actual Result : JObject");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(jObject, Formatting.Indented));
            Console.WriteLine("------------------------------------------");

            Console.WriteLine("Actual Result : Formatted string.");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(formattedString, Formatting.Indented));
           

            Console.WriteLine("Locker Station Details");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(lockerDetails, Formatting.Indented));
            Console.WriteLine("------------------------------------------");


            Console.WriteLine("Update Booking status");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(bookingStatusResult, Formatting.Indented));
            Console.WriteLine("------------------------------------------");


            Console.WriteLine("Finding Booking Result");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(findingBookingResult, Formatting.Indented));
            Console.WriteLine("------------------------------------------");


            Console.WriteLine("Verify Otp Result");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(verifyOtpResult, Formatting.Indented));
            Console.WriteLine("------------------------------------------");

            Console.WriteLine("Lsp Verification");
            Console.WriteLine("------------------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(lspVerification, Formatting.Indented));
            Console.WriteLine("------------------------------------------");

            Console.ReadKey();
        }
    }
}
