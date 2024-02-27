using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Orders;
using Assets._PC.Scripts.Gameplay.Componenets.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class OrdersUIManager : PCMonoBehaviour
    {
        [SerializeField] private OrderInstructionPopUpComponent _orderInstructionPopUpComponent;
        private List<OrderComponent> _ordersState;

        private void Start()
        {
            _ordersState = new List<OrderComponent>();
            Initialize();
        }

        private void Initialize()
        {
            Manager.PoolManager.InitPool<OrderComponent>(PoolType.Order, 10);
            Manager.PoolManager.InitPool<OrderInstructionsItemComponent>(PoolType.OrderInstructionsItem, 10);
            //_orderInstructionPopUpComponent.gameObject.SetActive(false);
            RegisterEventListeners();
        }

        private async void SpawnOrder(OrderData order)
        {
            var orderComponent = Manager.PoolManager.GetFromPool<OrderComponent>(PoolType.Order);
            await orderComponent.Initialize(order);
            _ordersState.Add(orderComponent);
            orderComponent.transform.SetParent(this.transform, false);
            orderComponent.gameObject.SetActive(true);
            orderComponent.StartTimer();
        }

        private void RemoveOrder(Guid orderId)
        {
            var orderToRemove = _ordersState.Where(x => x.Data.Id == orderId).FirstOrDefault();
            Manager.PoolManager.ReturnToPool(PoolType.Order, orderToRemove);
            _ordersState.Remove(orderToRemove);
        }

        private void OnOrderConditionsFulfilled(PCBaseEventData baseEventData)
        {
            var eventData = (OrderConditionsFulfilledEventData)baseEventData;
            foreach (var orderComponent in _ordersState) 
            {
                if(orderComponent.Data.IsOrderConditionsFulfilled)
                    orderComponent.SetFulfilled();
                else
                    orderComponent.SetUnFulfilled();
            }
        }

        private void OnPoolReady(PCBaseEventData baseEventData)
        {
            var eventData = (PoolReadyEventData)baseEventData;
            if (eventData.Type == PoolType.Order)
            {
                foreach (var order in Manager.OrdersManager.Orders)
                {
                    SpawnOrder(order.Value);
                }
            }
        }

        private void OnOrderCompleted(PCBaseEventData baseEventData)
        {
            var eventData = (OrderCompletedEventData)baseEventData;
            RemoveOrder(eventData.OrderId);
        }

        private void OnOrderExpired(PCBaseEventData baseEventData)
        {
            var eventData = (OrderExpiredEventData)baseEventData;
            RemoveOrder(eventData.OrderId);
        }

        private async void OnOrderInstructionsPopUpClicked(PCBaseEventData baseEventData)
        {
            var eventData = (OrderInstructionsPopUpClickedEventData)baseEventData;
            await _orderInstructionPopUpComponent.Initialize(eventData.RecipeData);
            _orderInstructionPopUpComponent.gameObject.SetActive(true);
        }

        private void RegisterEventListeners()
        {
            Manager.EventManager.AddListener(PCEventType.OnPoolReady, OnPoolReady);
            Manager.EventManager.AddListener(PCEventType.OnOrderConditionsFulfilled, OnOrderConditionsFulfilled);
            Manager.EventManager.AddListener(PCEventType.OnOrderCompleted, OnOrderCompleted);
            Manager.EventManager.AddListener(PCEventType.OnOrderExpired, OnOrderExpired);
            Manager.EventManager.AddListener(PCEventType.OnOrderInstructionsPopUpClicked, OnOrderInstructionsPopUpClicked);
        }

        private void UnRegisterEventListeners()
        {
            Manager.EventManager.RemoveListener(PCEventType.OnPoolReady, OnPoolReady);
            Manager.EventManager.RemoveListener(PCEventType.OnOrderConditionsFulfilled, OnOrderConditionsFulfilled);
            Manager.EventManager.RemoveListener(PCEventType.OnOrderCompleted, OnOrderCompleted);
            Manager.EventManager.RemoveListener(PCEventType.OnOrderCompleted, OnOrderCompleted);
            Manager.EventManager.RemoveListener(PCEventType.OnOrderInstructionsPopUpClicked, OnOrderInstructionsPopUpClicked);
        }

        private void OnDestroy()
        {
            UnRegisterEventListeners();
        }
    }
}
