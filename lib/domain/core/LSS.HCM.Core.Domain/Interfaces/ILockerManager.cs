using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.Domain.Interfaces
{
    /// <summary>
    ///   Represents locker as a sequence of compartment open and it's status.
    ///</summary>
    public interface ILockerManager
    {
        /// <summary>
        /// Gets the open compartment parameters with Json web token credentials and MongoDB database credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment open status with object detection, LED status by requested compartment id's.
        /// </returns>
        LockerDto OpenCompartment(Compartment model);

        /// <summary>
        /// Gets the compartment status with Json web token credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  List of compartment status with object detection and LED status based requested compartment id's.
        /// </returns>
        CompartmentStatusDto CompartmentStatus(Compartment model);

        /// <summary>
        /// Gets the capture image parameters with Json web token credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// </summary>
        /// <returns>
        ///  Byte array of image with transaction Id and image extension.
        /// </returns>
        CaptureDto CaptureImage(Capture model);

        /// <summary>
        /// Start scanner event listener
        /// </summary>
        /// <returns>
        ///  Nothing return
        /// </returns>
        void RegisterScannerEvent(Func<string, string> sendDataOnSocket);
    }
}
