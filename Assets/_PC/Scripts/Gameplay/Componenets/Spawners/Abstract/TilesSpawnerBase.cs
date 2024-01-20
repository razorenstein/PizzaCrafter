using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Managers;
using Assets._PC.Scripts.Gameplay.Components;
using System;
using UnityEngine;

namespace Assets._PC.Scripts.Gameplay.Componenets.Spawners.Abstract
{
    public abstract class TilesSpawnerBase<T> : PCMonoBehaviour, ITileSpawner where T : TileView
    {
        [SerializeField]
        protected GridView _gridView;
        [SerializeField]
        protected TileView _tilePrefab;
        protected PoolType _poolType;
        protected TileType _tileType;
        protected int _poolSize;

        public void Initialize(GridView gridView, TileType tileType, PoolType poolType, int poolSize)
        {
            _gridView = gridView;
            _poolType = poolType;
            _poolSize = poolSize;
            _tileType = tileType;
            InitializePool(_poolType, _poolSize);
        }

        public TileView CreateTile(TileData tileData)
        {
            var cell = _gridView.GetCell(tileData.CellData.Position);
            var tile = Manager.PoolManager.GetFromPool<T>(_poolType);
            tile.Initialize(tileData);
            tile.transform.SetParent(cell.transform, false);
            tile.transform.position = cell.transform.position;

            return tile;
        }

        public void RemoveTile(TileData tileData, TileView tileView)
        {
            if (tileView is T tileAsT)
                Manager.PoolManager.ReturnToPool<T>(_poolType, tileAsT);         
        }

        public virtual TileView[] SpawnTiles()
        {
            var tilesData = Manager.BoardManager.TilesState;
            var tiles = new TileView[tilesData.Count];
            int index = 0;
            foreach (var tileData in tilesData)
            {
                if (tileData.Type == _tileType)
                {
                    tiles[index] = CreateTile(tileData);
                    index++;
                }
                else
                    return Array.Empty<TileView>();
            }

            return tiles;
        }

        protected virtual void InitializePool(PoolType poolType, int poolSize) =>
            PCManager.Instance.PoolManager.InitPool<T>(poolType, poolSize);
    }
}
