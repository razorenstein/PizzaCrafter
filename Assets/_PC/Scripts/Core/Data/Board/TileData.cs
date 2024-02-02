using Assets._PC.Scripts.Core.Data.Enums;
using System;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public abstract class TileData
    {
        public Guid Id { get; set; }
        public CellData CellData { get; set; }
        public TileType Type { get; set; }
        public string SpriteAddressableKey { get; set; }

        public TileData()
        {
            Id = Guid.NewGuid();
        }
    }
}
