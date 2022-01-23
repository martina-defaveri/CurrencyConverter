using CurrencyConverter.DTO;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace CurrencyConverter.Api
{
    public static class ParseResponses
    {
        public static IReadOnlyCollection<Currency> ParseCurrencies(string data)
        {
            var currenciesList = new List<Currency>();
            try
            {
                var root = JObject.Parse(data);
                var currencies = root.Value<JObject>("symbols");
                foreach (var (key, value) in currencies)
                {
                    currenciesList.Add(new Currency(value.ToString(), key));
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error on parsing currency definition {e.Message}");
            }
            return currenciesList;
        }

        public static DailyRates ParseRates(string data)
        {
            var currenciesList = new List<Rate>();
            DateTime date;
            try
            {
                var root = JObject.Parse(data);
                var rates = root.Value<JObject>("rates");
                date = root.Value<DateTime>("date");
                foreach (var (key, value) in rates)
                {
                    double.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var rate);
                    currenciesList.Add(new Rate(key, rate));
                }

            }
            catch (Exception e)
            {
                throw new Exception($"Error on parsing rates {e.Message}");
            }

            return new DailyRates(currenciesList, date);
        }
    }
}
