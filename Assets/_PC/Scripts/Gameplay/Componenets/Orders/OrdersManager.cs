using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class OrdersManager : PCMonoBehaviour
    {
        private List<OrderComponent> _ordersState;

        private void Start()
        {
            _ordersState = new List<OrderComponent>();
            Initialize();
        }

        private void Initialize()
        {
            Manager.PoolManager.InitPool<OrderComponent>(PoolType.Order, 10);
            RegisterEventListeners();
        }

        private async void SpawnOrder(OrderData order)
        {
            var orderComponent = Manager.PoolManager.GetFromPool<OrderComponent>(PoolType.Order);
            await orderComponent.Initialize(order);
            _ordersState.Add(orderComponent);
            orderComponent.transform.SetParent(this.transform, false);
            orderComponent.gameObject.SetActive(true);
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

        private void RegisterEventListeners()
        {
            Manager.EventManager.AddListener(PCEventType.OnPoolReady, OnPoolReady);
            Manager.EventManager.AddListener(PCEventType.OnOrderConditionsFulfilled, OnOrderConditionsFulfilled);
            Manager.EventManager.AddListener(PCEventType.OnOrderCompleted, OnOrderCompleted);
        }


        private void UnRegisterEventListeners()
        {
            Manager.EventManager.RemoveListener(PCEventType.OnPoolReady, OnPoolReady);
            Manager.EventManager.RemoveListener(PCEventType.OnOrderConditionsFulfilled, OnOrderConditionsFulfilled);
            Manager.EventManager.AddListener(PCEventType.OnOrderCompleted, OnOrderCompleted);
        }

        private void OnDestroy()
        {
            UnRegisterEventListeners();
        }
    }
}
