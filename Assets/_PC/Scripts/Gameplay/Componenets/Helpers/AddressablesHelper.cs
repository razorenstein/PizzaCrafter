using Assets._PC.Scripts.Core.Components;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets._PC.Scripts.Gameplay.Componenets.Helpers
{
    public class AddressablesHelper : PCMonoBehaviour
    {
        public static void TryLoadAddressable(string addressableKey, Action<Sprite> onSuccess, Action<string> onFailure)
        {
            Addressables.LoadAssetAsync<Sprite>(addressableKey).Completed += onHandle =>
            {
                if (onHandle.Status == AsyncOperationStatus.Succeeded)
                {
                    Sprite loadedSprite = onHandle.Result;
                    onSuccess?.Invoke(loadedSprite); // Call the success callback with the loaded sprite
                }
                else
                {
                    onFailure?.Invoke($"Failed to load sprite with address: {addressableKey}"); // Call the failure callback
                }
            };
        }
    }
}
