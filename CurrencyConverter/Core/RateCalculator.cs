using CurrencyConverter.DTO;
using System;
using System.Globalization;
using System.Linq;
using static System.Double;

namespace CurrencyConverter.Core
{
    public class RateCalculator : ICalculator
    {
        private readonly DailyRates _rates;
        public RateCalculator(DailyRates rates)
        {
            _rates = rates;
        }

        public CurrencyRate CalculateRateFromCurrencies(Currency from, Currency to)
        {

            var rateFrom = _rates.CurrenciesRates.FirstOrDefault(x => x.CurrencyCode == from.Code)!.CurrencyRate;
            var rateTo = _rates.CurrenciesRates.FirstOrDefault(x => x.CurrencyCode == to.Code)!.CurrencyRate;
            if (rateFrom == 0)
            {
                throw new Exception("Cannot divide by zero");
            }
            var rate = rateTo / rateFrom;
            return new CurrencyRate(from, to, rate);
        }

        public string GetChangeValue(CurrencyRate rate, string value)
        {
            if (!TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var convValue))
            {
                throw new Exception("Input value has some bad characters");
            }
            var newValue = convValue * rate.Rate;
            return newValue.ToString(CultureInfo.InvariantCulture);

        }


    }
}
