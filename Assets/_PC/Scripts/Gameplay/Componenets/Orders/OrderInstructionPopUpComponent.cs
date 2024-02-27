using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Orders;
using Assets._PC.Scripts.Core.Data.Recipes;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets.Orders
{
    public class OrderInstructionPopUpComponent : PCMonoBehaviour
    {
        [SerializeField] private OrderInstructionsItemComponent _orderInstructionsRecipeItemComponent;
        [SerializeField] private HorizontalLayoutGroup _itemsHorizontalLayoutGroup;
        [SerializeField] string _orderInstructionItemsPlusSeparator;
        [SerializeField] string _orderInstructionItemsEqualsSeparator;

        public async Task Initialize(RecipeData data)
        {
            await SpawnOrderInstructionItems(data);
        }

        private async Task SpawnOrderInstructionItems(RecipeData data)
        {
            var count = 0;
            foreach (var ingredient in data.RequiredIngredients)
            {
                await SpawnOrderInstructionItem(ingredient.SpriteAddressableKey);
                count++;
                if (data.RequiredIngredients.Length == (count)) // the last ingredient on list
                {
                    await SpawnOrderInstructionItem(_orderInstructionItemsEqualsSeparator);
                    await SpawnOrderInstructionItem(data.SpriteAddressableKey);
                }
                else
                    await SpawnOrderInstructionItem(_orderInstructionItemsPlusSeparator);
            }
        }

        private async Task SpawnOrderInstructionItem(string spriteAdressableKey)
        {
            var orderIngredientComponent = Manager.PoolManager.GetFromPool<OrderInstructionsItemComponent>(PoolType.OrderInstructionsItem);
            await orderIngredientComponent.Initialize(spriteAdressableKey);
            orderIngredientComponent.transform.SetParent(_itemsHorizontalLayoutGroup.transform, false);
            orderIngredientComponent.gameObject.SetActive(true);
        }
    }
}
