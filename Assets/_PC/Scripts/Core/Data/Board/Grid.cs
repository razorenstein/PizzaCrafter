using Assets._PC.Scripts.Core.Data;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public class Grid
    {
        public CellData[,] CellsState { get; private set; }
        public GridSize GridSize { get; private set; }

        public Grid(GridSize gridSize)
        {
            //MFManager.Instance.ConfigManager.GetConfig<MinesConfig>(OnConfigLoaded);  
            //LoadMinesData();
            GridSize = gridSize;
            Initialize();
        }

        public void Initialize()
        {
            CellsState = new CellData[GridSize.Rows, GridSize.Columns];
            for (int row = 0; row < GridSize.Rows; row++)
            {
                for (int col = 0; col < GridSize.Columns; col++)
                {
                    CellsState[row, col] = new CellData(new GridPosition(row, col));
                }
            }
        }

        public bool TrySetTile(TileData tile, out CellData targetCell)
        {
            targetCell = null;

            if (IsPositionValid(tile.Position))
            {
                targetCell = CellsState[tile.Position.Row, tile.Position.Column];
                if (!targetCell.IsOccupied())
                {
                    targetCell.Tile = tile;
                    return true;
                }
            }
            return false;
        }

        public bool TryRemoveTile(TileData tile, out CellData targetCell)
        {
            targetCell = null;
            if (IsPositionValid(tile.Position))
            {
                targetCell = CellsState[tile.Position.Row, tile.Position.Column];
                if (targetCell.IsOccupied())
                {
                    targetCell.Tile = null;
                    return true;
                }
            }

            return false;
        }

        private bool IsPositionValid(GridPosition position) =>
            position.Row < GridSize.Rows && position.Column < GridSize.Columns;
    }
}
