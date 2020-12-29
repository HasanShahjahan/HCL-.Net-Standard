using LSS.BE.Core.Domain.Services;
using LSS.BE.Core.Entities.Models;
using LSS.BE.Core.TestCaller.Models;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Domain.Managers;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading.Tasks;
using LSS.HCM.Core.Domain.Services;

namespace LSS.BE.Core.TestCaller
{
    class Program
    {
        private static string scanningValue = string.Empty;
        static int Main(string[] args)
        {

            //Task.Run(() => StartSocketListener());
            try
            {

                var (lockerStationId, gatewayService) = GetewayServiceClient.Init();

                if (gatewayService.LockerManager!= null && !gatewayService.LockerManager.PortsHealthCheck.IsLockPortAvailable) Console.WriteLine("Lock Port : Not Available");
                else Console.WriteLine("Lock Port : Available");
                if (gatewayService.LockerManager != null && !gatewayService.LockerManager.PortsHealthCheck.IsDetectionPortAvailable) Console.WriteLine("Object Detection Port : Not Available");
                else Console.WriteLine("Object Detection Port : Available");
                if (gatewayService.LockerManager != null && !gatewayService.LockerManager.PortsHealthCheck.IsScannernPortAvailable) Console.WriteLine("Scanner Port : Not Available");
                else Console.WriteLine("Scanner Port : Available\n");

                if (gatewayService.TokenResponse.StatusCode != 200)
                {
                    Console.WriteLine("Sevice Initialization is failed with status : " + gatewayService.TokenResponse.StatusCode);
                    return 0;
                }

                Console.Write("Gateway Service Initialized\n");

                Console.WriteLine("Please select user Case type : \n 1.CDO (Courier Drop Off) \n 2.CC(Consumer Collect)");
                Console.Write("Use case type :");
                string useCaseType = Console.ReadLine();

                if (useCaseType == "CDO")
                {
                    GetewayServiceClient.CourierDropOff(lockerStationId, gatewayService);
                }
                else if (useCaseType == "CC")
                {
                    GetewayServiceClient.ConsumerCollect(lockerStationId, gatewayService);
                }
                else
                {
                    Console.WriteLine("Invalid use case type.");
                }

            }
            catch (System.IO.FileNotFoundException)
            {
                Console.WriteLine("Please provide valid configuration file path.");
                GetewayServiceClient.Init();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                Console.ReadKey();
            }
            return 0;

        }

        // Socket Scanner
        public static void StartSocketListener()
        {
            Task.Run(() =>
            {
                var server = new SocketListenerService("localhost", 11000);
                server.AsyncStart(UpdateScannerValue);
            });
            UpdateScannerValue("scanner start\r\n");
            //server.ResponseToClient(data.Key, "this is cool!");


        }

        private static string UpdateScannerValue(string inputText)
        {
            scanningValue = inputText;
            Console.Write("Scanning Value :");
            Console.WriteLine(scanningValue);
            return inputText;
        }
    }
}
