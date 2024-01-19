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
        public ConfigurationManager ConfigurationManager;
        public FactoryManager FactoryManager;
        public PoolManager PoolManager;

        //Gameplay
        public BoardManager BoardManager;
        public ResourceManager ResourceManager;
        public IngredientsManager IngredientsManager;



        public PCManager()
        {
            if (Instance == null)
                Instance = this;
            else
                Debug.LogError($"{nameof(PCManager)}- Only One Appearance is valid");

            EventManager = new PCEventManager<PCBaseEventData>();
            ConfigurationManager = new ConfigurationManager();
            PoolManager = new PoolManager();
            FactoryManager = new FactoryManager();

            BoardManager = new BoardManager(new GridSize(_boardRows, _boardCols));
            ResourceManager = new ResourceManager();
            IngredientsManager = new IngredientsManager();
        }
    }
}
