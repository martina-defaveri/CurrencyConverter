using CurrencyConverter.DTO;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter.Api
{

    public class ApiDataGateway : IDataGateway
    {
        private readonly string _siteUrl;
        private readonly string _apiKey;
        private readonly ApiConnector _apiConnector;

        public ApiDataGateway(IConfiguration configuration)
        {
            _siteUrl = configuration.GetValue<string>("BaseUrl");
            _apiKey = configuration.GetValue<string>("ApiKey");
            _apiConnector = new ApiConnector();
        }

        public async Task<DailyRates> GetRates()
        {
            var url = $"{_siteUrl}latest?access_key={_apiKey}";
            return ParseResponses.ParseRates(await _apiConnector.ConnectorAsync(url));
        }

        public async Task<IReadOnlyCollection<Currency>> GetAvailableCurrencies()
        {
            var url = $"{_siteUrl}symbols?access_key={_apiKey}";
            return ParseResponses.ParseCurrencies(await _apiConnector.ConnectorAsync(url));
        }
    }
}
