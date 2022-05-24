using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class AccessToken
    {
        public string kind { get; set; }
        public string idToken { get; set; }
        public string refreshToken { get; set; }
        public string exiresIn { get; set; }

        public bool isNewUser { get; set; }

        public static async Task<string> GetAccesToken(string custom_token)
        {
            var HttpClient = new HttpClient();
            Uri url = new Uri(@"https://identitytoolkit.googleapis.com/v1/accounts:signInWithCustomToken?key=AIzaSyDgSHqUdtL0XQ1i95Y_fMKTHV48Yjo_ZWs");

            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            object obj = new { token = custom_token, returnSecureToken = true };
            var dataAsString = JsonConvert.SerializeObject(obj);
            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await HttpClient.PostAsync(url, content);
            if (response.IsSuccessStatusCode)
            {
                var resultAsString = await response.Content.ReadAsStringAsync();
                AccessToken result = JsonConvert.DeserializeObject<AccessToken>(resultAsString);
                return result.idToken;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}
