using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Helpers;
using LSS.HCM.Core.Domain.Services;
using LSS.HCM.Core.Entities.Locker;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using CompartmentConfiguration = LSS.HCM.Core.DataObjects.Settings.Compartment;

namespace LSS.HCM.Core.Domain.Managers
{
    public sealed class CompartmentManager
    {
        public static Locker CompartmentOpen(DataObjects.Models.Compartment model, LockerConfiguration lockerConfiguration) 
        {
            var odbModuleList = new List<string> { }; // Find  object detection module id list by input list of compartment
            List<CompartmentConfiguration> compartments = lockerConfiguration.Compartments;

            if (model.CompartmentIds.Any(CompartmentId => CompartmentId == "All")) model.CompartmentIds = lockerConfiguration.Compartments.Select(compartment => compartment.CompartmentId).ToArray();
            foreach (var compartmentId in model.CompartmentIds) 
            {
                var compartment = compartments.Find(c => c.CompartmentId == compartmentId);
                if (!odbModuleList.Contains(compartment.CompartmentCode.Odbmod)) // Get object detection module id
                {
                    odbModuleList.Add(compartment.CompartmentCode.Odbmod);
                }
            }
            
            // Update object detection status of selected modules
            var objectdetectStatusAry = new Dictionary<string, Dictionary<string, byte>> { };
            foreach (string moduleNo in odbModuleList)
            {
                objectdetectStatusAry[moduleNo] = CompartmentHelper.GetStatusByModuleId(CommandType.ItemDetection, moduleNo);
            }

            var result = new Locker();
            foreach (var compartmentId in model.CompartmentIds)
            {
                var targetCompartment = CompartmentService.CompartmentOpen(compartmentId, lockerConfiguration);

                // Update objectdetection status
                CompartmentConfiguration targetCompartmentConfig = CompartmentHelper.MapCompartment(compartmentId, lockerConfiguration);
                Dictionary<string, byte> objectdetectStatus = objectdetectStatusAry[targetCompartmentConfig.CompartmentCode.Odbmod];
                targetCompartment.ObjectDetected = objectdetectStatus[targetCompartmentConfig.CompartmentCode.Odbid] == 1 ? true : false;
                result.Compartments.Add(targetCompartment);
            }
            result.TransactionId = model.TransactionId;
            return result;
        }
        public static List<Entities.Locker.Compartment> CompartmentStatus(DataObjects.Models.Compartment model, LockerConfiguration lockerConfiguration)
        {
            // Find  object detection module id list by input list of compartment
            var compartments = CompartmentService.CompartmentStatus(model, lockerConfiguration);
            return compartments;
        }
    }
}
