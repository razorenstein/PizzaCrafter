using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.VFX;

namespace Assets._PC.Scripts.Core.Managers
{
    public class BoardManager
    {
        public ICollection<TileData> TilesState { get; private set; }
        public Grid Grid { get; private set; }

        public BoardManager(GridSize gridSize)
        {
            Grid = new Grid(gridSize);
            TilesState = new Collection<TileData>();

            Initialize();
        }

        private void Initialize()
        {
            Grid.Initialize();
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

            if (Grid.TrySetTile(tile, out var targetCell))
            {
                tile.CellData = targetCell;
                TilesState.Add(tile);
                PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnTileSet,
                    new TileSetOnCellEventData
                    {
                        TileData = tile
                    });
                return true;
            }

            return false;
        }
    }
}
