using Assets._PC.Scripts.Core.Data.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class TilesMergeEventData : PCBaseEventData
    {
        public TileData MergedTile { get; set; }
        public TileData OriginTile { get; set; }
        public TileData TargetTile { get; set; }

    }
}
