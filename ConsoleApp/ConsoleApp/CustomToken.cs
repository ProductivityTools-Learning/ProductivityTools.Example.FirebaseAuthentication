using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class CustomToken
    {

        public static async Task<string> GetCustomToken()
        {

            var httpclient = new HttpClient();
            Uri url = new Uri(@"http://127.0.0.1:8080/Token");

            httpclient.DefaultRequestHeaders.Accept.Clear();
            httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await httpclient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var resultAsString = await response.Content.ReadAsStringAsync();
                // T result = JsonConvert.DeserializeObject<T>(resultAsString);
                return resultAsString;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}
