﻿using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Orders;
using Assets._PC.Scripts.Core.Data.Oven;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class OrderComponent : PCMonoBehaviour, IPointerClickHandler
    {
        public OrderData Data { get; private set; }
        [SerializeField]
        private Image _orderProductImage;
        [SerializeField]
        private Color _fulfilledBGColor;
        [SerializeField]
        private Color _unfulfilledBGColor;
        [SerializeField]
        private Image _orderBGImage;
        [SerializeField]
        private TMP_Text _amountText;
        [SerializeField]
        private TMP_Text _rewardText;

        public async virtual Task Initialize(OrderData data)
        {
            Data = data;
            _orderProductImage.sprite = await AddressablesHelper.TryLoadAddressableAsync(data.ProductData.SpriteAddressableKey);
            _amountText.text = "X" + data.Amount.ToString();
            _rewardText.text = data.Reward.ToString();

            if(data.IsOrderConditionsFulfilled)
                SetFulfilled();
            else
                SetUnFulfilled();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Manager.OrdersManager.TryCompleteOrder(Data.Id);           
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