namespace Assets._PC.Scripts.Core.Data
{
    public class TileCellData
    {
        public GridPosition Position { get; private set; }
        public TileData Tile { get; set; }

        public TileCellData(GridPosition position)
        {
            Position = position;
        }

        public bool IsOccupied() => Tile != null;
        
    }
}
