//using Assets._PC.Scripts.Core.Components;
//using Assets._PC.Scripts.Core.Data.Enums;
//using Assets._PC.Scripts.Core.Data.Orders;
//using System.Collections.Generic;

//namespace Assets._PC.Scripts.Gameplay.Componenets.Resources
//{
//    public class ResourcesUIManager : PCMonoBehaviour
//    {
//        private List<OrderComponent> _ordersState;

//        private void Start()
//        {
//            _ordersState = new List<ResourceTileView>();
//            Initialize();
//        }

//        private void Initialize()
//        {
//            Manager.PoolManager.InitPool<OrderComponent>(PoolType.Order, 10);
//            RegisterEventListeners();
//        }

//        private async void SpawnOrder(OrderData order)
//        {
//            var orderComponent = Manager.PoolManager.GetFromPool<OrderComponent>(PoolType.Order);
//            await orderComponent.Initialize(order);
//            _ordersState.Add(orderComponent);
//            orderComponent.transform.SetParent(this.transform, false);
//            orderComponent.gameObject.SetActive(true);
//            orderComponent.StartTimer();
//        }
//    }
//}
