using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Helpers;
using LSS.HCM.Core.Domain.Services;
using LSS.HCM.Core.Entities.Locker;
using LSS.HCM.Core.Infrastructure.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using CompartmentConfig = LSS.HCM.Core.DataObjects.Settings.Compartment;

namespace LSS.HCM.Core.Domain.Managers
{
    public sealed class CompartmentManager
    {
        public static Locker CompartmentOpen(DataObjects.Models.Compartment model) 
        {
            var lockerConfiguration = Repository<LockerConfiguration>.Get(model.DataBaseCredentials.ConnectionString, model.DataBaseCredentials.DatabaseName, model.DataBaseCredentials.CollectionName).Find(configuration => configuration.Id == "5fca3a252d8c3433f408558d").FirstOrDefault();
            // Find  object detection module id list by input list of compartment
            var odbModuleList = new List<string> { };
            List<CompartmentConfig> compartments = lockerConfiguration.Compartments;
            foreach (CompartmentConfig compatment in compartments)
            {
                if (model.CompartmentIds.Contains(compatment.CompartmentId))
                {
                    // Get object detection module id
                    if (!odbModuleList.Contains(compatment.CompartmentCode.Odbmod))
                    {
                        odbModuleList.Add(compatment.CompartmentCode.Odbmod);
                    }
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
                CompartmentConfig targetCompartmentConfig = CompartmentHelper.MapCompartment(compartmentId, lockerConfiguration);
                Dictionary<string, byte> objectdetectStatus = objectdetectStatusAry[targetCompartmentConfig.CompartmentCode.Odbmod];
                targetCompartment.ObjectDetected = objectdetectStatus[targetCompartmentConfig.CompartmentCode.Odbid] == 0 ? true : false;
                result.Compartments.Add(targetCompartment);
            }
            result.TransactionId = model.TransactionId;
            return result;
        }
        public static List<Entities.Locker.Compartment> CompartmentStatus(DataObjects.Models.Compartment model)
        {
            var lockerConfiguration = Repository<LockerConfiguration>.Get(model.DataBaseCredentials.ConnectionString, model.DataBaseCredentials.DatabaseName, model.DataBaseCredentials.CollectionName).Find(configuration => configuration.Id == "5fca3a252d8c3433f408558d").FirstOrDefault();
            // Find  object detection module id list by input list of compartment
            var compartments = CompartmentService.CompartmentStatus(lockerConfiguration);
            return compartments;
        }
    }
}
