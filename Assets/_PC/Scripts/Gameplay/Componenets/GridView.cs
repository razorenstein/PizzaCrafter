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
            InitializeGridUI();
            CreateGrid(Manager.BoardManager.Grid.CellsState);
        }

        private void CreateGrid(CellData[,] cellsData)
        {
            _gridView = new CellView[_gridSize.Rows, _gridSize.Columns];
            for (int row = 0; row < _gridSize.Rows; row++)
            {
                for (int column = 0; column < _gridSize.Columns; column++)
                {
                    var cell = Instantiate(_cellPrefab, _gridLayoutGroup.transform);
                    var cellData = cellsData[row, column];
                    cell.Initialize(cellData);
                    _gridView[row, column] = cell;
                    Debug.Log($"cell position is{cell.transform.position.x},{cell.transform.position.y},{cell.transform.position.z}");
                }
            }
        }

        private void InitializeGridUI()
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _gridSize.Columns;

            //set cell size
            var gridLayoutGroupRect = _gridLayoutGroup.transform as RectTransform;
            float totalHorizontalPadding = _gridLayoutGroup.padding.left + _gridLayoutGroup.padding.right;
            float totalVerticalPadding = _gridLayoutGroup.padding.top + _gridLayoutGroup.padding.bottom;

            float totalHorizontalSpacing = (_gridSize.Columns - 1) * _gridLayoutGroup.spacing.x;
            float totalVerticalSpacing = (_gridSize.Rows - 1) * _gridLayoutGroup.spacing.y;

            float cellWidth = (gridLayoutGroupRect.rect.width - totalHorizontalPadding - totalHorizontalSpacing) / _gridSize.Columns;
            float cellHeight = (gridLayoutGroupRect.rect.height - totalVerticalPadding - totalVerticalSpacing) / _gridSize.Rows;

            _gridLayoutGroup.cellSize = new Vector2(cellWidth, cellHeight);
        }
    }
}