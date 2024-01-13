using Assets._PC.Scripts.Core.Components;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public class TileCellView : PCMonoBehaviour
    {
        [SerializeField]
        private Image _item;
        private GridPosition _position;

        public void Initialize(GridPosition position)
        {
            _position = position;
            Debug.Log($"Initialized tile at {_position.Row}, {_position.Column}");
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
