using Assets._PC.Scripts.Core.Data.Config;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Orders
{
    public class OrdersConfig : BaseConfig
    {
        public List<OrderData> Orders { get; set; }
    }
}
