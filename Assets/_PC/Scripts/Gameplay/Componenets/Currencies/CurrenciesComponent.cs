using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Currency;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Gameplay.Componenets.Extensions;
using TMPro;
using UnityEngine;

namespace Assets._PC.Scripts.Gameplay.Componenets.Currencies
{
    public class CurrenciesComponent : PCMonoBehaviour
    {
        [SerializeField]
        private TMP_Text _amountTMPText;

        private void Start()
        {
            RegisterEventListeners();
            UpdateCurrencies();
        }

        private void OnCurrenciesUpdated(PCBaseEventData baseEventData)
        {
            var eventData = (CurrenciesUpdatedEventData)baseEventData;
            if (eventData.CurrencyType == CurrencyType.Coin)
                UpdateCurrencies();
        }

        private void UpdateCurrencies() 
        {
            _amountTMPText.text = Manager.CurrencyManager.GetCurrencyAmount(CurrencyType.Coin).ToKMB();
        }

        private void RegisterEventListeners()
        {
            Manager.EventManager.AddListener(PCEventType.OnCurrenciesUpdated, OnCurrenciesUpdated);
        }

        protected virtual void UnRegisterEventListeners()
        {
            Manager.EventManager.RemoveListener(PCEventType.OnCurrenciesUpdated, OnCurrenciesUpdated);
        }

        private void OnDestroy()
        {
            UnRegisterEventListeners();
        }
    }
}
