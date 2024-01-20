﻿using Assets._PC.Scripts.Core.Data.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class TileRemovedEventData : PCBaseEventData
    {
        public TileData Tile { get; set; }
        public CellData Cell { get; set; }
    }
}
