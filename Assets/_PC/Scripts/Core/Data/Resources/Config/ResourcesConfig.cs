using Assets._PC.Scripts.Core.Data.Config;
using Assets._PC.Scripts.Core.Data.Enums;
using Assets._PC.Scripts.Core.Data.Ingredients.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Resources.Config
{
    public class ResourcesConfig : BaseConfig
    {
        public Dictionary<ResourceType, ResourceData> Resources { get; set; }
    }
}
