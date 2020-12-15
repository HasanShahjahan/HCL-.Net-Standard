using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Infrastructure.Repository;
using LSS.HCM.Core.Security.Handlers;
using MongoDB.Driver;
using System.Linq;

namespace LSS.HCM.Core.Validator
{
    public class LockerManagementValidator
    {
        public static (int, ApplicationException, LockerConfiguration) PayloadValidator(bool jwtToken, string jwtSecret, string token, string payloadType, string connectionString, string databaseName, string collectionName, string lockerId, string transactionId, string[] compartmentIds, string captureType)
        {
            int statusCode = StatusCode.Status200OK;
            ApplicationException result = null;
            LockerConfiguration lockerConfiguration = null;
            try
            {
                #region Json Web Token

                if (jwtToken)
                {
                    if (string.IsNullOrEmpty(token))
                    {
                        statusCode = StatusCode.Status422UnprocessableEntity;
                        result = new ApplicationException { Code = ApplicationErrorCodes.InvalidToken, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) };
                        return (statusCode, result, null);
                    }
                    var (isVerified, transactionid) = JwtTokenHandler.VerifyJwtSecurityToken(jwtSecret, token);
                    if ((!isVerified) || string.IsNullOrEmpty(transactionid))
                    {
                        statusCode = StatusCode.Status422UnprocessableEntity;
                        result = new ApplicationException { Code = ApplicationErrorCodes.InvalidToken, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) };
                        return (statusCode, result, null);
                    }
                }

                #endregion

                #region Mongo DB Initialization 

                lockerConfiguration = Repository<LockerConfiguration>.Get(connectionString, databaseName, collectionName).Find(configuration => configuration.LockerId == lockerId).FirstOrDefault();

                #endregion

                #region Payload Validation 

                switch (payloadType)
                {
                    case PayloadTypes.OpenCompartment:

                        if (string.IsNullOrEmpty(transactionId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyTransactionId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId) };
                            return (statusCode, result, null);
                        }
                        else if (string.IsNullOrEmpty(lockerId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) };
                            return (statusCode, result, null);
                        }
                        else if (compartmentIds == null || compartmentIds.Length == 0)
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyCompartmentId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCompartmentId) };
                            return (statusCode, result, null);
                        }
                        else if (compartmentIds.Length > 0 && !compartmentIds.Contains("All"))
                        {
                            if (lockerConfiguration != null && lockerConfiguration.LockerId != lockerId)
                            {
                                statusCode = StatusCode.Status422UnprocessableEntity;
                                result = new ApplicationException { Code = ApplicationErrorCodes.InvalidLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) };
                                return (statusCode, result, null);
                            }
                            if (lockerConfiguration != null && lockerConfiguration.Compartments.Count() > 0)
                            {
                                foreach (string compartmentId in compartmentIds)
                                {
                                    var flag = lockerConfiguration.Compartments.Any(com => com.CompartmentId == compartmentId);
                                    if (flag) continue;
                                    else
                                    {
                                        statusCode = StatusCode.Status422UnprocessableEntity;
                                        result = new ApplicationException
                                        {
                                            Code = ApplicationErrorCodes.InvalidCompartmentId,
                                            Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCompartmentId)
                                        };
                                        return (statusCode, result, null);

                                    }
                                }

                            }
                        }
                        return (statusCode, result, lockerConfiguration);

                    case PayloadTypes.CompartmentStatus:

                        if (string.IsNullOrEmpty(lockerId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) };
                            return (statusCode, result, null);
                        }
                        else if (lockerConfiguration != null && lockerConfiguration.LockerId != lockerId)
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.InvalidLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) };
                            return (statusCode, result, null);
                        }
                        if (lockerConfiguration != null && lockerConfiguration.Compartments.Count() > 0)
                        {
                            foreach (string compartmentId in compartmentIds)
                            {
                                var flag = lockerConfiguration.Compartments.Any(compartment => compartment.CompartmentId == compartmentId);
                                if (flag) continue;
                                else
                                {
                                    statusCode = StatusCode.Status422UnprocessableEntity;
                                    result = new ApplicationException
                                    {
                                        Code = ApplicationErrorCodes.InvalidCompartmentId,
                                        Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCompartmentId)
                                    };
                                    return (statusCode, result, null);

                                }
                            }

                        }
                        return (statusCode, result, lockerConfiguration);

                    case PayloadTypes.LockerStatus:

                        if (string.IsNullOrEmpty(lockerId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) };
                            return (statusCode, result, null);
                        }
                        if (lockerConfiguration != null && lockerConfiguration.LockerId != lockerId)
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.InvalidLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) };
                            return (statusCode, result, null);
                        }
                        return (statusCode, result, lockerConfiguration);

                    case PayloadTypes.CaptureImage:

                        if (string.IsNullOrEmpty(lockerId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) };
                            return (statusCode, result, null);
                        }
                        else if (lockerConfiguration != null && lockerConfiguration.LockerId != lockerId)
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.InvalidLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) };
                            return (statusCode, result, null);
                        }
                        else if (string.IsNullOrEmpty(captureType))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyCaptureType, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCaptureType) };
                            return (statusCode, result, null);
                        }
                        else if (!(captureType == CaptureType.Photo || captureType == CaptureType.Screen))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.InvalidCaptureType, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCaptureType) };
                            return (statusCode, result, null);
                        }
                        else if (string.IsNullOrEmpty(transactionId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyTransactionId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId) };
                            return (statusCode, result, null);
                        }
                        return (statusCode, result, lockerConfiguration);

                }
                
                #endregion
            }
            catch (MongoConfigurationException)
            {
                statusCode = StatusCode.Status502BadGateway;
                result = new ApplicationException
                {
                    Code = ApplicationErrorCodes.MongoDbConnectionProblem,
                    Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.MongoDbConnectionProblem)
                };
            }
            return (statusCode, result, lockerConfiguration);
        }

    }
}
