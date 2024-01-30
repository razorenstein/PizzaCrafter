using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Managers;
using Assets._PC.Scripts.Gameplay.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._PC.Scripts.Gameplay.Componenets.Spawners.Abstract
{
    public abstract class TilesSpawnerBase<T> : PCMonoBehaviour, ITileSpawner where T : TileView
    {
        [SerializeField]
        protected TileView _tilePrefab;
        protected PoolType _poolType;
        protected TileType _tileType;
        protected int _poolSize;

        public abstract void Initialize();

        protected virtual void Initialize(TileType tileType, PoolType poolType, int poolSize)
        {
            _poolType = poolType;
            _poolSize = poolSize;
            _tileType = tileType;
            InitializePool(_poolType, _poolSize);
        }

        public async Task<TileView> CreateTile(TileData tileData)
        {
            var tile = Manager.PoolManager.GetFromPool<T>(_poolType);
            await tile.Initialize(tileData);

            return tile; 
        }

        public void RemoveTile(TileView tileView)
        {
            tileView.Unload();
            if (tileView is T tileAsT)
                Manager.PoolManager.ReturnToPool<T>(_poolType, tileAsT);
        }

        public async virtual Task<List<TileView>> SpawnTiles()
        {
            var tilesData = Manager.BoardManager.TilesState;
            var tiles = new List<TileView>();
            foreach (var tileData in tilesData)
            {
                if (tileData.Type == _tileType)
                {
                    tiles.Add(await CreateTile(tileData));
                }
            }

            return tiles;
        }
        protected virtual void InitializePool(PoolType poolType, int poolSize) =>
            PCManager.Instance.PoolManager.InitPool<T>(poolType, poolSize);
    }
}
