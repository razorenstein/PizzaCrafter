using Assets._PC.Scripts.Core.Data.Board;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class TilesPositionUpdateEventData : PCBaseEventData
    {
        public TileData[] UpdatedTiles { get; set; }
    }
}
