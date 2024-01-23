using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Events;
using Assets._PC.Scripts.Core.Data.Pool;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Managers
{
    public class PoolManager
    {
        public Dictionary<PoolType, PoolData> Pools = new();
        private GameObject _poolsHolder;
        private static string _poolsHolderName = "PoolsHolder";

        private GameObject GetPoolsHolder() 
        {
            _poolsHolder = _poolsHolder != null ? _poolsHolder : new GameObject(_poolsHolderName);
            return _poolsHolder;
        }
        
        public async void InitPool<T>(PoolType poolType, int amount) where T : Component
        {
            
            var addressableKey = PoolTypesHelper.Map(poolType);

            var generateObjects = await PCManager.Instance.FactoryManager.GenerateObjects<T>(addressableKey, amount);

            if (generateObjects == null || !generateObjects.Any())
            {
                //PCManager.MonitorManager.ReportException("Failed to generate objects.");
                Debug.Log("Failed to generate objects.");
                return;
            }

            var poolHolder = new GameObject($"Pool_{addressableKey}");
            poolHolder.transform.SetParent(GetPoolsHolder().transform);

            Pools[poolType] = new PoolData(generateObjects.ToArray(), poolHolder);

            PCManager.Instance.EventManager.InvokeEvent(PCEventType.PoolReady, new PoolReadyEventData
            {
                Type = poolType,
            });
        }

        public T GetFromPool<T>(PoolType poolType) where T : Component
        {
            if (!Pools.ContainsKey(poolType))
            {
                //PCManager.MonitorManager.ReportException($"No pool initialized found with the name {poolName}");
                Debug.Log($"No pool initialized found with the name {poolType.ToString()}");
                return null;
            }

            //Returning poolable
            var availableItem = Pools[poolType].AvailableItems[0];
            Pools[poolType].AvailableItems.Remove(availableItem);
            Pools[poolType].UnAvailableItems.Add(availableItem);

            return (T)availableItem;
        }

        public void ReturnToPool<T>(PoolType poolType, T returnedObject) where T : Component
        {
            returnedObject.transform.SetParent(GetPoolsHolder().transform);
            Pools[poolType].AvailableItems.Add(returnedObject);
            Pools[poolType].UnAvailableItems.Remove(returnedObject);

            returnedObject.gameObject.SetActive(false);
        }
    }
}
