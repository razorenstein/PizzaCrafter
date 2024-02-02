using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Gameplay.Componenets.Spawners.Abstract;
using Assets._PC.Scripts.Gameplay.Componenets.Spawners.Concrete;
using Assets._PC.Scripts.Gameplay.Components;
using log4net.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting.YamlDotNet.Core;
using UnityEngine;

namespace Assets._PC.Scripts.Gameplay.Componenets.Spawners
{
    public class TileSpawnerManager : PCMonoBehaviour
    {
        private Dictionary<TileType, ITileSpawner> _tileSpawners;
        [SerializeField]
        private ResourceTileSpawner _resourceTileSpawner;
        [SerializeField]
        private IngredientTileSpawner _ingredientTileSpawner;
        [SerializeField]
        private ProductTileSpawner _productTileSpawner;
        [SerializeField]
        private OvenTileSpawner _ovenTileSpawner;

        public void Initialize()
        {
            _tileSpawners = new Dictionary<TileType, ITileSpawner>
            {
                { TileType.Resource, _resourceTileSpawner },
                { TileType.Ingredient, _ingredientTileSpawner },
                { TileType.Product, _productTileSpawner },
                { TileType.Oven, _ovenTileSpawner }
            };

            foreach(var tileSpawner in _tileSpawners)
            {
                tileSpawner.Value.Initialize();
            }
        }

        public async Task<TileView> CreateTile(TileData tileData)
        {
            TileView tileView = null;
            if (_tileSpawners.TryGetValue(tileData.Type, out var tileSpawner))
            {
                tileView =  await tileSpawner.CreateTile(tileData);
            }

            return tileView;
        }

        public void RemoveTile(TileData tileData, TileView tileView)
        {
            if (_tileSpawners.TryGetValue(tileData.Type, out var tileSpawner))
            {
                tileSpawner.RemoveTile(tileView);
            }
        }

        public async Task<List<TileView>> SpawnTiles(TileType tileType)
        {
            if(_tileSpawners.TryGetValue(tileType, out var tileSpawner))
            {
                return await tileSpawner.SpawnTiles();
            }

            return new List<TileView>();
        }
    }
}
