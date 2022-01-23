using System.Threading.Tasks;

namespace CurrencyConverter.Api
{
    public interface IConnector
    {
        Task<string> ConnectorAsync(string url);
    }
}