using System;
using System.Threading.Tasks;
using CurrencyConverter.Api;
using Xunit;

namespace CurrencyConverter.UnitTests
{
    public class ApiConnectorTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("http\\\\:itdoesntexist.it")]
        public async Task ExceptionWhenCantReachApi(string url)
        {
            var sut = new ApiConnector();
            await Assert.ThrowsAsync<Exception>(async () => await sut.ConnectorAsync(url));
        }
    }
}
