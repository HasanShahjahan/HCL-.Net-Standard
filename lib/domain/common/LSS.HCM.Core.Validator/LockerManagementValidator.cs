using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Security.Handlers;
using System;
using System.Linq;
using ApplicationException = LSS.HCM.Core.Common.Exceptions.ApplicationException;

namespace LSS.HCM.Core.Validator
{
    public class LockerManagementValidator
    {
        public static (int, ApplicationException) PayloadValidator(AppSettings lockerConfiguration, bool jwtToken, string jwtSecret, string token, string payloadType, string lockerId, string transactionId, string[] compartmentIds, string captureType)
        {
            int statusCode = StatusCode.Status200OK;
            ApplicationException result = null;
            try
            {
                #region Json Web Token

                if (jwtToken)
                {
                    if (string.IsNullOrEmpty(token))
                    {
                        statusCode = StatusCode.Status422UnprocessableEntity;
                        result = new ApplicationException { Code = ApplicationErrorCodes.InvalidToken, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) };
                        return (statusCode, result);
                    }
                    var (isVerified, transactionid) = JwtTokenHandler.VerifyJwtSecurityToken(jwtSecret, token);
                    if ((!isVerified) || string.IsNullOrEmpty(transactionid))
                    {
                        statusCode = StatusCode.Status422UnprocessableEntity;
                        result = new ApplicationException { Code = ApplicationErrorCodes.InvalidToken, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidToken) };
                        return (statusCode, result);
                    }
                }

                #endregion


                #region Payload Validation 

                switch (payloadType)
                {
                    case PayloadTypes.OpenCompartment:

                        if (string.IsNullOrEmpty(transactionId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyTransactionId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId) };
                            return (statusCode, result);
                        }
                        else if (string.IsNullOrEmpty(lockerId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) };
                            return (statusCode, result);
                        }
                        else if (compartmentIds == null || compartmentIds.Length == 0)
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyCompartmentId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCompartmentId) };
                            return (statusCode, result);
                        }
                        else if (compartmentIds.Length > 0 && !compartmentIds.Contains("All"))
                        {
                            if (lockerConfiguration != null && lockerConfiguration.Locker.LockerId != lockerId)
                            {
                                statusCode = StatusCode.Status422UnprocessableEntity;
                                result = new ApplicationException { Code = ApplicationErrorCodes.InvalidLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) };
                                return (statusCode, result);
                            }
                            if (lockerConfiguration != null && lockerConfiguration.Locker.Compartments.Count() > 0)
                            {
                                foreach (string compartmentId in compartmentIds)
                                {
                                    var flag = lockerConfiguration.Locker.Compartments.Any(com => com.CompartmentId == compartmentId);
                                    if (flag) continue;
                                    else
                                    {
                                        statusCode = StatusCode.Status422UnprocessableEntity;
                                        result = new ApplicationException
                                        {
                                            Code = ApplicationErrorCodes.InvalidCompartmentId,
                                            Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCompartmentId)
                                        };
                                        return (statusCode, result);

                                    }
                                }

                            }
                        }
                        return (statusCode, result);

                    case PayloadTypes.CompartmentStatus:

                        if (string.IsNullOrEmpty(lockerId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) };
                            return (statusCode, result);
                        }
                        else if (lockerConfiguration != null && lockerConfiguration.Locker.LockerId != lockerId)
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.InvalidLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) };
                            return (statusCode, result);
                        }
                        if (lockerConfiguration != null && lockerConfiguration.Locker.Compartments.Count() > 0 && !compartmentIds.Contains("All"))
                        {
                            foreach (string compartmentId in compartmentIds)
                            {
                                var flag = lockerConfiguration.Locker.Compartments.Any(compartment => compartment.CompartmentId == compartmentId);
                                if (flag) continue;
                                else
                                {
                                    statusCode = StatusCode.Status422UnprocessableEntity;
                                    result = new ApplicationException
                                    {
                                        Code = ApplicationErrorCodes.InvalidCompartmentId,
                                        Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCompartmentId)
                                    };
                                    return (statusCode, result);

                                }
                            }

                        }
                        return (statusCode, result);

                    case PayloadTypes.LockerStatus:

                        if (string.IsNullOrEmpty(lockerId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) };
                            return (statusCode, result);
                        }
                        if (lockerConfiguration != null && lockerConfiguration.Locker.LockerId != lockerId)
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.InvalidLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) };
                            return (statusCode, result);
                        }
                        return (statusCode, result);

                    case PayloadTypes.CaptureImage:

                        if (string.IsNullOrEmpty(lockerId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId) };
                            return (statusCode, result);
                        }
                        else if (lockerConfiguration != null && lockerConfiguration.Locker.LockerId != lockerId)
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.InvalidLockerId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId) };
                            return (statusCode, result);
                        }
                        else if (string.IsNullOrEmpty(captureType))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyCaptureType, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCaptureType) };
                            return (statusCode, result);
                        }
                        else if (!(captureType == CaptureType.Photo || captureType == CaptureType.Screen))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.InvalidCaptureType, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCaptureType) };
                            return (statusCode, result);
                        }
                        else if (string.IsNullOrEmpty(transactionId))
                        {
                            statusCode = StatusCode.Status422UnprocessableEntity;
                            result = new ApplicationException { Code = ApplicationErrorCodes.EmptyTransactionId, Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId) };
                            return (statusCode, result);
                        }
                        return (statusCode, result);

                }
                
                #endregion
            }
            catch (Exception ex)
            {
                statusCode = StatusCode.Status502BadGateway;
                result = new ApplicationException
                {
                    Code = ApplicationErrorCodes.UnknownError,
                    Message = ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.UnknownError)
                };
            }
            return (statusCode, result);
        }

    }
}
