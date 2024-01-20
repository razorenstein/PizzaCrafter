using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Gameplay.Componenets.Spawners.Abstract
{
    public interface ITileSpawner 
    {
        TileView[] SpawnTiles();
        TileView CreateTile(TileData tileData);
        void RemoveTile(TileData tileData, TileView tileView);

    }
}
