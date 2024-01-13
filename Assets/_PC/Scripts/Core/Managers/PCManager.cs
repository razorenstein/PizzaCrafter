using Assets._PC.Scripts.Core.Data;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Managers
{
    public class PCManager
    {
        private static int _boardRows = 5;
        private static int _boardCols = 5;

        public static PCManager Instance { get; private set; }

        //Gameplay
        public GridManager GridManager;
        public BoardManager BoardManager;

        public PCManager()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError($"{nameof(PCManager)}- Only One Appearance is valid");
            }

            GridManager = new GridManager(new GridSize(_boardRows, _boardCols));
            BoardManager = new BoardManager();
        }
    }
}
