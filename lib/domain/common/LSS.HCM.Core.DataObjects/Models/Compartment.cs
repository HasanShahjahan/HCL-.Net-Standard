using LSS.HCM.Core.DataObjects.Settings;

namespace LSS.HCM.Core.DataObjects.Models
{
    /// <summary>
    ///   Represents compartment as a sequence of compartment units.
    ///</summary>
    public class Compartment
    {

        /// <summary>
        ///     Initializes a new instance of the Compartment class to the value indicated
        ///     by all members.
        /// </summary>
        public Compartment() 
        {
            TransactionId = string.Empty;
            LockerId = string.Empty;
            CompartmentIds = null;
            JwtCredentials = new JsonWebTokens();
            DataBaseCredentials = new DatabaseSettings();
        }

        /// <summary>
        ///    Initializes a new instance of the Compartment class to the value indicated by 
        ///    all members.
        /// </summary>
        /// Parameters.
        /// <param name="transactionId"> An request Transaction Id.</param>
        /// <param name="lockerId">An request Locker Id where compartment will be opened.</param>
        /// <param name="compartmentIds"> List of compartment id's which will be opened.</param>
        /// <param name="jwtEnabled">Jsone Web Token Flag.By default will be false, If you want to enable then specify this flag true.</param>
        /// <param name="jwtSecret">Shared secret between middleware and Hardware Control Module, where HCM will decode JWT using provided secret key.</param>
        /// <param name="jwtToken">Json web token with specified format which will contains JOSE Header Algoritm(HMAC SHA256 (Base64Url)),JWS Payload (Identity (Must Include Transaction Id))</param>
        /// <param name="connectionString"> MongoDB connection string</param>
        /// <param name="databaseName">MongoDB database name where specific collection is available.</param>
        /// <param name="collectionName">MongoDB collection name.</param>
        public Compartment(string transactionId, string lockerId, string[] compartmentIds, bool jwtEnabled, string jwtSecret, string jwtToken, string connectionString, string databaseName, string collectionName)
        {
            TransactionId = transactionId;
            LockerId = lockerId;
            CompartmentIds = compartmentIds;
            JwtCredentials = new JsonWebTokens(jwtEnabled, jwtSecret, jwtToken);
            DataBaseCredentials = new DatabaseSettings(connectionString, databaseName, collectionName);
        }


        /// <summary>
        ///    Initializes a new instance of the Compartment class to the value indicated by all members.
        /// </summary>
        /// Parameters.
        /// <param name="transactionId"> An request Transaction Id.</param>
        /// <param name="lockerId">An request Locker Id where compartment will be opened.</param>
        /// <param name="compartmentIds"> List of compartment id's which will be opened.</param>
        /// <param name="jwtCredentials">Jsone Web Token credentials receive Flag, shared key and Token.</param>
        /// <param name="dataBaseCredentials">Database credentials receive connection string name, database name and collection name of MongoDB.</param>
        public Compartment(string transactionId, string lockerId, string[] compartmentIds, JsonWebTokens jwtCredentials, DatabaseSettings dataBaseCredentials)
        {
            TransactionId = transactionId;
            LockerId = lockerId;
            CompartmentIds = compartmentIds;
            JwtCredentials = jwtCredentials;
            DataBaseCredentials = dataBaseCredentials;
        }


        /// <summary>
        ///     Gets and sets the transaction Id in the current Compartment object.
        /// </summary>
        /// <returns>
        ///     The Transaction Id in the current Compartment.
        ///</returns>
        public string TransactionId { get; set; }


        /// <summary>
        ///     Gets and sets the Locker Id in the current Compartment object.
        /// </summary>
        /// <returns>
        ///     The Locker Id in the current Compartment.
        ///</returns>
        public string LockerId { get; set; }


        /// <summary>
        ///     Gets and sets the list of Compartment Id in the current Compartment object.
        /// </summary>
        /// <returns>
        ///     The Compartment Ids in the current Compartment.
        ///</returns>
        public string[] CompartmentIds { get; set; }


        /// <summary>
        ///     Gets and sets the Jwt Credentials in the current Compartment object.
        /// </summary>
        /// <returns>
        ///     The Jwt Credentials in the current Compartment.
        ///</returns>
        public JsonWebTokens JwtCredentials { get; set; }


        /// <summary>
        ///     Gets and sets the Data BaseCredentials in the current Compartment object.
        /// </summary>
        /// <returns>
        ///    The Data BaseCredentials in the current Compartment.
        ///</returns>
        public DatabaseSettings DataBaseCredentials { get; set; }
    }
}
