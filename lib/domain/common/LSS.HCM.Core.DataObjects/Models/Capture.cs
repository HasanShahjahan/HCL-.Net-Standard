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
            JwtCredentials = null;
        }
        public Capture(string transactionId, string lockerId, JsonWebTokens jwtCredentials)
        {
            TransactionId = transactionId;
            LockerId = lockerId;
            JwtCredentials = jwtCredentials;
        }
        public Capture(string transactionId, string lockerId, bool jwtEnabled, string jwtSecret, string jwtToken)
        {
            TransactionId = transactionId;
            LockerId = lockerId;
            JwtCredentials = new JsonWebTokens(jwtEnabled, jwtSecret, jwtToken); ;
        }

        public string TransactionId { get; set; }
        public string LockerId { get; set; }
        public JsonWebTokens JwtCredentials { get; set; }
    }
}
