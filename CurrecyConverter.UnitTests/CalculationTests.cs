using CurrencyConverter.Core;
using CurrencyConverter.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using Xunit;

namespace CurrencyConverter.UnitTests
{
    public class CalculationTests
    {
        [Theory]
        [InlineData(10.65, 11.21)]
        [InlineData(1.0, 11.7)]
        [InlineData(1.7, 0.0)]
        public void CalculatingRateFromCurrencies(double rate1, double rate2)
        {
            var date = DateTime.Parse("2022-01-23");
            var rates = new DailyRates(new List<Rate>() { new Rate("TST", rate1), new Rate("TEST", rate2) },
                date);
            var calculator = new RateCalculator(rates);
            var result = calculator.CalculateRateFromCurrencies(new Currency("Test1", "TST"), new Currency("Test2", "TEST"));
            Assert.Equal((rate2 / rate1), result.Rate);
        }

        [Theory]
        [InlineData(0, 0.85)]
        [InlineData(0.0, 7)]
        public void ExceptionOnCalculatingRateFromCurrencies(double rate1, double rate2)
        {
            var date = DateTime.Parse("2022-01-23");
            var rates = new DailyRates(new List<Rate>() { new Rate("TST", rate1), new Rate("TEST", rate2) },
                date);
            var calculator = new RateCalculator(rates);
            Assert.Throws<Exception>(() => calculator.CalculateRateFromCurrencies(new Currency("Test1", "TST"), new Currency("Test2", "TEST")));
        }

        [Theory]
        [InlineData(10.65, 11.21, "7.22")]
        [InlineData(1.0, 11.7, "11.5")]
        [InlineData(1.0, 11.7, "11.")]
        [InlineData(1.0, 11.7, "2")]
        [InlineData(1.0, 11.7, "11e-5")]
        public void CalculatingChangeValue(double rate1, double rate2, string value)
        {
            var date = DateTime.Parse("2022-01-23");
            var rates = new DailyRates(new List<Rate>() { new Rate("TST", rate1), new Rate("TEST", rate2) },
                date);
            var calculator = new RateCalculator(rates);
            var rate = new CurrencyRate(new Currency("Test1", "TST"), new Currency("Test2", "TEST"), rate1);
            var result = calculator.GetChangeValue(rate, value);
            Assert.Equal((rate.Rate * Double.Parse(value, NumberStyles.Any)).ToString(CultureInfo.InvariantCulture), result);
        }

        [Theory]
        [InlineData(10.65, 11.21, "7.22aadfdd")]
        [InlineData(1.0, 11.7, "2!?&")]
        [InlineData(1.0, 11.7, "")]
        [InlineData(1.0, 11.7, null)]
        public void ExceptionOnCalculatingChangeValue(double rate1, double rate2, string value)
        {
            var date = DateTime.Parse("2022-01-23");
            var rates = new DailyRates(new List<Rate>() { new Rate("TST", rate1), new Rate("TEST", rate2) },
                date);
            var calculator = new RateCalculator(rates);
            var rate = new CurrencyRate(new Currency("Test1", "TST"), new Currency("Test2", "TEST"), rate1);
            Assert.Throws<Exception>(() => calculator.GetChangeValue(rate, value));
        }
    }
}
