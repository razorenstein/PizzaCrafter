﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class OrderExpiredEventData : PCBaseEventData
    {
        public Guid OrderId { get; set; }
    }
}
