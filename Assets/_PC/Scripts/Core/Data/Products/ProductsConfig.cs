using Assets._PC.Scripts.Core.Data.Config;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Oven;
using Codice.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Products
{
    public class ProductsConfig : BaseConfig
    {
        public Dictionary<ProductType, ProductLevelsConfig> ProductLevels { get; set; }
    }
}
