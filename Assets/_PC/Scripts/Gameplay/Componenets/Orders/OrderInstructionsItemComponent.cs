using Assets._PC.Scripts.Core.Components;
using Assets._PC.Scripts.Gameplay.Componenets.Helpers;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets.Orders
{
    public class OrderInstructionsItemComponent : PCMonoBehaviour
    {
        [SerializeField] private Image _image;

        public async Task Initialize(string spriteAdressableKey)
        {
            var sprite = await AddressablesHelper.TryLoadAddressableAsync(spriteAdressableKey);
            _image.sprite = sprite;
        }
    }
}
