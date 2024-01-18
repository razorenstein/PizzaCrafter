using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using Assets._PC.Scripts.Gameplay.Components;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class BoardView : PCMonoBehaviour
    {
        private Dictionary<GridPosition, TileView> _tilesState;
        [SerializeField]
        private TileView _tilePrefab;
        [SerializeField]
        private GridView _gridView;
        private TileView _currentDraggedTile;

        private void Start()
        {
            Manager.PoolManager.InitPool<TileView>(PoolType.Tile, Manager.BoardManager.GetBoardCellsCount());
            _tilesState = new Dictionary<GridPosition, TileView>();
            _currentDraggedTile = null;
            Initialize();
        }

        private void Initialize()
        {
            _gridView.Initialize();
            RegisterEventListeners();
            InitializeTiles();
        }

        public void SetDraggedTile(TileView tile) => _currentDraggedTile = tile;

        public void RemoveDraggedTile() => _currentDraggedTile = null;

        public void OnTileDragDrop(CellData targetCellData)
        {
            var originCell = _gridView.GetCell(_currentDraggedTile.Data.CellData.Position);
            var targetCell = _gridView.GetCell(targetCellData.Position);
            if (Manager.BoardManager.TryMoveTile(_currentDraggedTile.Data, targetCellData.Position, out var movementType))
            {
                switch (movementType)
                {
                    case TileMovementType.MoveToEmptyCell:
                        UpdateTilePosition(_currentDraggedTile, originCell, targetCell, true);
                        break;

                    case TileMovementType.MoveToOccupiedCell:

                        var tileOnTargetCell = _tilesState[targetCell.Data.Position];
                        UpdateTilePosition(tileOnTargetCell, targetCell, originCell, false);
                        UpdateTilePosition(_currentDraggedTile, originCell, targetCell, false);
                        break;
                }
            }
        }

        private void InitializeTiles()
        {
            InitializeTilesState();
            foreach (var tileData in Manager.BoardManager.TilesState)
            {
                CreateTile(tileData);
            }
        }

        private void InitializeTilesState()
        {
            var gridSize = Manager.BoardManager.Grid.GridSize;
            for (int row = 0; row < gridSize.Rows; row++)
            {
                for (int column = 0; column < gridSize.Columns; column++)
                {
                    _tilesState.Add(new GridPosition(row, column), null);
                }
            }
        }

        private void CreateTile(TileData tileData)
        {
            var cell = _gridView.GetCell(tileData.CellData.Position);
            var tile = Manager.PoolManager.GetFromPool<TileView>(PoolType.Tile);
            tile.transform.SetParent(cell.transform, false);
            tile.Initialize(tileData, this);
            _tilesState[tileData.CellData.Position] = tile;
        }

        private void UpdateTilePosition(TileView tile, CellView originCell, CellView targetCell, bool isMoveToEmptyCell)
        {
            tile.transform.SetParent(targetCell.transform);
            tile.transform.position = targetCell.transform.position;
            _tilesState[targetCell.Data.Position] = tile;
            if (isMoveToEmptyCell)
                _tilesState[originCell.Data.Position] = null;
        }

        private void OnTileCreated(PCBaseEventData baseEventData)
        {
            var eventData = (TileCreatedEventData)baseEventData;
            CreateTile(eventData.Tile);
        }

        private void RegisterEventListeners()
        {
            Manager.EventManager.AddListener(PCEventType.OnTileCreated, OnTileCreated);
        }

        private void UnRegisterEventListeners()
        {
            Manager.EventManager.RemoveListener(PCEventType.OnTileCreated, OnTileCreated);
        }

        private void OnDestroy()
        {
            UnRegisterEventListeners();
        }
    }
}
