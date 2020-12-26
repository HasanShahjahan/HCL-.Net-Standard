using LSS.BE.Core.Domain.Services;
using LSS.BE.Core.Entities.Models;
using LSS.BE.Core.TestCaller.Models;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Domain.Managers;
using Newtonsoft.Json;
using System;
using System.Globalization;

namespace LSS.BE.Core.TestCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var (lockerStationId, gatewayService) = GetewayServiceClient.Init();
                Console.Write("Use case type :");

                Console.WriteLine("User Case Type : \n 1.CDO (Courier Drop Off) \n 2.CC(Consumer Collect)");
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
            catch (System.IO.FileNotFoundException ex)
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

        }
    }
}
