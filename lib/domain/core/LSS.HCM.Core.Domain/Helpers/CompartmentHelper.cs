using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Utiles;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Services;
using System;
using System.Collections.Generic;

namespace LSS.HCM.Core.Domain.Helpers
{
    public sealed class CompartmentHelper
    {
        public static Compartment MapCompartment(string compartmentId, AppSettings lockerConfiguration)
        {
            var target_compartment = lockerConfiguration.Locker.Compartments.Find(compartment => compartment.CompartmentId.Contains(compartmentId));
            return target_compartment;
        }
        public static Dictionary<string, byte> GetStatusByModuleId(string commandType, string compartmentModuleId, AppSettings lockerConfiguration)
        {
            var commandPinCode = new List<byte>()
            {
                Convert.ToByte(compartmentModuleId, 16),
                Convert.ToByte("FF", 16) // Fix data for object detection
            };

            // Command to get status string
            var result = CommunicationPortControlService.SendCommand(commandType, commandPinCode, lockerConfiguration);
            Dictionary<string, byte> list = null;

            if (commandType == CommandType.DoorStatus) list = Utiles.GetStatusList(result["statusAry"]);
            else if (commandType == CommandType.ItemDetection) list = Utiles.GetStatusList(result["detectionAry"]); // Convert statius string to byte array

            return list;
        }
        public static List<byte> MapModuleId(Compartment compartment)
        {
            List<byte> compartmentPinCode = new List<byte>() {
                Convert.ToByte(compartment.CompartmentCode.Lcbmod, 16),
                Convert.ToByte(compartment.CompartmentCode.Lcbid, 16)
            };
            return compartmentPinCode;
        }
    }
}
