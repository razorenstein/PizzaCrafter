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
        public List<OvenData> Ovens { get; private set; }
        private OvensConfig _config;

        public OvenManager()
        {
            Ovens = new List<OvenData>();
            _config = new OvensConfig();
            PCManager.Instance.ConfigurationManager.GetConfig<OvensConfig>(OnConfigLoaded);
        }

        public bool TrySetToOven(OvenData oven, IngredientData ingredient)
        {
            if (oven.MaxCapacity > oven.Ingredients.Count)
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
            Ovens.Add(_config.Ovens.Values.First());
        }
    }
}
