using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;
using Assets._PC.Scripts.Core.Data.Orders;
using Assets._PC.Scripts.Core.Data.Oven;
using Assets._PC.Scripts.Core.Data.Products;
using Assets._PC.Scripts.Core.Data.Recipes;
using Assets._PC.Scripts.Core.Data.Resources.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Managers
{
    public class OrdersManager
    {
        public Dictionary<Guid, OrderData> Orders { get; private set; }
        private OrdersConfig _config;

        public OrdersManager()
        {
            Orders = new Dictionary<Guid, OrderData>();
            _config = new OrdersConfig();
            PCManager.Instance.ConfigurationManager.GetConfig<OrdersConfig>(OnConfigLoaded);
        }

        public bool TryCompleteOrder(Guid orderId)
        {
            if (Orders.TryGetValue(orderId, out OrderData orderData))
            {
                if (IsOrderSatisfied(orderData, out var products))
                {
                    PCManager.Instance.BoardManager.RemoveTiles(products.Select(t => t as TileData).ToList());
                    Orders.Remove(orderId);

                    PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnOrderCompleted, new OrderCompletedEventData()
                    {
                        OrderId = orderId
                    });

                    return true;
                }
                //get coins
            }

            return false;
        }

        public void CheckForCompletedOrders()
        {
            foreach (var order in Orders)
            {
                if (IsOrderSatisfied(order.Value, out _))
                {
                    PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnOrderConditionsFulfilled, new OrderConditionsFulfilledEventData()
                    {
                        OrderId = order.Key
                    });
                }
            }
        }

        private bool IsOrderSatisfied(OrderData order, out List<ProductData> products)
        {
            products = GetProductsFromBoard(order.ProductType);
            if (products.Count() == order.Amount)
                return true;
            
            return false;
        }

        private List<ProductData> GetProductsFromBoard(ProductType productType)
        {
            var products = new List<ProductData>();
            foreach (var tileData in PCManager.Instance.BoardManager.TilesState)
            {
                if (tileData is ProductData productData && productType == productData.ProductType)
                    products.Add(productData);
            }

            return products;
        }

        private void OnConfigLoaded(OrdersConfig config)
        {
            _config = config;
            foreach (var order in _config.Orders)
            {
                if (PCManager.Instance.ProductManager.TryGetProduct(order.ProductType, 1, out ProductData productData))
                {
                    order.ProductData = productData;
                    Orders.Add(order.Id, order);
                }
            }
        }
    }
}
