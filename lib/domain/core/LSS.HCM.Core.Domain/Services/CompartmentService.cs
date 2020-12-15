using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using System;
using System.Collections.Generic;
using CompartmentConfiguration = LSS.HCM.Core.DataObjects.Settings.Compartment;
using Compartment = LSS.HCM.Core.Entities.Locker.Compartment;
using LSS.HCM.Core.Domain.Helpers;
using System.Linq;

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

        public static List<Compartment> CompartmentStatus(DataObjects.Models.Compartment model, LockerConfiguration lockerConfiguration)
        {
            // Find locker controller board module id and object detection module id list
            var lcbModuleList = new List<string> { };
            var odbModuleList = new List<string> { };
            List<CompartmentConfiguration> compartments = lockerConfiguration.Compartments;
           
            if (model.CompartmentIds.Any(CompartmentId => CompartmentId == "All")) model.CompartmentIds = lockerConfiguration.Compartments.Select(compartment => compartment.CompartmentId).ToArray();
           
            foreach (var compartmentId in model.CompartmentIds)
            {
                var compartment = compartments.Find(c => c.CompartmentId == compartmentId);
                if (!odbModuleList.Contains(compartment.CompartmentCode.Odbmod)) // Get object detection module id
                {
                    odbModuleList.Add(compartment.CompartmentCode.Odbmod);
                }

                // Get locker controller module id
                if (!lcbModuleList.Contains(compartment.CompartmentCode.Lcbmod))
                {
                    lcbModuleList.Add(compartment.CompartmentCode.Lcbmod);
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

            foreach (var compatmentId in model.CompartmentIds)
            {
                var compartment = compartments.Find(c => c.CompartmentId == compatmentId);
                
                Dictionary<string, byte> opendoorStatus = opendoorStatusAry[compartment.CompartmentCode.Lcbmod]; //[compatment.CompartmentCode.Lcbid];
                Dictionary<string, byte> objectdetectStatus = objectdetectStatusAry[compartment.CompartmentCode.Odbmod]; //[compatment.CompartmentCode.Odbid];
                Compartment compartmentResult = new Compartment(lockerConfiguration.LockerId,
                                                                 compartment.CompartmentId,
                                                                 compartment.CompartmentSize,
                                                                 opendoorStatus[compartment.CompartmentCode.Lcbid] == 0 ? true : false,
                                                                 opendoorStatus[compartment.CompartmentCode.Lcbid] == 0 ? false : true,
                                                                 objectdetectStatus[compartment.CompartmentCode.Odbid] == 1 ? true : false,
                                                                 opendoorStatus[compartment.CompartmentCode.Lcbid] == 0 ? "ON" : "OFF");
                compartmentList.Add(compartmentResult);
            }

            return compartmentList;
        }
        
    }
}
