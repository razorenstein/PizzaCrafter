using _MineFest.Core.Managers;
using UnityEngine;

namespace _PizzaCrafter.Core.Components
{
    public class PCMonoBehaviour : MonoBehaviour
    {
        public PCManager Manager => PCManager.Instance;
    }
}

