using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Pool;
using Assets._PC.Scripts.Gameplay.Componenets.Spawners;
using Assets._PC.Scripts.Gameplay.Components;
using Assets._PC.Scripts.Core.Data.Board;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;
using System.Linq;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class BoardView : PCMonoBehaviour
    {
        public static BoardView Instance { get; private set; }
        private List<TileView> _tilesState;
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

            _tilesState = new List<TileView>();
            _currentDraggedTile = null;
            Initialize();
        }

        private void Initialize()
        {
            _gridView.Initialize();
            RegisterEventListeners();
            _tileSpawnerManager.Initialize();
        }

        private async Task SpawnTiles(TileType tileType)
        {
            var spawnedTiles = await _tileSpawnerManager.SpawnTiles(tileType);
            foreach (var spawnedTile in spawnedTiles)
            {
                SetTileOnBoard(spawnedTile);
            }
        }

        private async Task CreateTile(TileData tileData)
        {
            var tile = await _tileSpawnerManager.CreateTile(tileData);
            SetTileOnBoard(tile);
        }

        private void SetTileOnBoard(TileView tileView)
        {
            var cell = _gridView.GetCell(tileView.Data.CellData.Position);
            tileView.RectTransform.SetParent(cell.transform, false);
            tileView.RectTransform.localScale = new Vector2(1, 1);
            tileView.RectTransform.sizeDelta = new Vector2(1, 1);
            tileView.RectTransform.position = cell.transform.position;
            tileView.gameObject.SetActive(true);

            _tilesState.Add(tileView);
        }

        private bool TryGetTileById(Guid id, out TileView tile)
        {
            tile = _tilesState.FirstOrDefault(t => t.Data.Id == id);

            return tile != null;
        }

        private void RemoveTile(TileData tileData)
        {
            if (TryGetTileById(tileData.Id, out var tile))
            {
                _tileSpawnerManager.RemoveTile(tileData, tile);
                _tilesState.RemoveAll(t => t.Data.Id == tileData.Id);
            }
        }

        private async void OnTileCreated(PCBaseEventData baseEventData)
        {
            var eventData = (TileCreatedEventData)baseEventData;
            await CreateTile(eventData.Tile);
        }

        private void OnTilesPositionUpdate(PCBaseEventData baseEventData)
        {
            var eventData = (TilesPositionUpdateEventData)baseEventData;
            foreach (var movementData in eventData.UpdatedTiles)
            {
                if (TryGetTileById(movementData.TileData.Id, out var tileView))
                {
                    UpdateTilePosition(tileView, movementData.TargetPosition);
                }
            }
        }

        private void OnIngredientMovedToOven(PCBaseEventData baseEventData)
        {
            var eventData = (IngredientMovedToOven)baseEventData;
            if (TryGetTileById(eventData.Ingredient.Id, out var tileView))
            {
                UpdateTilePosition(tileView, tileView.Data.CellData.Position);
                tileView.Deactivate();
            }
        }

        private async void OnTilesMerged(PCBaseEventData baseEventData)
        {
            var eventData = (TilesMergeEventData)baseEventData;
            RemoveTile(eventData.OriginTile);
            RemoveTile(eventData.TargetTile);
            await CreateTile(eventData.MergedTile);
        }

        private async void OnPoolReady(PCBaseEventData baseEventData)
        {
            var eventData = (PoolReadyEventData) baseEventData;
            await SpawnTiles(PoolTypesHelper.MapToTileType(eventData.Type));
        }

        private void UpdateTilePosition(TileView tile, GridPosition targetPosition)
        {
            var targetCell = _gridView.GetCell(targetPosition);
            tile.transform.SetParent(targetCell.transform);
            tile.transform.position = targetCell.transform.position;
        }

        private void RegisterEventListeners()
        {
            Manager.EventManager.AddListener(PCEventType.OnTileCreated, OnTileCreated);
            Manager.EventManager.AddListener(PCEventType.OnTilesMerged, OnTilesMerged);
            Manager.EventManager.AddListener(PCEventType.OnIngredientMovedToOven, OnIngredientMovedToOven);
            Manager.EventManager.AddListener(PCEventType.OnTilesPositionUpdate, OnTilesPositionUpdate);
            Manager.EventManager.AddListener(PCEventType.PoolReady, OnPoolReady);
        }

        private void UnRegisterEventListeners()
        {
            Manager.EventManager.RemoveListener(PCEventType.OnTileCreated, OnTileCreated);
            Manager.EventManager.RemoveListener(PCEventType.OnTilesMerged, OnTilesMerged);
            Manager.EventManager.RemoveListener(PCEventType.OnIngredientMovedToOven, OnIngredientMovedToOven);
            Manager.EventManager.RemoveListener(PCEventType.OnTilesPositionUpdate, OnTilesPositionUpdate);
            Manager.EventManager.RemoveListener(PCEventType.PoolReady, OnPoolReady);
        }

        private void OnDestroy()
        {
            UnRegisterEventListeners();
        }
    }
}
