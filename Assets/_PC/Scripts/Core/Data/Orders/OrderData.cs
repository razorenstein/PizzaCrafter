using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Products;
using Assets._PC.Scripts.Core.Data.Recipes;
using System;

namespace Assets._PC.Scripts.Core.Data.Orders
{
    public class OrderData
    {
        public Guid Id { get; set; }
        public ProductType ProductType { get; set; }
        public ProductData ProductData { get; set; }
        public int Amount { get; set; }
        public int Reward { get; set; }

        public OrderData()
        {
            Id = Guid.NewGuid();
        }
    }
}
