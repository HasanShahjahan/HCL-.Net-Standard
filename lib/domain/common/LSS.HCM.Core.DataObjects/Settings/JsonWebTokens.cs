using System;
using System.Collections.Generic;
using System.Text;

namespace LSS.HCM.Core.DataObjects.Settings
{
    /// <summary>
    ///   Represents JsonWebTokens as a sequence of token units.
    ///</summary>
    public class JsonWebTokens
    {
        /// <summary>
        ///    Initializes a new instance of the Json Web Token class to the value indicated by 
        ///    all members.
        /// </summary>
        public JsonWebTokens()
        {
            IsEnabled = false;
            Secret = string.Empty;
            Token = string.Empty;
        }
        /// <summary>
        ///    Initializes a new instance of the Compartment class to the value indicated by 
        ///    all members.
        /// </summary>
        /// Parameters.
        /// <param name="isEnabled"> Jsone Web Token Flag.By default will be false, If you want to enable then specify this flag true.</param>
        /// <param name="secret">Shared secret between middleware and Hardware Control Module, where HCM will decode JWT using provided secret key.</param>
        /// <param name="token">Json web token with specified format which will contains JOSE Header Algoritm(HMAC SHA256 (Base64Url)),JWS Payload (Identity (Must Include Transaction Id))</param>
        public JsonWebTokens(bool isEnabled, string secret, string token)
        {
            IsEnabled = isEnabled;
            Secret = secret;
            Token = token;
        }

        /// <summary>
        ///     Gets and sets the Jwt Credentials flag in the current JsonWebTokens object.
        /// </summary>
        /// <returns>
        ///     The Jwt Credentials flag in the current JsonWebTokens.
        ///</returns>
        public bool IsEnabled { get; set; }

        /// <summary>
        ///     Gets and sets the Jwt Credentials shared secret in the current JsonWebTokens object.
        /// </summary>
        /// <returns>
        ///     The Jwt Credentials shared secret in the current JsonWebTokens.
        ///</returns>
        public string Secret { get; set; }

        /// <summary>
        ///     Gets and sets the Jwt Credentials token in the current JsonWebTokens object.
        /// </summary>
        /// <returns>
        ///     The Jwt Credentials token in the current JsonWebTokens.
        ///</returns>
        public string Token { get; set; }
    }
}
