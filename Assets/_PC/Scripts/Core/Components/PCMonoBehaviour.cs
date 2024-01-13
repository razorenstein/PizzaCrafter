using Assets._PC.Scripts.Core.Managers;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Components
{
    public class PCMonoBehaviour : MonoBehaviour
    {
        public PCManager Manager => PCManager.Instance;
    }
}

