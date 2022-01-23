namespace CurrencyConverter.DTO
{
    public class Currency
    {
        public string Name { get; }
        public string Code { get; }

        public Currency(string name, string code)
        {
            Name = name;
            Code = code;
        }
    }
}
