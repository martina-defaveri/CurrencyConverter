namespace CurrencyConverter.DTO
{
    public class CurrencyRate
    {
        public Currency FromCurrency { get; }
        public Currency ToCurrency { get; }
        public double Rate { get; }


        public CurrencyRate(Currency fromCurrency, Currency toCurrency, double rate)
        {
            FromCurrency = fromCurrency;
            ToCurrency = toCurrency;
            Rate = rate;
        }
    }
}
