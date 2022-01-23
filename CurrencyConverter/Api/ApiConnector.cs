using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace CurrencyConverter.Api
{
    public class ApiConnector : IConnector
    {
        public async Task<string> ConnectorAsync(string url)
        {

            using var client = new HttpClient();
            var response = new HttpResponseMessage();
            try
            {
                response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
            }
            catch (Exception)
            {
                throw new Exception(
                    "Something went wrong on retrieving data from API, please check configuration or API disponibility");
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}