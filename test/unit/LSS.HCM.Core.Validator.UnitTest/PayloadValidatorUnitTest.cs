using LSS.HCM.Core.Common.Enums;
using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.Validator;
using Xunit;

namespace HCM.Core.Validator.UnitTest
{
    public class PayloadValidatorUnitTest
    {
        private readonly string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJleHAiOjE2MDkzNTU5MjEsInRyYW5zYWN0aW9uX2lkIjoiNzBiMzZjNDEtMDc4Yi00MTFiLTk4MmMtYzViNzc0YWFjNjZmIn0.ujOkQJUq5WY_tZJgKXqe_n4nql3cSAeHMfXGABZO3E4";
        private readonly string jwtSecret = "HWAPI_0BwRn5Bg4rJAe5eyWkRz";
        private readonly string transactionId = "70b36c41-078b-411b-982c-c5b774aac66f";
        private readonly string lockerId = "PANLOCKER-1";
        private readonly string[] validCompartmentIds = { "M0-1", "M0-2","M0-3"};
        private readonly string[] invalidCompartmentIds = { "S0-1"};
        private readonly string captureType = "Photo";
        private readonly string connectionString = "mongodb://localhost:27017";
        private readonly string databaseName = "LssHwapiDb";
        private readonly string collectionName = "Lockers";


        #region Open Compartment
        [Fact]
        public void OpenCompartmentEmptyTransactionId()
        {
            var (_, errorResult,_) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.OpenCompartment, connectionString, databaseName, collectionName, lockerId, string.Empty, null, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId), errorResult.Message);
        }

        [Fact]
        public void OpenCompartmentEmptyLockerId()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.OpenCompartment, connectionString, databaseName, collectionName, string.Empty, transactionId, null, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId), errorResult.Message);
        }

        [Fact]
        public void OpenCompartmentInvalidLockerId()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.OpenCompartment, connectionString, databaseName, collectionName, "Hasan", transactionId, validCompartmentIds, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId), errorResult.Message);
        }

        [Fact]
        public void OpenCompartmentValidLockerId()
        {
            var (statusCode, _, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.OpenCompartment, connectionString, databaseName, collectionName ,lockerId, transactionId, validCompartmentIds, null);
            Assert.Equal(200, statusCode);
        }

        [Fact]
        public void OpenCompartmentEmptyCompartmentIds()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.OpenCompartment, connectionString, databaseName, collectionName ,lockerId, transactionId, null, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCompartmentId), errorResult.Message);
        }

        [Fact]
        public void OpenCompartmentInvalidCompartmentIds()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.OpenCompartment, connectionString, databaseName, collectionName ,lockerId, transactionId, invalidCompartmentIds, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCompartmentId), errorResult.Message);
        }

        [Fact]
        public void OpenCompartmentValidCompartmentIds()
        {
            var (statusCode, _, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.OpenCompartment, connectionString, databaseName, collectionName,lockerId, transactionId, validCompartmentIds, null);
            Assert.Equal(200, statusCode);
        }

        #endregion

        #region Compartment Status
        [Fact]
        public void CompartmentStatusEmptyLockerId()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true,jwtSecret, token, PayloadTypes.CompartmentStatus, connectionString, databaseName, collectionName ,string.Empty, transactionId, null, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId), errorResult.Message);
        }

        [Fact]
        public void CompartmentStatusInvalidLockerId()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.CompartmentStatus, connectionString, databaseName, collectionName ,"Hasan", transactionId, validCompartmentIds, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId), errorResult.Message);
        }

        [Fact]
        public void CompartmentStatusValidLockerId()
        {
            var (statusCode, _, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.CompartmentStatus, connectionString, databaseName, collectionName ,lockerId, transactionId, validCompartmentIds, null);
            Assert.Equal(200, statusCode);
        }
        #endregion

        #region Locker Status
        [Fact]
        public void LockerStatusEmptyLockerId()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.LockerStatus, connectionString, databaseName, collectionName, string.Empty, transactionId, null, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId), errorResult.Message);
        }

        [Fact]
        public void LockerStatusInvalidLockerId()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.LockerStatus, connectionString, databaseName, collectionName ,"Hasan", transactionId, validCompartmentIds, null);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId), errorResult.Message);
        }

        [Fact]
        public void LockerStatusValidLockerId()
        {
            var (statusCode, _, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.LockerStatus, connectionString, databaseName, collectionName, lockerId, transactionId, validCompartmentIds, null);
            Assert.Equal(200, statusCode);
        }
        #endregion

        #region Capture Image
        [Fact]
        public void CaptureImageEmptyLockerId()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.CaptureImage, connectionString, databaseName, collectionName, string.Empty, transactionId, null, captureType);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyLockerId), errorResult.Message);
        }

        [Fact]
        public void CaptureImageInvalidLockerId()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.CaptureImage, connectionString, databaseName, collectionName,"Hasan", transactionId, null, captureType);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidLockerId), errorResult.Message);
        }

        [Fact]
        public void CaptureImageValidLockerId()
        {
            var (statusCode, _, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.CaptureImage, connectionString, databaseName, collectionName  ,lockerId, transactionId, null, captureType);
            Assert.Equal(200, statusCode);
        }

        [Fact]
        public void CaptureImageEmptyCaptureType()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.CaptureImage, connectionString, databaseName, collectionName ,lockerId, transactionId, null, string.Empty);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyCaptureType), errorResult.Message);
        }

        [Fact]
        public void CaptureImageInvalidCaptureType()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.CaptureImage, connectionString, databaseName, collectionName ,lockerId, transactionId, null, "Hasan");
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.InvalidCaptureType), errorResult.Message);
        }

        [Fact]
        public void CaptureImageValidCaptureType()
        {
            var (statusCode, _, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.CaptureImage, connectionString, databaseName, collectionName ,lockerId, transactionId, null, captureType);
            Assert.Equal(200, statusCode);
        }

        [Fact]
        public void CaptureImageEmptyTransactionId()
        {
            var (_, errorResult, _) = LockerManagementValidator.PayloadValidator(true, jwtSecret, token, PayloadTypes.CaptureImage, connectionString, databaseName, collectionName ,lockerId, string.Empty, null, captureType);
            Assert.Equal(ApplicationErrorCodes.GetMessage(ApplicationErrorCodes.EmptyTransactionId), errorResult.Message);
        }
        #endregion
    }
}
