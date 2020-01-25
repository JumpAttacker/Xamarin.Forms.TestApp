using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Forms.Api
{
    // ReSharper disable once InconsistentNaming
    public static class RestAPI
    {
        public static HttpClient Client = new HttpClient();

        public static bool Init()
        {
            try
            {
                Client.BaseAddress = new Uri("http://192.168.1.5:8080/");
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static async Task<RegistrationObject> LoginAsync(string login, string password)
        {
            var nvc = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Login", login),
                new KeyValuePair<string, string>("Password", password)
            };
            var res = await Client.PostAsync("api/login", new FormUrlEncodedContent(nvc));
            if (!res.IsSuccessStatusCode)
                return new RegistrationObject() { ErrorLevel = -1, Message = "error in connection" };
            var result = await res.Content.ReadAsStringAsync();
            var test = JsonConvert.DeserializeObject<RegistrationObject>(result);
            return test;
        }
        
        public static async Task<RegistrationObject> CreateAccountAsync(List<KeyValuePair<string, string>> nvc)
        {
            var res = await Client.PostAsync("api/registration", new FormUrlEncodedContent(nvc));
            if (!res.IsSuccessStatusCode)
                return new RegistrationObject() { ErrorLevel = -1, Message = "error in connection" };
            var result = await res.Content.ReadAsStringAsync();
            var test = JsonConvert.DeserializeObject<RegistrationObject>(result);
            return test;

        }

        public static async Task<RegistrationObject> LoginAsyncWithToken(object token)
        {
            var nvc = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Token", token.ToString()),
            };
            var res = await Client.PostAsync("api/login", new FormUrlEncodedContent(nvc));
            if (!res.IsSuccessStatusCode)
                return new RegistrationObject() { ErrorLevel = -1, Message = "error in connection" };
            var result = await res.Content.ReadAsStringAsync();
            var test = JsonConvert.DeserializeObject<RegistrationObject>(result);
            return test;
        }
    }
}