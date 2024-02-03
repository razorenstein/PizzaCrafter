using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Managers.Events;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Managers
{
    public class PCManager
    {
        private static int _boardRows = 4;
        private static int _boardCols = 4;

        public static PCManager Instance { get; private set; }

        //Core
        public PCEventManager<PCBaseEventData> EventManager;
        public ConfigurationManager ConfigurationManager;
        public FactoryManager FactoryManager;
        public PoolManager PoolManager;

        //Gameplay
        public ResourceManager ResourceManager;
        public IngredientsManager IngredientsManager;
        public ProductManager ProductManager;
        public OvenManager OvenManager;
        public RecipesManager RecipesManager;
        public MergeManager MergeManager;
        public BoardManager BoardManager;

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

            ResourceManager = new ResourceManager();
            IngredientsManager = new IngredientsManager();
            ProductManager = new ProductManager();
            OvenManager = new OvenManager();
            RecipesManager = new RecipesManager();
            MergeManager = new MergeManager();
            BoardManager = new BoardManager(new GridSize(_boardRows, _boardCols));
        }
    }
}
