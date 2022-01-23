using CurrencyConverter.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CurrencyConverter.Api
{
    public interface IDataGateway
    {
        Task<IReadOnlyCollection<Currency>> GetAvailableCurrencies();
        Task<DailyRates> GetRates();
    }
}
