using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Utiles;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Domain.Services;
using System;
using System.Collections.Generic;

using NAudio.Wave;
using System.Timers;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace LSS.HCM.Core.Domain.Helpers
{
    /// <summary>
    ///   Represents Compartment Manager helper class.
    ///</summary>
    public class CompartmentHelper
    {
        
        private static double _timeout;
        private static string _audioFileName; 
        private static int _playbackTime;
        private static WaveOutEvent _outputDevice = new WaveOutEvent();
        private readonly CommunicationPortControlService _communicationPortControlService;

        /// <summary>
        ///   To detect redundant calls.
        ///</summary>
        private bool _disposed = false;

        /// <summary>
        ///   Instantiate a SafeHandle instance.
        ///</summary>
        private SafeHandle _safeHandle = new SafeFileHandle(IntPtr.Zero, true);

        /// <summary>
        ///   Initializes Compartment Manager helper class.
        ///</summary>
        public CompartmentHelper(AppSettings lockerConfiguration) 
        {
            _communicationPortControlService = new CommunicationPortControlService(lockerConfiguration);
        }

        /// <summary>
        /// Map compartment from locker configuration by requested compartment Id.
        /// </summary>
        /// <returns>
        ///  The compartment object mapped from locker configuration.
        /// </returns>
        public static Compartment MapCompartment(string compartmentId, AppSettings lockerConfiguration)
        {
            var target_compartment = lockerConfiguration.Locker.Compartments.Find(compartment => compartment.CompartmentId.Contains(compartmentId));
            return target_compartment;
        }

        /// <summary>
        /// Get any status by compartment module id with comparing locker cofiguration.
        /// </summary>
        /// <returns>
        ///  Return dictionary by getting value from communication port service.
        /// </returns>
        public Dictionary<string, byte> GetStatusByModuleId(string commandType, string compartmentModuleId, AppSettings lockerConfiguration)
        {
            var commandPinCode = new List<byte>()
            {
                Convert.ToByte(compartmentModuleId, 16),
                Convert.ToByte("FF", 16) // Fix data for object detection
            };

            // Command to get status string
            var result = _communicationPortControlService.SendCommand(commandType, commandPinCode, lockerConfiguration);
            Dictionary<string, byte> list = null;

            if (commandType == CommandType.DoorStatus) list = Utiles.GetStatusList(result["statusAry"]);
            else if (commandType == CommandType.ItemDetection) list = Utiles.GetStatusList(result["detectionAry"]); // Convert statius string to byte array

            return list;
        }

        /// <summary>
        /// Get compartment pin code by requested compartment object.
        /// </summary>
        /// <returns>
        ///  List of byte of compartment pin. 
        /// </returns>
        public List<byte> MapModuleId(Compartment compartment)
        {
            List<byte> compartmentPinCode = new List<byte>() {
                Convert.ToByte(compartment.CompartmentCode.Lcbmod, 16),
                Convert.ToByte(compartment.CompartmentCode.Lcbid, 16)
            };
            return compartmentPinCode;
        }


        /// <summary>
        /// Setup door open timer to alert if time out.
        /// </summary>
        /// <returns>
        ///  Return nothing
        /// </returns>
        public void SetDoorOpenTimer(AppSettings lockerConfiguration)
        {
            _timeout = lockerConfiguration.Buzzer.Timeout;
            _audioFileName = lockerConfiguration.Buzzer.AudioFileName;
            _playbackTime = lockerConfiguration.Buzzer.PlaybackTime;
            System.Timers.Timer aTimer = new System.Timers.Timer(_timeout);

            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        /// <summary>
        /// Alert sound timer event.
        /// </summary>
        /// <returns>
        ///  Return nothing
        /// </returns>
        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            AudioFileReader _audioFile = new AudioFileReader(_audioFileName); // "C:/Windows/media/Ring08.wav");
            _outputDevice.Init(_audioFile);
            _outputDevice.Play();
            Thread.Sleep(_playbackTime);
            _outputDevice.Stop();
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
                // Dispose managed state (managed objects).
                _safeHandle?.Dispose();
                _communicationPortControlService.Dispose();
            }

            _disposed = true;
        }
    }
}
