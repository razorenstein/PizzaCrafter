using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;
using Assets._PC.Scripts.Core.Data.Oven;
using Assets._PC.Scripts.Core.Data.Recipes;
using Assets._PC.Scripts.Core.Data.Resources;
using Assets._PC.Scripts.Core.Data.Resources.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Managers
{
    public class OvenManager
    {
        public Dictionary<OvenType, OvenData> Ovens { get; private set; }
        private OvensConfig _config;

        public OvenManager()
        {
            Ovens = new Dictionary<OvenType, OvenData>();
            _config = new OvensConfig();
            PCManager.Instance.ConfigurationManager.GetConfig<OvensConfig>(OnConfigLoaded);
        }

        public bool TrySetToOven(OvenData oven, IngredientData ingredient)
        {
            if (ingredient.Level != ingredient.MaxLevel)
            {
                oven.Ingredients.Add(ingredient);

                PCManager.Instance.EventManager.InvokeEvent(PCEventType.OnIngredientMovedToOven, new IngredientMovedToOven()
                {
                    Oven = oven,
                    Ingredient = ingredient
                });

                return true;
            }

            return false;
        }

        private void OnConfigLoaded(OvensConfig config)
        {
            _config = config;
            Ovens = _config.Ovens;
        }
    }
}
