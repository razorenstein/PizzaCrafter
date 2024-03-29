﻿using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Ingredients;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Oven;
using Codice.Client.BaseCommands.Merge;
using log4net.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
            if (!TilesState.Any())
            {
                foreach (var resource in PCManager.Instance.ResourceManager.Resources.Values)
                {
                    TrySetTileRandomally(resource);
                }

                foreach (var oven in PCManager.Instance.OvenManager.Ovens)
                {
                    TrySetTileRandomally(oven);
                }
            }
        }

        public int GetBoardCellsCount() => Grid.GridSize.Rows * Grid.GridSize.Columns;

        public bool TryMoveTile(TileData tile, GridPosition targetPosition, out TileMovementType movementType)
        {
            movementType = TileMovementType.None;
            var targetCell = Grid.GetCellData(targetPosition);
            var originCell = tile.CellData;

            if (targetCell.IsOccupied() && targetCell != originCell)
            {
                var targetTile = targetCell.Tile;

                //Ingredient set to oven
                if ((targetTile is OvenData ovenData) && (tile is IngredientData ingredient))
                {
                    if (PCManager.Instance.OvenManager.TrySetToOven(ovenData, ingredient))
                    {
                        movementType = TileMovementType.MoveIngredientToOven;
                        return true;
                    }
                }
                //two mergables
                if (PCManager.Instance.MergeManager.TryMerge(tile, targetTile, out TileData merged))
                {
                    RemoveTiles(new List<TileData> { tile, targetTile });
                    if (TrySetTile(merged, targetPosition))
                    {
                        movementType = TileMovementType.MergeTiles;
                        return true;
                    }
                }
                // switch positions
                if (Grid.TrySwitchTilesPositions(tile.CellData.Position, targetPosition))
                {
                    movementType = TileMovementType.MoveToOccupiedCell;
                    PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnTilesPositionUpdate, new TilesPositionUpdateEventData()
                    {
                        UpdatedTiles = new List<TileMovementData>
                        {
                            new TileMovementData() { TileData = tile, OriginPosition = originCell.Position, TargetPosition = targetCell.Position },
                            new TileMovementData() { TileData = targetTile, OriginPosition = targetCell.Position, TargetPosition = originCell.Position }
                        }
                    });
                    return true;
                }
            }
            else // target cell is not occupied
            {
                //regular move to empty cell
                if (Grid.TryMoveTile(tile, targetPosition, out targetCell))
                {
                    movementType = TileMovementType.MoveToEmptyCell;
                    PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnTilesPositionUpdate, new TilesPositionUpdateEventData()
                    {
                        UpdatedTiles = new List<TileMovementData>
                        {
                            new TileMovementData() { TileData = tile, OriginPosition = originCell.Position, TargetPosition = targetCell.Position } 
                        }
                    });

                    return true;
                }
            }

            return false;
        }

        public bool TrySetTile(TileData tile, GridPosition position)
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

        public void RemoveTiles(List<TileData> tiles)
        {
            foreach (var tile in tiles)
            {
                if (Grid.TryRemoveTile(tile.CellData.Position, out var targetCell))
                {
                    TilesState.Remove(tile);
                    PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnTileRemoved, new TileRemovedEventData()
                    {
                        Tile = tile,
                    });
                }
            }
        }

        public bool TrySetTileRandomally(TileData tile)
        {
            if (Grid.TryGetRandomEmptyCell(out var emptyCell))
            {
                if (TrySetTile(tile, emptyCell.Position))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
