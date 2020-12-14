using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using System;
using System.Collections.Generic;
using CompartmentConfig = LSS.HCM.Core.DataObjects.Settings.Compartment;
using Compartment = LSS.HCM.Core.Entities.Locker.Compartment;
using LSS.HCM.Core.Domain.Helpers;

namespace LSS.HCM.Core.Domain.Services
{
    public class CompartmentService
    {
        public static Compartment CompartmentOpen(string compartmentId, LockerConfiguration lockerConfiguration)
        {
            // Find the target compartment
            var target_compartment = lockerConfiguration.Compartments.Find(compartment => compartment.CompartmentId.Contains(compartmentId));

            // Commanding
            var DoorOpenResult = CommunicationPortControlService.SendCommand(CommandType.DoorOpen, CompartmentHelper.MapModuleId(target_compartment));
            
            Compartment compartmentResult = new Compartment(lockerConfiguration.LockerId,
                                                             target_compartment.CompartmentId,
                                                             target_compartment.CompartmentSize,
                                                             Convert.ToBoolean(DoorOpenResult["DoorOpen"]),
                                                             !Convert.ToBoolean(DoorOpenResult["DoorOpen"]),
                                                             false,
                                                             Convert.ToBoolean(DoorOpenResult["DoorOpen"]) ? "ON" : "OFF");
            return compartmentResult;
        }

        public static List<Compartment> CompartmentStatus(LockerConfiguration lockerConfiguration)
        {
            // Find locker controller board module id and object detection module id list
            var lcbModuleList = new List<string> { };
            var odbModuleList = new List<string> { };
            List<CompartmentConfig> compartments = lockerConfiguration.Compartments;
            foreach (CompartmentConfig compatment in compartments)
            {
                // Get locker controller module id
                if (!lcbModuleList.Contains(compatment.CompartmentCode.Lcbmod))
                {
                    lcbModuleList.Add(compatment.CompartmentCode.Lcbmod);
                }
                // Get object detection module id
                if (!odbModuleList.Contains(compatment.CompartmentCode.Odbmod))
                {
                    odbModuleList.Add(compatment.CompartmentCode.Odbmod);
                }
            }

            // Get all module update
            List<Compartment> compartmentList = new List<Compartment>();
            var opendoorStatusAry = new Dictionary<string, Dictionary<string, byte>> { };
            foreach (string moduleNo in lcbModuleList)
            {
                opendoorStatusAry[moduleNo] = CompartmentHelper.GetStatusByModuleId(CommandType.DoorStatus, moduleNo);
            }
            var objectdetectStatusAry = new Dictionary<string, Dictionary<string, byte>> { };
            foreach (string moduleNo in odbModuleList)
            {
                objectdetectStatusAry[moduleNo] = CompartmentHelper.GetStatusByModuleId(CommandType.ItemDetection ,moduleNo);
            }

            foreach (CompartmentConfig compatment in compartments)
            {
                Dictionary<string, byte> opendoorStatus = opendoorStatusAry[compatment.CompartmentCode.Lcbmod]; //[compatment.CompartmentCode.Lcbid];
                Dictionary<string, byte> objectdetectStatus = objectdetectStatusAry[compatment.CompartmentCode.Odbmod]; //[compatment.CompartmentCode.Odbid];
                Compartment compartmentResult = new Compartment(lockerConfiguration.LockerId,
                                                                 compatment.CompartmentId,
                                                                 compatment.CompartmentSize,
                                                                 opendoorStatus[compatment.CompartmentCode.Lcbid] == 0 ? true : false,
                                                                 opendoorStatus[compatment.CompartmentCode.Lcbid] == 0 ? false : true,
                                                                 objectdetectStatus[compatment.CompartmentCode.Odbid] == 0 ? true : false,
                                                                 opendoorStatus[compatment.CompartmentCode.Lcbid] == 0 ? "ON" : "OFF");
                compartmentList.Add(compartmentResult);
            }

            return compartmentList;
        }
        
    }
}
