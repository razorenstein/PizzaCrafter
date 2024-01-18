using Assets._PC.Scripts.Core.Data.Ingredients;
using System;
using UnityEditor;
using UnityEngine.Scripting;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public class TileData
    {
        public GUID ID { get; private set; }
        public CellData CellData { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
