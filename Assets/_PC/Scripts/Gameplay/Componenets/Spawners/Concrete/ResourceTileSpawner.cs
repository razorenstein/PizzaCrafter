﻿using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Managers;
using Assets._PC.Scripts.Gameplay.Componenets.Spawners.Abstract;
using Assets._PC.Scripts.Gameplay.Components;

namespace Assets._PC.Scripts.Gameplay.Componenets.Spawners.Concrete
{
    public class ResourceTileSpawner : TilesSpawnerBase<ResourceTileView>
    {
        public void Initialize(GridView gridView)
        {
            base.Initialize(gridView, TileType.Resource, PoolType.ResourceTile, poolSize: PCManager.Instance.ResourceManager.Resources.Count);
        }
    }
}
