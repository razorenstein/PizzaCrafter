using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Board;

namespace Assets._PC.Scripts.Gameplay.Managers
{
    public class BoardManager
    {
        public TileData[] TilesState { get; private set; }
        private Grid _grid;

        public BoardManager(GridSize gridSize)
        {
            _grid = new Grid(gridSize);
            _grid.Initialize();
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

            if (_grid.TrySetTile(tile, out var targetCell))
            {
                tile.CellData = targetCell;
                return true;
            }

            return false;
        }
    }
}
