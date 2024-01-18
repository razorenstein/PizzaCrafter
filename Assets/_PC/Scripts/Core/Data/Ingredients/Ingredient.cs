using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Ingredients
{
    public class Ingredient
    {
        public IngredientType Type { get; set; }
        public int Level { get; set; }
        public string SpriteAddressableKey { get; set; }
    }
}
