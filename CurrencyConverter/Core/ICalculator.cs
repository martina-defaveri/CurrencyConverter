using CurrencyConverter.DTO;

namespace CurrencyConverter.Core
{
    public interface ICalculator
    {
        CurrencyRate CalculateRateFromCurrencies(Currency from, Currency to);
        string GetChangeValue(CurrencyRate rate, string value);
    }
}