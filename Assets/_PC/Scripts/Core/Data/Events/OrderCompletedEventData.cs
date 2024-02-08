using System;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class OrderCompletedEventData : PCBaseEventData
    {
        public Guid OrderId { get; set; }
    }
}
