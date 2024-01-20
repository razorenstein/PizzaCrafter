using Assets._PC.Scripts.Core.Data.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Ingredients.Config
{
    public class IngredientsDataConfig : BaseConfig
    {
        public Dictionary<IngredientType, IngredientConfig> Ingredients;
    }
}
