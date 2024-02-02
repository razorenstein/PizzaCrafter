using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Managers;
using Assets._PC.Scripts.Gameplay.Componenets.Spawners.Abstract;

namespace Assets._PC.Scripts.Gameplay.Componenets.Spawners.Concrete
{
    public class ProductTileSpawner : TilesSpawnerBase<ProductTileView>
    {
        public override void Initialize()
        {
            base.Initialize(TileType.Product, PoolType.ProductTile, poolSize: PCManager.Instance.BoardManager.GetBoardCellsCount());
        }
    }
}
