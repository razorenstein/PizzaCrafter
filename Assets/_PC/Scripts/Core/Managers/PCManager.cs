using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Managers.Events;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Managers
{
    public class PCManager
    {
        private static int _boardRows = 5;
        private static int _boardCols = 5;

        public static PCManager Instance { get; private set; }

        //Core
        public PCEventManager<PCBaseEventData> EventManager;

        //Gameplay
        public BoardManager BoardManager;
        public ResourceManager ResourceManager;
        public FactoryManager FactoryManager;
        public PoolManager PoolManager;

        public PCManager()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.LogError($"{nameof(PCManager)}- Only One Appearance is valid");

            EventManager = new PCEventManager<PCBaseEventData>();
            BoardManager = new BoardManager(new GridSize(_boardRows, _boardCols));
            ResourceManager = new ResourceManager();
            PoolManager = new PoolManager();
            FactoryManager = new FactoryManager();
        }
    }
}
