using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Ingredients;
using log4net.Core;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Assets._PC.Scripts.Core.Managers
{
    public class BoardManager
    {
        public ICollection<TileData> TilesState { get; private set; }
        public Grid Grid { get; private set; }

        public BoardManager(GridSize gridSize)
        {
            Grid = new Grid(gridSize);
            TilesState = new Collection<TileData>();

            Initialize();
        }

        private void Initialize()
        {
            Grid.Initialize();
        }

        public bool TrySetTile(IngredientData ingredient)
        {
            if (Grid.TryGetRandomEmptyCell(out var emptyCell))
            {
                if (TrySetTile(emptyCell.Position, ingredient))
                {
                    return true;
                }
            }

            return false;
        }

        public bool TrySetTile(GridPosition position, IngredientData ingredientData)
        {
            var tile = new TileData()
            {
                Ingredient = ingredientData,
                Position = position
            };

            if (Grid.TrySetTile(tile, out var targetCell))
            {
                tile.CellData = targetCell;
                TilesState.Add(tile);
                PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnTileSet,
                    new TileSetOnCellEventData
                    {
                        TileData = tile
                    });
                return true;
            }

            return false;
        }
    }
}
