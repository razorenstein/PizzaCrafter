using Assets._PC.Scripts.Core.Data.Board;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Assets._PC.Scripts.Core.Managers
{
    public class FactoryManager
    {
        public async Task<List<T>> GenerateObjects<T>(string addressableKey, int amount) where T : Component
        {
            var created = new List<T>();

            var original = await GenerateObjectAsync<T>(addressableKey);

            if (original == null)
            {
                return null;
            }

            created.Add(original);

            for (var i = 1; i < amount; i++)
            {
                var newCreated = GameObject.Instantiate(original);
                created.Add(newCreated);
            }

            return created;
        }

        private async Task<T> GenerateObjectAsync<T>(string addressableKey) where T : Component
        {
            var handle = Addressables.InstantiateAsync(addressableKey);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var original = handle.Result;
                return original.GetComponent<T>();
            }

            //Manager.MonitorManager.ReportException($"Failed to load asset with key: {addressableKey}");
            return null;
        }
    }
}
