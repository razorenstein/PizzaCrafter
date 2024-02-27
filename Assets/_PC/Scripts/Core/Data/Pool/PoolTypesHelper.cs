using Assets._PC.Scripts.Core.Data.Enums;
using System;

namespace Assets._PC.Scripts.Core.Data.Pool
{
    public class PoolTypesHelper
    {
        public static string Map(PoolType type)
        {
            return type switch
            {
                PoolType.ResourceTile => "ResourceTile",
                PoolType.IngredientTile => "IngredientTile",
                PoolType.ProductTile => "ProductTile",
                PoolType.OvenTile => "OvenTile",
                PoolType.Cell => "Cell",
                PoolType.Order => "Order",
                PoolType.OrderInstructionsItem => "OrderInstructionsItem",
                _ => throw new NotImplementedException("No mapping between pool class type and adressable name")
            };
        }

        public static TileType MapToTileType(PoolType type)
        {
            return type switch
            {
                PoolType.ResourceTile => TileType.Resource,
                PoolType.IngredientTile => TileType.Ingredient,
                PoolType.ProductTile => TileType.Product,
                PoolType.OvenTile => TileType.Oven,
                _ => TileType.None
            };
        }
    }
}
