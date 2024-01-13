using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data;
using Assets._PC.Scripts.Gameplay.Components;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Components
{
    public class GridView : PCMonoBehaviour
    {
        [SerializeField] private int _width;
        [SerializeField] private int _height;
        [SerializeField] private TileCellView _tilePrefab;
        [SerializeField] private GridLayoutGroup _gridLayoutGroup;

        private TileCellView[,] _grid;

        void Start()
        {
            _gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            _gridLayoutGroup.constraintCount = _width;
            //get from manager all data
            CreateGrid();
            Manager.BoardManager.TrySetTile(new GridPosition(1,1), ItemType.Tomato);
        }

        private void CreateGrid()
        {
            _grid = new TileCellView[_width, _height];
            for (int row = 0; row < _height; row++)
            {
                for (int column = 0; column < _width; column++)
                {
                    var tile = Instantiate(_tilePrefab, _gridLayoutGroup.transform, false);
                    var tilePosition = new GridPosition(row, column);
                    tile.Initialize(tilePosition);
                    _grid[column, row] = tile;
                }
            }
        }
    }
}