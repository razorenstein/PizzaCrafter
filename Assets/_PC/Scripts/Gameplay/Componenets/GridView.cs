using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Core.Data.Board;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Components
{
    public class GridView : PCMonoBehaviour
    {
        private GridSize _gridSize;
        [SerializeField] private CellView _cellPrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;
        private CellView[,] _gridView;

        public CellView GetCell(GridPosition position) => _gridView[position.Row, position.Column];
     
        public void Initialize()
        {
            _gridSize = Manager.BoardManager.Grid.GridSize;
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _gridSize.Columns;

            CreateGrid(Manager.BoardManager.Grid.CellsState);
        }

        private void CreateGrid(CellData[,] cellsData)
        {
            _gridView = new CellView[_gridSize.Rows, _gridSize.Columns];
            for (int row = 0; row < _gridSize.Rows; row++)
            {
                for (int column = 0; column < _gridSize.Columns; column++)
                {
                    var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform, false);
                    var cellData = cellsData[row, column];
                    cell.Initialize(cellData);
                    _gridView[column, row] = cell;
                }
            }
        }
    }
}