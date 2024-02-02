using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;
using Assets._PC.Scripts.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets._PC.Scripts.Core.Data.Products;
using Assets._PC.Scripts.Core.Data.Enums;

namespace Assets._PC.Scripts.Core.Managers
{
    public class ProductManager
    {
        private ProductsConfig _config;

        public ProductManager()
        {
            _config = new ProductsConfig();
            PCManager.Instance.ConfigurationManager.GetConfig<ProductsConfig>(OnConfigLoaded);
        }

        public bool TryGetProduct(ProductType type, int level, out ProductData productData)
        {
            productData = null;
            if (_config.ProductLevels.TryGetValue(type, out var productDataConfig))
            {
                if (productDataConfig.LevelData.TryGetValue(level, out var levelDataConfig))
                {
                    productData = new ProductData(levelDataConfig);
                    return true;
                }
            }

            return false;
        }

        public bool IsMaxLevel(IngredientData ingredientData) => ingredientData.Level == ingredientData.MaxLevel;
        private void OnConfigLoaded(ProductsConfig config) => _config = config;

    }
}
