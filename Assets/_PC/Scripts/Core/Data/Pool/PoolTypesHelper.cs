using Assets._PC.Scripts.Core.Data.Enums;
using Codice.Client.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Pool
{
    public class PoolTypesHelper
    {
        public static string Map(PoolType type)
        {
            return type switch
            {
                PoolType.IngredientTile => "IngredientTile",
                PoolType.ResourceTile => "ResourceTile",
                PoolType.OvenTile => "OvenTile",
                PoolType.Cell => "Cell",
                _ => throw new NotImplementedException("No mapping between pool class type and adressable name")
            };  
        }

        public static TileType MapToTileType(PoolType type)
        {
            return type switch
            {
                PoolType.IngredientTile => TileType.Ingredient,
                PoolType.OvenTile => TileType.Oven,
                PoolType.ResourceTile => TileType.Resource,             
                _ => throw new NotImplementedException("No mapping between pool class type and adressable name")
            };
        }
    }
}
