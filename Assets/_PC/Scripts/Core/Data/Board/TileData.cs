using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using System;
using UnityEditor;
using UnityEngine.Scripting;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public abstract class TileData
    {
        public CellData CellData { get; set; }
        public TileType Type { get; set; }
        public string SpriteAddressableKey { get; set; }
    }
}
