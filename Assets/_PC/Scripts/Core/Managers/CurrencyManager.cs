using Assets._PC.Scripts.Core.Data.Currency;
using Assets._PC.Scripts.Core.Data.Events;

namespace Assets._PC.Scripts.Core.Managers
{
    public class CurrencyManager
    {
        private CurrencyData _currencies;

        public CurrencyManager()
        {
            _currencies = new CurrencyData();
            _currencies.Data.Add(CurrencyType.Coin, 0);
        }

        public int GetCurrencyAmount(CurrencyType currencyType)
        {
            return _currencies.Data[currencyType];
        }

        public void AddCurrencies(CurrencyType currencyType, int amount)
        {
            if (!_currencies.Data.TryGetValue(currencyType, out var currencyAmount))
            {
                _currencies.Data.Add(currencyType, 0);
            }

            _currencies.Data[currencyType] += amount;
            PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnCurrenciesUpdated,
                new CurrenciesUpdatedEventData
                {
                    CurrencyType = currencyType
                });
        }

        public void RemoveCurrencies(CurrencyType currencyType, int amount)
        {
            if (_currencies.Data.TryGetValue(currencyType, out var currencyAmount))
            {
                _currencies.Data[currencyType] -= amount;
                PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnCurrenciesUpdated,
                    new CurrenciesUpdatedEventData
                    {
                        CurrencyType = currencyType
                    });
                //_currencies.SaveData();
            }
        }

        //private void LoadCurrencyData()
        //{
        //    _currencies = MFManager.Instance.SaveManager.LoadData<PlayerCurrencyData>();
        //    if (PlayerCurrencies == null)
        //    {
        //        PlayerCurrencies = new PlayerCurrencyData();
        //        PlayerCurrencies.Data.Add(CurrencyType.Coin, 0);
        //        MFManager.Instance.SaveManager.SaveData(PlayerCurrencies);
        //    }
        //}
    }
}
