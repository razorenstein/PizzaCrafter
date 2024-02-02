using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PlasticPipe.Client.InvokeMethodRetry;

namespace Assets._PC.Scripts.Core.Data.Ingredients.Mappers
{
    public static class IngredientMapper
    {
        public static IngredientLevelData Map(this IngredientData source)
        {
            if (source == null)
                return null;
            
            return new IngredientLevelData
            {
                Type = source.IngredientType,
                Level = source.Level,
                MaxLevel = source.MaxLevel,
                SpriteAddressableKey = source.SpriteAddressableKey
            };
        }
    }
}
