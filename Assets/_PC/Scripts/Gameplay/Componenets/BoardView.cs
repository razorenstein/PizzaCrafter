using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Gameplay.Components;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class BoardView : PCMonoBehaviour
    {
        private ICollection<TileView> _tiles;
        [SerializeField]
        private TileView _tilePrefab;
        [SerializeField]
        private GridView _gridView;

        private void Start()
        {
            _tiles = new Collection<TileView>();
            Initialize();
        }

        private void Initialize()
        {
            _gridView.Initialize();
            RegisterEventListeners();
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            foreach (var tileData in Manager.BoardManager.TilesState)
            {
                SetTile(tileData);
            }
        }

        private void SetTile(TileData tileData)
        {
            var cell = _gridView.GetCell(tileData.Position).transform as RectTransform;
            var tile = Instantiate(_tilePrefab, cell.transform);
            tile.Initialize(tileData);
            _tiles.Add(tile);
        }

        private void RegisterEventListeners()
        {
            Manager.EventManager.AddListener(PCEventType.OnTileSet, OnTileSet);
        }

        private void UnRegisterEventListeners()
        {
            Manager.EventManager.RemoveListener(PCEventType.OnTileSet, OnTileSet);
        }

        private void OnTileSet(PCBaseEventData baseEventData)
        {
            var eventData = (TileSetOnCellEventData)baseEventData;
            SetTile(eventData.TileData);
        }

        private void OnDestroy()
        {
            UnRegisterEventListeners();
        }
    }
}
