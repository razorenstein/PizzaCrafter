using Assets._PC.Scripts.Core.Data.Ingredients;

namespace Assets._PC.Scripts.Core.Data.Board
{
    public class TileData
    {
        public GridPosition Position { get; set; }
        public CellData CellData { get; set; }
        public IngredientData Ingredient { get; set; }
    }
}
