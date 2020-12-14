using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.DataObjects.Dtos;
using LSS.HCM.Core.DataObjects.Mappers;
using LSS.HCM.Core.DataObjects.Models;
using LSS.HCM.Core.Validator;
using System.Collections.Generic;

namespace LSS.HCM.Core.Domain.Managers
{
    
    /// <summary>
    ///   Represents locker as a sequence of compartment open and it's status.
    ///</summary>
    public sealed class LockerManager
    {
        /// <summary>
        /// Gets the open compartment parameters with Json web token credentials and MongoDB database credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// MongoDb contains each and every Locker station configuration with compartment details.  
        /// </summary>
        /// <returns>
        ///  List of compartment open status with object detection, LED status by requested compartment id's.
        /// </returns>
        public static LockerDto OpenCompartment(Compartment model)
        {
           
            var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token,PayloadTypes.OpenCompartment, model.DataBaseCredentials.ConnectionString, model.DataBaseCredentials.DatabaseName, model.DataBaseCredentials.CollectionName, model.LockerId, model.TransactionId, model.CompartmentIds, null);
            if (statusCode != StatusCode.Status200OK) return OpenCompartmentMapper.ToError(new LockerDto { StatusCode = statusCode, Error = errorResult});
            var result = CompartmentManager.CompartmentOpen(model);
            return OpenCompartmentMapper.ToObject(result);
        }

        /// <summary>
        /// Gets the compartment status with Json web token credentials and MongoDB database credentials.
        /// Json web token credentials contains enable or disable, if enable then need to provide jwt secret and token.
        /// MongoDb contains each and every Locker station configuration with compartment details.  
        /// </summary>
        /// <returns>
        ///  List of compartment status with object detection and LED status based requested compartment id's.
        /// </returns>
        public static CompartmentStatusDto CompartmentStatus(Compartment model)
        {
            var (statusCode, errorResult) = LockerManagementValidator.PayloadValidator(model.JwtCredentials.IsEnabled, model.JwtCredentials.Secret, model.JwtCredentials.Token, PayloadTypes.CompartmentStatus, model.DataBaseCredentials.ConnectionString, model.DataBaseCredentials.DatabaseName, model.DataBaseCredentials.CollectionName, model.LockerId, model.TransactionId, model.CompartmentIds, null);
            if (statusCode != StatusCode.Status200OK) return CompartmentStatusMapper.ToError(new CompartmentStatusDto { StatusCode = statusCode, Error = errorResult });
            var result = CompartmentManager.CompartmentStatus(model);
            return CompartmentStatusMapper.ToObject(result);
        }
    }
}
