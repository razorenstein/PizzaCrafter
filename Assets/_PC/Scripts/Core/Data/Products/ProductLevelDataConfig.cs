using Assets._PC.Scripts.Core.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Products
{
    public class ProductLevelDataConfig
    {
        public ProductType Type { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public string SpriteAddressableKey { get; set; }
    }
}
