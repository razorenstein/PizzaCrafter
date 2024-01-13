using Assets._PC.Scripts.Core.Data;

namespace Assets._PC.Scripts.Core.Managers
{
    public class BoardManager
    {
        public TileData[] TilesState { get; private set; }

        public BoardManager()
        {

        }

        public bool TrySetTile(GridPosition position, ItemType itemType)
        {
            var tile = new TileData()
            {
                Item = new ItemData
                {
                    Type = itemType,
                    Level = 1
                },
                Position = position
            };

            if (PCManager.Instance.GridManager.TrySetTile(tile, out var targetCell))
            {
                tile.CellData = targetCell;
                return true;
            }

            return false;
        }
    }
}
