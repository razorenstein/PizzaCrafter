using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;

namespace Assets._PC.Scripts.Core.Data.Products
{
    public class ProductData : TileData
    {
        public ProductType ProductType { get; private set; }
        public int Level { get; private set; }
        public int MaxLevel { get; private set; }

        public ProductData(ProductLevelDataConfig productLevelDataConfig) : base()
        {
            ProductType = productLevelDataConfig.Type;
            Level = productLevelDataConfig.Level;
            MaxLevel = productLevelDataConfig.MaxLevel;
            SpriteAddressableKey = productLevelDataConfig.SpriteAddressableKey;
            Type = TileType.Product;
        }
    }
}
