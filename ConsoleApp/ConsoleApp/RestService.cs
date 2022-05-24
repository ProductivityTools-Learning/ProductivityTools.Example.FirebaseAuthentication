using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class RestService
    {
        public static async Task<string> GetResource(string access_token)
        {
            var httpClient = new HttpClient();
            Uri url = new Uri(@"http://127.0.0.1:8080/ProtectedDate");

            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", access_token);

            HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var resultAsString = await response.Content.ReadAsStringAsync();
                return resultAsString;
            }
            throw new Exception(response.ReasonPhrase);
        }
    }
}
