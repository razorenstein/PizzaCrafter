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

        public bool TrySetTile(ItemData itemData)
        {
            if (Grid.TryGetRandomEmptyCell(out var emptyCell))
            {
                if (TrySetTile(emptyCell.Position, itemData))
                {
                    return true;
                }
            }

            return false;
        }

        public bool TrySetTile(GridPosition position, ItemData itemData)
        {
            var tile = new TileData()
            {
                Item = itemData,
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
