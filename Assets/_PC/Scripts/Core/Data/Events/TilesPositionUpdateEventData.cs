using Assets._PC.Scripts.Core.Data.Board;
using System.Collections.Generic;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class TilesPositionUpdateEventData : PCBaseEventData
    {
        public List<TileMovementData> UpdatedTiles { get; set; }
    }

    public class TileMovementData
    {
        public TileData TileData { get; set; }
        public GridPosition OriginPosition { get; set; }
        public GridPosition TargetPosition { get; set; }
    }
}
