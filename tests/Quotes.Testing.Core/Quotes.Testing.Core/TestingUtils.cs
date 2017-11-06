using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Quotes.Testing.Core
{
    public class TestingUtils
    {
        public static AuthenticationHeaderValue GetTokenForTestingUser(HttpClient client)
        {
            var form = new Dictionary<string, string>
            {
                { "username", TestingConstants.UserName },
                { "password", TestingConstants.UserPassword }
            };
            var content = new FormUrlEncodedContent(form);
            var result = client.PostAsync("/api/token", content).Result;

            var json = result.Content.ReadAsStringAsync().Result;
            var jObject = JObject.Parse(json);
            var accessToken = jObject.GetValue("access_token").ToString();
            var authenticationHeaderValue = new AuthenticationHeaderValue("Bearer", accessToken);
            return authenticationHeaderValue;
        }
    }
}