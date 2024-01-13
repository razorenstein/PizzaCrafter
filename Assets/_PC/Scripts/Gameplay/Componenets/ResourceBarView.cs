using Assets._PC.Scripts.Core.Data.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using Assets._PC.Scripts.Core.Components;
using System.Collections.ObjectModel;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class ResourceBarView : PCMonoBehaviour
    {
        [SerializeField] private ResourceView _resourcePrefab;
        [SerializeField] private HorizontalLayoutGroup _layoutGroup;
        private ICollection<ResourceView> _resources;

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            _resources = new Collection<ResourceView>();
            InitializeBar();
        }

        private void InitializeBar()
        {
            var resources = Manager.ResourceManager.Resources;
            foreach (var resourceData in resources.Values)
            {
                var resource = Instantiate(_resourcePrefab, _layoutGroup.transform);
                resource.Initialize(resourceData);
                _resources.Add(resource);
            }
        }
    }
}
