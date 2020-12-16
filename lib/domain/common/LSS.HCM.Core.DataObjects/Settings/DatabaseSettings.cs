using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    /// <summary>
    ///   Represents settings as a sequence of Database Settings units.
    ///</summary>
    [Serializable]
    public class DatabaseSettings
    {
        /// <summary>
        ///     Initializes a new instance of the Database Settings class to the value indicated
        ///     by all members.
        /// </summary>
        public DatabaseSettings() 
        {
            CollectionName = string.Empty;
            ConnectionString = string.Empty;
            DatabaseName = string.Empty;
        }

        /// <summary>
        ///    Initializes a new instance of the Compartment class to the value indicated by 
        ///    all members.
        /// </summary>
        /// Parameters.
        /// <param name="connectionString"> MongoDB connection string</param>
        /// <param name="databaseName">MongoDB database name where specific collection is available.</param>
        /// <param name="collectionName">MongoDB collection name.</param>
        public DatabaseSettings(string connectionString, string databaseName, string collectionName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
            CollectionName = collectionName;
        }

        /// <summary>
        ///     Gets and sets the Database Credentials collection name in the current Database Settings object.
        /// </summary>
        /// <returns>
        ///    The Database Credentials Collection Name in the current Database Settings.
        ///</returns>
        public string CollectionName { get; set; }

        /// <summary>
        ///     Gets and sets the Data BaseCredentials connection string name in the current Database Settings object.
        /// </summary>
        /// <returns>
        ///    The Database Credentials Connection String in the current Database Settings.
        ///</returns>
        public string ConnectionString { get; set; }

        /// <summary>
        ///     Gets and sets the Database Credentials Database Name in the current Database Settings object.
        /// </summary>
        /// <returns>
        ///    The Database Credentials DatabaseName in the current Database Settings.
        ///</returns>
        public string DatabaseName { get; set; }
    }
}
