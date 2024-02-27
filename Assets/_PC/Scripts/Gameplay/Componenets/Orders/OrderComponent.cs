using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Orders;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using Assets._PC.Scripts.Gameplay.Componenets.Orders;
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class OrderComponent : PCMonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public OrderData Data { get; private set; }
        [SerializeField] TimerComponent _timer;
        [SerializeField] private Image _orderProductImage;
        [SerializeField] private Color _fulfilledBGColor;
        [SerializeField] private Color _unfulfilledBGColor;
        [SerializeField] private Image _orderBGImage;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _rewardText;

        public async virtual Task Initialize(OrderData data)
        {
            Data = data;
            _orderProductImage.sprite = await AddressablesHelper.TryLoadAddressableAsync(data.ProductData.SpriteAddressableKey);
            _amountText.text = "X" + data.Amount.ToString();
            _rewardText.text = data.Reward.ToString();

            if (data.IsOrderConditionsFulfilled)
                SetFulfilled();
            else
                SetUnFulfilled();
        }

        public void StartTimer()
        {
            _timer.Initialize(Data.Id, Data.ExpiryDurationSeconds);
        }

        //public void OnPointerClick(PointerEventData eventData)
        //{
        //    Manager.OrdersManager.TryCompleteOrder(Data.Id);
        //}

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Data.IsOrderConditionsFulfilled)
                Manager.OrdersManager.TryCompleteOrder(Data.Id);
            else
                Manager.OrdersManager.TryGetOrderInstructions(Data.Id, out var recipeData);
        }


        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log("Up");
        }

        public void SetFulfilled()
        {
            _orderBGImage.color = _fulfilledBGColor;
        }

        public void SetUnFulfilled()
        {
            _orderBGImage.color = _unfulfilledBGColor;
        }
    }
}
