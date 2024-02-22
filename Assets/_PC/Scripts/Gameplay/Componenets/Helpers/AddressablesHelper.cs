using Assets._PC.Scripts.Core.Components;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets._PC.Scripts.Gameplay.Componenets.Helpers
{
    public class AddressablesHelper : PCMonoBehaviour
    {
        public static async Task<Sprite> TryLoadAddressableAsync(string addressableKey)
        {
            AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(addressableKey);
            await handle.Task; 
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                return handle.Result;
            }
            else
            {
                throw new Exception($"Failed to load sprite with address: {addressableKey}");
            }
        }
    }
}
