﻿using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Pool;
using Assets._PC.Scripts.Core.Managers;
using Assets._PC.Scripts.Gameplay.Componenets.Spawners;
using Assets._PC.Scripts.Gameplay.Components;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class BoardView : PCMonoBehaviour
    {
        public static BoardView Instance { get; private set; }
        private Dictionary<GridPosition, TileView> _tilesState;
        [SerializeField]
        private GridView _gridView;
        [SerializeField]
        private TileSpawnerManager _tileSpawnerManager;
        private TileView _currentDraggedTile;

        private void Start()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.LogError($"{nameof(BoardView)}- Only One Appearance is valid");

            _tilesState = new Dictionary<GridPosition, TileView>();
            _currentDraggedTile = null;
            Initialize();
        }

        private void Initialize()
        {
            _gridView.Initialize();
            RegisterEventListeners();
            _tileSpawnerManager.Initialize(_gridView);
            InitializeTilesState();
            // InitializeTiles();

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

        private void SpawnTiles(TileType tileType)
        {
            var spawnedTiles = _tileSpawnerManager.SpawnTiles(tileType);
            foreach (var spawnedTile in spawnedTiles)
            {
                _tilesState[spawnedTile.Data.CellData.Position] =  spawnedTile;
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
            var tile = _tileSpawnerManager.CreateTile(tileData);
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

        private void OnPoolReady(PCBaseEventData baseEventData)
        {
            var eventData = (PoolReadyEventData)baseEventData;
            SpawnTiles(PoolTypesHelper.MapToTileType(eventData.Type));
        }

        private void RegisterEventListeners()
        {
            Manager.EventManager.AddListener(PCEventType.OnTileCreated, OnTileCreated);
            Manager.EventManager.AddListener(PCEventType.PoolReady, OnPoolReady);
        }


        private void UnRegisterEventListeners()
        {
            Manager.EventManager.RemoveListener(PCEventType.OnTileCreated, OnTileCreated);
            Manager.EventManager.RemoveListener(PCEventType.PoolReady, OnPoolReady);
        }

        private void OnDestroy()
        {
            UnRegisterEventListeners();
        }
    }
}
