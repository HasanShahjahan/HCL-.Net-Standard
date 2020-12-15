using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Domain.Managers;
using System;
using System.Text.Json;

namespace HardwareControlModule.TestCaller
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "mongodb://localhost:27017";
            string databaseName = "LssHwapiDb";
            string collectionName = "Lockers";
            //string[] validCompartmentIds = { "M0-1"};
            //string[] validCompartmentIds = { "M0-1", "M0-2"};
            //string[] validCompartmentIds = { "M0-1", "M0-2", "M0-3" };
            string[] validCompartmentIds = { "All"};
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2MDkzNTU5MjEsInRyYW5zYWN0aW9uX2lkIjoiNzBiMzZjNDEtMDc4Yi00MTFiLTk4MmMtYzViNzc0YWFjNjZmIn0.ujOkQJUq5WY_tZJgKXqe_n4nql3cSAeHMfXGABZO3E4";
            string jwtSecret = "HWAPI_0BwRn5Bg4rJAe5eyWkRz";

            var compartment = new Compartment("1234", "PANLOCKER-1", validCompartmentIds, false, jwtSecret, token, connectionString, databaseName, collectionName);
            var result = LockerManager.OpenCompartment(compartment);

            //var result = LockerManager.CompartmentStatus(compartment);
            Console.WriteLine(JsonSerializer.Serialize(result));
            Console.ReadKey();
        }
    }
}
