using Assets._PC.Scripts.Core.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

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

        public bool TrySetTile(TileData tile, out CellData targetCell)
        {
            targetCell = null;

            if (IsPositionValid(tile.Position))
            {
                targetCell = CellsState[tile.Position.Row, tile.Position.Column];
                if (!targetCell.IsOccupied())
                {
                    targetCell.Tile = tile;
                    _emptyCells.Remove(targetCell);
                    return true;
                }
            }
            return false;
        }

        public bool TryRemoveTile(TileData tile, out CellData targetCell)
        {
            targetCell = null;
            if (IsPositionValid(tile.Position))
            {
                targetCell = CellsState[tile.Position.Row, tile.Position.Column];
                if (targetCell.IsOccupied())
                {
                    targetCell.Tile = null;
                    _emptyCells.Add(targetCell);
                    return true;
                }
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
