using Assets._PC.Scripts.Core.Data.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Assets._PC.Scripts.Core.Components;
using System.Collections.ObjectModel;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Resources;
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
