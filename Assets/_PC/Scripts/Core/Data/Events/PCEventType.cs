using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public enum PCEventType
    {
        OnPoolReady,
        OnTileCreated,
        OnTileRemoved,
        OnTilesMerged,
        OnTilesPositionUpdate,
        OnIngredientMovedToOven,
        OnOrderConditionsFulfilled, //all the conditions are satisfied
        OnOrderCompleted,
        OnOrderExpired,
        OnOvenIngredientsFormRecipe
    }
}
