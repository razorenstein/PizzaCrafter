using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

namespace _MineFest.Core.Managers
{
    public class PCManager
    {
        public static PCManager Instance { get; private set; }

        public PCManager()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Debug.LogError($"{nameof(PCManager)}- Only One Appearance is valid");
            }
        }
    }
}
