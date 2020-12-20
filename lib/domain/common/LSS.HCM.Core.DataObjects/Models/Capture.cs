using LSS.HCM.Core.DataObjects.Settings;
using LSS.HCM.Core.Entities.Locker;
using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Models
{
    public class Capture
    {
        public Capture()
        {
            TransactionId = string.Empty;
            LockerId = string.Empty;
            Image = new Image();
        }
        public Capture(string transactionId, string lockerId, Image captureImage, JsonWebTokens jwtCredentials)
        {
            TransactionId = transactionId;
            LockerId = lockerId;
            Image = captureImage;
            JwtCredentials = jwtCredentials;
        }
        public Capture(string transactionId, string lockerId, string imageExtension, byte[] imageData, bool jwtEnabled, string jwtSecret, string jwtToken)
        {
            TransactionId = transactionId;
            LockerId = lockerId;
            Image = new Image(imageExtension, imageData);
            JwtCredentials = new JsonWebTokens(jwtEnabled, jwtSecret, jwtToken); ;
        }

        public string TransactionId { get; set; }
        public string LockerId { get; set; }
        public Image Image { get; set; }
        public JsonWebTokens JwtCredentials { get; set; }
    }
}
