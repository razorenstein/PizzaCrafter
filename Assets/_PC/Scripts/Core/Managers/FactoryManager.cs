using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Pool;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEditor.MaterialProperty;

namespace Assets._PC.Scripts.Core.Managers
{
    public class FactoryManager 
    {   
        public async Task<List<T>> GenerateObjects<T>(string addressableKey, int amount) where T : Component
        {
            var createdObjects = new List<T>();

            var originalObject = await GenerateObjectAsync<T>(addressableKey);
            if (originalObject == null)
                return null;

            createdObjects.Add(originalObject);

            for (var i = 1; i < amount; i++)
            {
                var newCreated = GameObject.Instantiate(originalObject);
                createdObjects.Add(newCreated);
            }

            return createdObjects;
        }

        private async Task<T> GenerateObjectAsync<T>(string addressableKey) where T : Component
        {
            var handle = Addressables.InstantiateAsync(addressableKey);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var originalObject = handle.Result;
                return originalObject.GetComponent<T>();
            }

            //Manager.MonitorManager.ReportException($"Failed to load asset with key: {addressableKey}");
            return null;
        }
    }
}
