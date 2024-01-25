using Assets._PC.Scripts.Core.Data.Config;
using Assets._PC.Scripts.Core.Data.Enums;
using System.Collections.Generic;

namespace Assets._PC.Scripts.Core.Data.Oven
{
    public class OvensConfig : BaseConfig
    {
        public Dictionary<OvenType, OvenData> Ovens;
    }
}
