using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Helpers;
using LSS.HCM.Core.Domain.Services;
using LSS.HCM.Core.Entities.Locker;
using Microsoft.Win32.SafeHandles;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using CompartmentConfiguration = LSS.HCM.Core.DataObjects.Settings.Compartment;

namespace LSS.HCM.Core.Domain.Managers
{
    /// <summary>
    ///   Represents Compartment Mangement for open compartment and compartment status.
    ///</summary>
    public class CompartmentManager
    {
        /// <summary>
        ///   To detect redundant calls.
        ///</summary>
        private bool _disposed = false;

        /// <summary>
        ///   Instantiate a SafeHandle instance.
        ///</summary>
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        private readonly CompartmentHelper _compartmentHelper;
        private readonly CompartmentService _compartmentService;

        /// <summary>
        ///   Initializes Compartment Mangement for open compartment and compartment status.
        ///</summary>
        public CompartmentManager(AppSettings lockerConfiguration) 
        {
            _compartmentHelper = new CompartmentHelper(lockerConfiguration);
            _compartmentService = new CompartmentService(lockerConfiguration);
        }

        /// <summary>
        /// Manage open compartment by requested object and based on locker configuration.
        /// </summary>
        /// <returns>
        ///  The compartment object mapped from locker configuration.
        /// </returns>
        public Locker CompartmentOpen(DataObjects.Models.Compartment model, AppSettings lockerConfiguration) 
        {
            var odbModuleList = new List<string> { }; // Find  object detection module id list by input list of compartment
            List<CompartmentConfiguration> compartments = lockerConfiguration.Locker.Compartments;

            if (model.CompartmentIds.Any(CompartmentId => CompartmentId == "All")) model.CompartmentIds = lockerConfiguration.Locker.Compartments.Select(compartment => compartment.CompartmentId).ToArray();
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
                objectdetectStatusAry[moduleNo] = _compartmentHelper.GetStatusByModuleId(CommandType.ItemDetection, moduleNo, lockerConfiguration);
                Log.Debug("[HCM][Compartment Manager][Compartment Open]" + "[Module No : " + moduleNo + "]" + "[Object Detection Status Array : " + objectdetectStatusAry[moduleNo] + "]");
            }


            var result = new Locker();
            bool compartmentDoorStatusAlert = false;
            foreach (var compartmentId in model.CompartmentIds)
            {
                var targetCompartment = _compartmentService.CompartmentOpen(compartmentId, lockerConfiguration);

                // Update objectdetection status
                CompartmentConfiguration targetCompartmentConfig = CompartmentHelper.MapCompartment(compartmentId, lockerConfiguration);
                Dictionary<string, byte> objectdetectStatus = objectdetectStatusAry[targetCompartmentConfig.CompartmentCode.Odbmod];
                targetCompartment.ObjectDetected = objectdetectStatus[targetCompartmentConfig.CompartmentCode.Odbid] == 1 ? true : false;
                result.Compartments.Add(targetCompartment);
                compartmentDoorStatusAlert |= targetCompartment.CompartmentDoorOpen;
            }
            result.TransactionId = model.TransactionId;

            // Set alert timer
            if(compartmentDoorStatusAlert)
            {
                Log.Information("[HCM][Compartment Manager][Compartment Open]" + "[Set Door Open Timer]");
                _compartmentHelper.SetDoorOpenTimer(lockerConfiguration);
                //CompartmentHelper.EndDoorOpenTimer();
            }

            return result;
        }


        /// <summary>
        /// Manage compartment status by requested object and based on locker configuration.
        /// </summary>
        /// <returns>
        ///  List of compartment status object mapped from locker configuration.
        /// </returns>
        public List<Entities.Locker.Compartment> CompartmentStatus(DataObjects.Models.Compartment model, AppSettings lockerConfiguration)
        {
            // Find  object detection module id list by input list of compartment
            var compartments = _compartmentService.CompartmentStatus(model, lockerConfiguration);
            return compartments;
        }

        /// <summary>
        ///   Public implementation of Dispose pattern callable by consumers.
        ///</summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///   Protected implementation of Dispose pattern.
        ///</summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _safeHandle?.Dispose();
                _compartmentHelper?.Dispose();
                _compartmentService?.Dispose();
            }

            _disposed = true;
        }
    }
}
