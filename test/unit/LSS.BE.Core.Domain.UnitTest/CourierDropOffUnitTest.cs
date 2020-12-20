using LSS.BE.Core.Entities.Courier;
using LSS.BE.Core.Security.Handlers;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace LSS.BE.Core.Domain.UnitTest
{
    public class CourierDropOffUnitTest
    {
        public readonly string clientId = "d5cd3087-749c-4efe-baf7-b38126a69b8a";
        public readonly string clientSecret = "sgKEx9BXzV76oSXLt7NEv4HOF4g594IzYW9ffNU3";
        public readonly string uriString = "http://18.138.61.187";
        public readonly string version = "v1";
        public readonly string type = "Bearer";
        public readonly string uriPath = "otp/verify";
        public readonly string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiJ9.eyJhdWQiOiIyZDUyODFjYy1mMTUwLTRhNGUtOTA4My04OGZlZjk3ZThlYjAiLCJqdGkiOiJlODZhYzE1Mzg3NzgyMDYxNjE5Njc5NTI2NjZmMmFkM2U0ZmZmZGQyNDc3MWI0YmNlMGFjZjgxMmI5YTE1NDk1ZWM4NzU3NWUwMzUzNTdlNSIsImlhdCI6MTYwNTY3MTU5NiwibmJmIjoxNjA1NjcxNTk2LCJleHAiOjE2MDU2NzUxOTYsInN1YiI6IiIsInNjb3BlcyI6WyIqIl19.jwM9J6DCWpZir3mqiGmUpF2MkHYku7ahXua4QS5Qh2RAMkzPe9tvv4RYqrTkVV_5I1NsKX1pbk4s8WNW6BMgqM9SGuONIQOXtGVP3AXI1oY5xneaBvVkRGjWkePWdHtzcMy5zaLv7uUSpz0qQaJUbz5v7u-rTpVTU48hZsrxqDS1Gkz0MJkwBjTQwORfoH9PJj4qn6sQOeP950yMCvYULGDB0b6aFPCui83vkKGobzqqJaD5mH1luis6NeEEhSptEivZfg3SyILmZOMpY1nipSU2KdYCe7zkUC6914qnO3gCMgavg6bkWzQmSoBMhFmnzqBjfaW6C3kj1ky7L-015-asXdiW---n2Foi501S1xH8ENDMh_O6gOErB1aCIQ3lrrB6hDuyHEFmObfpbMAlWeFW1IrEBjl6vFbbX9TuAR_X_W6vvOt5UdJqXBPo4TkqfyQ0jgt5B12gg2WDLDSmhwZDDzYCKo9TPUqgGv7JiHyy0g0wpShpas5hDwFCHjLi2YR2n7c_sV4YaiTrXfUS72CWbyQc5Erf2IiW0nAfXIfl9dTMNPkMHmF51X2GoNLqQNwo5qP5Jm1MfF70ivohiEj1dJNQYGPDv_-Jjby_4RKrUmdcq1CSiYW-ZfT1faDZccfh2uDtvpzT_zNWwlTMGFZB2pdh-1ArIATz95_X9vY";

        [Fact]
        public void VerifyOtp()
        {
            var request = new VerifyOtp
            {
                PhoneNumber = "+6597398077",
                Code = "321252",
                LspId = "0add4ba4-2e62-417b-984f-183f3d11baf7",
                RefCode = "AOAX",
                LockerStationid = "87471edc-37d6-41ef-8521-b96116e707a5"
            };

            var json = JsonConvert.SerializeObject(request, new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore });
            var response = HttpHandler.PostAsync(json, uriString, version, uriPath, type, token);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var applicationError = JsonConvert.DeserializeObject<Common.Exceptions.ApplicationError>(content);
            }
        }
    }
}
