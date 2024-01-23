using Assets._PC.Scripts.Core.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Data.Pool
{
    public class PoolData
    {
        public List<Component> TotalItems;
        public List<Component> AvailableItems;
        public List<Component> UnAvailableItems;

        public GameObject PoolHolder;

        public PoolData(Component[] generateObjects, GameObject poolHolder)
        {
            TotalItems = generateObjects.ToList();
            AvailableItems = generateObjects.ToList();
            UnAvailableItems = new List<Component>();

            PoolHolder = poolHolder;

            foreach (var generateObject in generateObjects)
            {
                generateObject.transform.SetParent(PoolHolder.transform);
                generateObject.gameObject.SetActive(false);
            }
        }

        public void AddNewToPool<T>(T generateObject) where T : Component
        {
            TotalItems.Add(generateObject);
            AvailableItems.Add(generateObject);

            generateObject.transform.SetParent(PoolHolder.transform);
            generateObject.gameObject.SetActive(false);
        }
    }
}
