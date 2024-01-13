using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Gameplay.Components;
using UnityEngine;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class TileView : PCMonoBehaviour
    {
        private TileData _data;

        public void Initialize(TileData data)
        {
            _data = data;
            Debug.Log($"Initialized tile at {_data.Position.Row}, {_data.Position.Column}");
        }
    }
}
