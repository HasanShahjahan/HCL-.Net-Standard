using System.Collections.Generic;
using LSS.HCM.Core.Common.Exceptions;
using LSS.HCM.Core.Entities.Locker;

namespace LSS.HCM.Core.DataObjects.Dtos
{
    public class CaptureDto : ApplicationError
    {
        public CaptureDto()
        {
            TransactionId = string.Empty;
            LockerId = string.Empty;
            CaptureImage = new Image();
        }

        public CaptureDto(string transactionId, string lockerId, string imageExtension, byte[] imageData)
        {
            TransactionId = transactionId;
            LockerId = lockerId;
            CaptureImage = new Image(imageExtension, imageData);
        }

        public CaptureDto(string transactionId, string lockerId, Image captureImage) 
        {
            TransactionId = transactionId;
            LockerId = lockerId;
            CaptureImage = captureImage;
        }
        public string TransactionId { get; set; }
        public string LockerId { get; set; }
        public Image CaptureImage { get; set; }

    }
}
