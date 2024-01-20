using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Managers;
using Assets._PC.Scripts.Gameplay.Componenets.Spawners.Abstract;
using Assets._PC.Scripts.Gameplay.Components;
using System.Linq;

namespace Assets._PC.Scripts.Gameplay.Componenets.Spawners.Concrete
{
    public class IngredientTileSpawner : TilesSpawnerBase<IngredientTileView>
    {
        public void Initialize(GridView gridView)
        {
            base.Initialize(gridView, TileType.Ingredient, PoolType.IngredientTile, poolSize: PCManager.Instance.BoardManager.GetBoardCellsCount());
        }
    }
}
