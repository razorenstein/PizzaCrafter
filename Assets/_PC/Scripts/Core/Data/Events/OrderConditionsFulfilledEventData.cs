using System;
using System.Collections.Generic;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class OrderConditionsFulfilledEventData : PCBaseEventData
    {
        public List<Guid> OrdersIds { get; set; }
    }
}
