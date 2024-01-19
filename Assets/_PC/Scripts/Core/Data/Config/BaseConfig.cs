using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Config
{
    [Serializable]
    public class BaseConfig
    {
        public bool IsEnabled = true;
        public int ConfigVersion = 1;
    }
}
