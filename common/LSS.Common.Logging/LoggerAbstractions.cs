using LSS.BE.Core.Common.Base;
using LSS.HCM.Core.DataObjects.Settings;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSS.Common.Logging
{
    public class LoggerAbstractions
    {
        public static void SetupStaticLogger(MemberInfo memberInfo) 
        {
            var lockerConfiguration = GetConfiguration(memberInfo.ConfigurationPath);
            if (lockerConfiguration != null)
            {
                Enum.TryParse(lockerConfiguration.Logger.MinimumLevel, out LogEventLevel RestrictedToMinimumLevel);
                Enum.TryParse(lockerConfiguration.Logger.RollingInterval, out RollingInterval RollingInterval);

                Log.Logger = new LoggerConfiguration().WriteTo.File(lockerConfiguration.Logger.Path, rollingInterval: RollingInterval, restrictedToMinimumLevel: RestrictedToMinimumLevel).CreateLogger();
                Log.Information("Logger setup is initialized.");
            }

        }

        /// <summary>
        /// Check Valid Path
        /// </summary>
        /// <returns>
        ///  True if the caller has the required permissions and path contains the name of an existing file; otherwise, false.
        /// </returns>
        private static bool IsValidPath(string path)
        {
            bool exists = File.Exists(path);
            return exists;
        }

        /// <summary>
        /// Check Valid Path
        /// </summary>
        /// <returns>
        ///  True if the caller has the required permissions and path contains the name of an existing file; otherwise, false.
        /// </returns>
        public static AppSettings GetConfiguration(string path)
        {
            AppSettings configuration = null;
            if (IsValidPath(path))
            {
                var content = File.ReadAllText(path);
                if (string.IsNullOrEmpty(content)) return configuration;
                configuration = JsonConvert.DeserializeObject<AppSettings>(content);
            }
            return configuration;
        }
    }
}