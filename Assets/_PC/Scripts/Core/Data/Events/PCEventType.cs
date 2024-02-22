namespace Assets._PC.Scripts.Core.Data.Events
{
    public enum PCEventType
    {
        OnPoolReady,
        //BoardTiles
        OnTileCreated,
        OnTileRemoved,
        OnTilesMerged,
        OnTilesPositionUpdate,
        OnIngredientMovedToOven,
        //Orders
        OnOrderConditionsFulfilled, //all the conditions are satisfied
        OnOrderCompleted,
        OnOrderExpired,
        OnOvenIngredientsFormRecipe,
        //Currencies
        OnCurrenciesUpdated
    }
}
