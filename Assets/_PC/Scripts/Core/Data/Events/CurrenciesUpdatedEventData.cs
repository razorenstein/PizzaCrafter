using Assets._PC.Scripts.Core.Data.Currency;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class CurrenciesUpdatedEventData : PCBaseEventData
    {
        public CurrencyType CurrencyType { get; set; }
    }
}
