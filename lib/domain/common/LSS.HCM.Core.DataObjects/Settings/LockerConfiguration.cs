using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace LSS.HCM.Core.DataObjects.Settings
{
    public class LockerConfiguration
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string LockerId { get; set; }
        public List<Compartment> Compartments { get; set; }
    }
}
