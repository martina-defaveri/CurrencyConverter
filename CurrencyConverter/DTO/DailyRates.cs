using System;
using System.Collections.Generic;

namespace CurrencyConverter.DTO
{
    public class DailyRates
    {
        public IReadOnlyCollection<Rate> CurrenciesRates { get; }
        public DateTime RatesDate { get; }

        public DailyRates(IReadOnlyCollection<Rate> currenciesRates, DateTime ratesDate)
        {
            CurrenciesRates = currenciesRates;
            RatesDate = ratesDate;
        }

    }
}
