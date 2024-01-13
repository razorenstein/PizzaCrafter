using Assets._PC.Scripts.Core.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public class Grid
    {
        public CellData[,] CellsState { get; private set; }
        public GridSize GridSize { get; private set; }

        public Grid(GridSize gridSize)
        {
            //MFManager.Instance.ConfigManager.GetConfig<MinesConfig>(OnConfigLoaded);  
            //LoadMinesData();
            GridSize = gridSize;
            Initialize();
        }

        public void Initialize()
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
                    return true;
                }
            }

            return false;
        }

        public bool TryGetRandomEmptyCell(out CellData cell)
        {
            cell = null;
            var emptyCells = GetEmptyCells();

            if (emptyCells.Any())
            {
                var randomIndex = Random.Range(0, emptyCells.Count - 1);
                cell = emptyCells[randomIndex];

                return true;
            }

            return false;
        }

        private List<CellData> GetEmptyCells()
        {
            var emptyCells = new List<CellData>();
            foreach (var cell in CellsState)
            {
                if (!cell.IsOccupied())
                    emptyCells.Add(cell);
            }

            return emptyCells;
        }

        private bool IsPositionValid(GridPosition position) =>
            position.Row < GridSize.Rows && position.Column < GridSize.Columns;
    }
}
