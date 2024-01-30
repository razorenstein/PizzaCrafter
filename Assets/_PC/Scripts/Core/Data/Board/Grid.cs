using Assets._PC.Scripts.Core.Data;
using Codice.Client.Common;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public class Grid
    {
        public CellData[,] CellsState { get; private set; }
        public GridSize GridSize { get; private set; }
        private List<CellData> _emptyCells { get; set; }


        public Grid(GridSize gridSize)
        {
            GridSize = gridSize;
            _emptyCells = new List<CellData>();
        }

        public void Initialize()
        {
            InitializeGrid();
            SetEmptyCells();
        }

        public void InitializeGrid()
        {
            CellsState = new CellData[GridSize.Rows, GridSize.Columns];
            for (int row = 0; row < GridSize.Rows; row++)
            {
                for (int col = 0; col < GridSize.Columns; col++)
                {
                    CellsState[row, col] = new CellData(new GridPosition(row, col));
                }
            }
        }

        public CellData GetCellData(GridPosition position)
        {
            if (IsPositionValid(position))
            {
                return CellsState[position.Row, position.Column];
            }

            throw new System.Exception("inbalid grid position selected");
        }

        public bool TrySetNewTile(TileData tile, GridPosition position, out CellData targetCell)
        {
            targetCell = null;

            if (IsPositionValid(position))
            {
                targetCell = CellsState[position.Row, position.Column];
                if (!targetCell.IsOccupied())
                {
                    targetCell.Tile = tile;
                    tile.CellData = targetCell;
                    _emptyCells.Remove(targetCell);
                    return true;
                }
            }

            return false;
        }

        public bool TryRemoveTile(GridPosition position, out CellData targetCell)
        {
            targetCell = null;
            if (IsPositionValid(position))
            {
                targetCell = CellsState[position.Row, position.Column];
                if (targetCell.IsOccupied())
                {
                    targetCell.Tile = null; 
                    _emptyCells.Add(targetCell);
                    return true;
                }
            }

            return false;
        }

        public bool TryMoveTile(TileData tile, GridPosition targetPosition, out CellData targetCell)
        {
            targetCell = null;

            if (IsPositionValid(targetPosition))
            {
                var originCell = CellsState[tile.CellData.Position.Row, tile.CellData.Position.Column];
                targetCell = CellsState[targetPosition.Row, targetPosition.Column];

                originCell.Tile = null;
                targetCell.Tile = tile;
                tile.CellData = targetCell;

                _emptyCells.Add(originCell);
                _emptyCells.Remove(targetCell);

                return true;
            }

            return false;
        }

        public bool TryMergeTiles(TileData mergedTile, GridPosition firstPosition, GridPosition secondPosition)
        {
            if (IsPositionValid(firstPosition) && IsPositionValid(secondPosition))
            {
                var originCell = CellsState[firstPosition.Row, firstPosition.Column];
                var targetCell = CellsState[secondPosition.Row, secondPosition.Column];

                originCell.Tile = null;
                targetCell.Tile = mergedTile;
                mergedTile.CellData = targetCell;
                _emptyCells.Add(originCell);
                _emptyCells.Remove(targetCell);

                return true;
            }

            return false;
        }

        public bool TrySwitchTilesPositions(GridPosition firstPosition, GridPosition secondPosition)
        {
            if (IsPositionValid(firstPosition) && IsPositionValid(secondPosition))
            {
                var firstCell = CellsState[firstPosition.Row, firstPosition.Column];
                var secondCell = CellsState[secondPosition.Row, secondPosition.Column];
                var firstCellTile = firstCell.Tile;
                var secondCellTile = secondCell.Tile;

                // Swap tiles
                firstCell.Tile = secondCellTile;
                secondCell.Tile = firstCellTile;

                // Update the CellData on the swapped tiles to point to their new cells
                firstCell.Tile.CellData = firstCell;
                secondCell.Tile.CellData = secondCell;

                return true;
            }

            return false;
        }

        public bool TryGetRandomEmptyCell(out CellData cell)
        {
            cell = null;
            if (_emptyCells.Any())
            {
                var randomIndex = Random.Range(0, _emptyCells.Count - 1);
                cell = _emptyCells.ElementAt(randomIndex);

                return true;
            }

            return false;
        }

        private void SetEmptyCells()
        {
            foreach (var cell in CellsState)
            {
                if (!cell.IsOccupied())
                    _emptyCells.Add(cell);
            }
        }

        private bool IsPositionValid(GridPosition position) =>
            position.Row < GridSize.Rows && position.Column < GridSize.Columns;

    }
}
