using CurrencyConverter.Api;
using CurrencyConverter.Core;
using CurrencyConverter.DTO;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CurrencyConverter
{
    public partial class MainWindow
    {
        private readonly IDataGateway _apiDataGateway;
        private DailyRates _rates;
        private ICalculator _calculator;

        public MainWindow(IDataGateway dataGateway)
        {
            _apiDataGateway = dataGateway;

            InitVariables();

            InitializeComponent();
        }

        private async void InitVariables()
        {
            try
            {
                await GetCurrencies();
                await GetRates();

                _calculator = new RateCalculator(_rates);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task GetCurrencies()
        {
            var listCurrency = await _apiDataGateway.GetAvailableCurrencies();
            FirstCurrency.ItemsSource = listCurrency;
            FirstCurrency.DisplayMemberPath = "Name";
            SecondCurrency.ItemsSource = listCurrency;
            SecondCurrency.DisplayMemberPath = "Name";
            FirstCurrency.SelectedItem = listCurrency.FirstOrDefault();
            SecondCurrency.SelectedItem = listCurrency.FirstOrDefault();
        }

        private async Task GetRates()
        {
            _rates = await _apiDataGateway.GetRates();
        }

        private void FirstComboSelectedChangeEventHandler(object sender, SelectionChangedEventArgs e)
        {
            if (FirstCurrency.SelectionBoxItem.GetType() == typeof(Currency) && SecondCurrency.SelectionBoxItem.GetType() == typeof(Currency))
            {
                SecondValue.Text = GetValueChanged((Currency)e.AddedItems[0], (Currency)SecondCurrency.SelectionBoxItem, FirstValue.Text);
            }
        }

        private void SecondComboChangedEventHandler(object sender, SelectionChangedEventArgs e)
        {
            if (FirstCurrency.SelectionBoxItem.GetType() == typeof(Currency) && SecondCurrency.SelectionBoxItem.GetType() == typeof(Currency))
            {
                FirstValue.Text = GetValueChanged((Currency)e.AddedItems[0], (Currency)FirstCurrency.SelectionBoxItem, SecondValue.Text);
            }
        }


        private void FirstTextChangedEventHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (FirstCurrency.SelectionBoxItem.GetType() == typeof(Currency) && SecondCurrency.SelectionBoxItem.GetType() == typeof(Currency))
            {
                SecondValue.Text = GetValueChanged((Currency)FirstCurrency.SelectionBoxItem, (Currency)SecondCurrency.SelectionBoxItem, FirstValue.Text);
            }
        }

        private void SecondTextChangedEventHandler(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (FirstCurrency.SelectionBoxItem.GetType() == typeof(Currency) && SecondCurrency.SelectionBoxItem.GetType() == typeof(Currency))
            {
                FirstValue.Text = GetValueChanged((Currency)SecondCurrency.SelectionBoxItem, (Currency)FirstCurrency.SelectionBoxItem, SecondValue.Text);
            }
        }

        private string GetValueChanged(Currency from, Currency to, string value)
        {
            var rate = _calculator.CalculateRateFromCurrencies(from, to);
            string valueTo = string.Empty;
            try
            {
                valueTo = _calculator.GetChangeValue(rate, value);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SetLabelsValue(from, to, value, valueTo);
            return valueTo;
        }

        private void SetLabelsValue(Currency from, Currency to, string valueFrom, string valueTo)
        {
            FromValues.Content = $"{valueFrom} {from.Name} is equals to";
            ToValues.Content = $"{valueTo} {to.Name}";
        }
    }
}
