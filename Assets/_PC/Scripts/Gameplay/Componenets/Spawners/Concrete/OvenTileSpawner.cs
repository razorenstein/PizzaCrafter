using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Managers;
using Assets._PC.Scripts.Gameplay.Componenets.Spawners.Abstract;
using Assets._PC.Scripts.Gameplay.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Gameplay.Componenets.Spawners.Concrete
{
    public class OvenTileSpawner : TilesSpawnerBase<OvenTileView>
    {
        public void Initialize(GridView gridView)
        {
            base.Initialize(gridView, TileType.Oven, PoolType.OvenTile, poolSize: 3);
        }
    }
}
