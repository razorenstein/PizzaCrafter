namespace Assets._PC.Scripts.Core.Data.Board
{
    public class CellData
    {
        public GridPosition Position { get; private set; }
        public TileData Tile { get; set; }

        public CellData(GridPosition position)
        {
            Position = position;
        }

        public bool IsOccupied() => Tile != null;
    }
}
