using Assets._PC.Scripts.Core.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class PoolReadyEventData : PCBaseEventData
    {
        public PoolType Type { get; set; }
    }
}
