using Assets._PC.Scripts.Core.Components;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public class CellView : PCMonoBehaviour
    {
        [SerializeField]
        private Image _item;
        private CellData _data;

        public void Initialize(CellData data)
        {
            _data = data;
            Debug.Log($"Initialized cell at {_data.Position.Row}, {_data.Position.Column}");
        }

        private void OnMouseDown()
        {
            // Handle the click event, like selecting the tile or placing an ingredient
            Debug.Log($"Tile clicked.");

            // Perform actions based on game logic, e.g., placing an ingredient
            // This is where you would typically interact with other game systems
        }
    }
}
