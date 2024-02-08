using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Core.Data.Orders;
using Assets._PC.Scripts.Core.Data.Oven;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class OrderComponent : PCMonoBehaviour, IPointerClickHandler
    {
        public OrderData Data { get; private set; }
        [SerializeField]
        private Image _recipeImage;
        [SerializeField]
        private TMP_Text _amountText;
        [SerializeField]
        private TMP_Text _rewardText;
        private bool _isFulfilled = false;

        public async virtual Task Initialize(OrderData data)
        {
            Data = data;
            _recipeImage.sprite = await AddressablesHelper.TryLoadAddressableAsync(data.ProductData.SpriteAddressableKey);
            _amountText.text = "X" + data.Amount.ToString();
            _rewardText.text = data.Reward.ToString();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Manager.OrdersManager.TryCompleteOrder(Data.Id);           
        }

        public void SetFulfilled()
        {
            _isFulfilled = true;
            _recipeImage.color = Color.red;
        }
    }
}
