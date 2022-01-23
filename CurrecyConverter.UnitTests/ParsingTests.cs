using CurrencyConverter.Api;
using System;
using Xunit;

namespace CurrencyConverter.UnitTests
{
    public class ParsingTests
    {
        [Fact]
        public void CorrectParsingCurrency()
        {
            const string json = "{\"success\":true,\"symbols\":{\"AED\":\"United Arab Emirates Dirham\",\"AFN\":\"Afghan Afghani\",\"ALL\":\"Albanian Lek\",\"AMD\":\"Armenian Dram\",\"ANG\":\"Netherlands Antillean Guilder\",\"AOA\":\"Angolan Kwanza\",\"ARS\":\"Argentine Peso\",\"AUD\":\"Australian Dollar\",\"AWG\":\"Aruban Florin\",\"AZN\":\"Azerbaijani Manat\"}}";
            var currencies = ParseResponses.ParseCurrencies(json);

            Assert.Equal(10, currencies.Count);
            Assert.Contains(currencies, x => x.Code == "ALL" && x.Name == "Albanian Lek");
            Assert.Contains(currencies, x => x.Name == "Azerbaijani Manat");
        }

        [Fact]
        public void CorrectParsingCurrencyWithSpecialCharacter()
        {
            const string json = "{\"success\":true,\"symbols\":{\"AED\":\"United Arab Emirates Dirham\",\"AFN\":\"Afghan Afghani\",\"ALL\":\"Albanian & Lek !\",\"AMD\":\"Armenian Dram\",\"ANG\":\"Netherlands Antillean Guilder\",\"AOA\":\"Angolan Kwanza\",\"ARS\":\"Argentine Peso\",\"AUD\":\"Australian Dollar\",\"AWG\":\"Aruban Florin\",\"AZN\":\"Azerbaijani Manat%&%&\"}}";
            var currencies = ParseResponses.ParseCurrencies(json);

            Assert.Equal(10, currencies.Count);
            Assert.Contains(currencies, x => x.Code == "ALL" && x.Name == "Albanian & Lek !");
            Assert.Contains(currencies, x => x.Name == "Azerbaijani Manat%&%&");
        }

        [Theory]
        [InlineData("{\"success\":true,\"symbols\":{\"AED\":\"United Arab Emirates Dirham\" : \"RealError\",\"AFN\":\"Afghan Afghani\",\"ALL\":\"Albanian & Lek !\",\"AMD\":\"Armenian Dram\",\"ANG\":\"Netherlands Antillean Guilder\",\"AOA\":\"Angolan Kwanza\",\"ARS\":\"Argentine Peso\",\"AUD\":\"Australian Dollar\",\"AWG\":\"Aruban Florin\",\"AZN\":\"Azerbaijani Manat%&%&\"}}")]
        [InlineData("")]
        [InlineData("3")]
        public void ThrowIncorrectParsingException(string json)
        {
            Assert.Throws<Exception>(() => ParseResponses.ParseCurrencies(json));
        }

        [Fact]
        public void CorrectParsingRate()
        {
            const string json = "{\"success\":true,\"timestamp\":1642790043,\"base\":\"EUR\",\"date\":\"2022-01-21\",\"rates\":{\"AED\":4.167919,\"AFN\":119.264911,\"ALL\":121.700514,\"AMD\":547.07767,\"ANG\":2.045573,\"AOA\":601.293642,\"ARS\":118.402087,\"AUD\":1.577935,\"AWG\":2.036845,\"AZN\":1.93355,\"BAM\":1.959348,\"BBD\":2.291715,\"BDT\":97.544705,\"BGN\":1.956573,\"BHD\":0.427761,\"BIF\":2281.946974,\"BMD\":1.134732,\"BND\":1.527256,\"BOB\":7.825952,\"BRL\":6.192807,\"BSD\":1.135038}}";
            var rates = ParseResponses.ParseRates(json);

            Assert.Equal(21, rates.CurrenciesRates.Count);
            Assert.Contains(rates.CurrenciesRates, x => x.CurrencyCode == "AED" && x.CurrencyRate == 4.167919);
            Assert.Equal(DateTime.Parse("2022-01-21"), rates.RatesDate);
        }

        [Fact]
        public void CorrectParsingRateWithScientificNotation()
        {
            const string json = "{\"success\":true,\"timestamp\":1642790043,\"base\":\"EUR\",\"date\":\"2022-01-23\",\"rates\":{\"BTC\":2.9617442e-5}}";
            var rates = ParseResponses.ParseRates(json);

            Assert.Equal(1, rates.CurrenciesRates.Count);
            Assert.Contains(rates.CurrenciesRates, x => x.CurrencyCode == "BTC" && x.CurrencyRate == 2.9617442e-5);
            Assert.Equal(DateTime.Parse("2022-01-23"), rates.RatesDate);
        }

        [Theory]
        [InlineData("{\"success\":true,\"timestamp\":1642790043,\"base\":\"EUR\",\"date\":\"2022-01-23\",\"rates\":{\"BTC\":2.9617442lmnopdhjde-5}}")]
        [InlineData("")]
        [InlineData("3")]
        public void ThrowIncorrectParsingRateException(string json)
        {
            Assert.Throws<Exception>(() => ParseResponses.ParseRates(json));
        }


    }
}
