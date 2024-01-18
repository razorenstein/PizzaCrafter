using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Ingredients;
using log4net.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine.UIElements;

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

        public int GetBoardCellsCount() => Grid.GridSize.Rows * Grid.GridSize.Columns;

        public bool TryMoveTile(TileData tile, GridPosition targetPosition, out TileMovementType movementType)
        {
            movementType = TileMovementType.None;
            var targetCell = Grid.GetCellData(targetPosition);
            var originCell = tile.CellData;

            if (targetCell.IsOccupied())
            {
                if (Grid.TrySwitchTilesPositions(tile.CellData.Position, targetPosition))
                {
                    movementType = TileMovementType.MoveToOccupiedCell;
                    PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnTilesPositionUpdate, new TilesPositionUpdateEventData()
                    {
                        UpdatedTiles = new TileData[2]{ originCell.Tile, targetCell.Tile }
                    });

                    return true;
                }
            }
            else
            {
                if (Grid.TryMoveTile(tile, targetPosition, out targetCell))
                {
                    movementType = TileMovementType.MoveToEmptyCell;
                    PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnTilesPositionUpdate, new TilesPositionUpdateEventData()
                    {
                        UpdatedTiles = new TileData[1] { tile }
                    });

                    return true;
                }
            }

            return false;
        }

        public bool TrySetTile(TileData tile, GridPosition position, bool isTileCreated)
        {
            if (Grid.TrySetNewTile(tile, position, out var targetCell))
            {
                TilesState.Add(tile);
                PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnTileCreated, new TileCreatedEventData()
                {
                    Tile = tile
                });

                return true;
            }

            return false;
        }

        public bool TrySetTileRandomally(TileData tile)
        {
            if (Grid.TryGetRandomEmptyCell(out var emptyCell))
            {
                if (TrySetTile(tile, emptyCell.Position, isTileCreated: true))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
