namespace CurrencyConverter.DTO
{
    public class Rate
    {
        public string CurrencyCode { get; }
        public double CurrencyRate { get; }

        public Rate(string currencyCode, double rate)
        {
            CurrencyCode = currencyCode;
            CurrencyRate = rate;
        }
    }
}
