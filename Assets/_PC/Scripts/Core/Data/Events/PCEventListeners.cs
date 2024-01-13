using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Events
{
    public class PCEventListeners<T>
    {
        public List<Action<T>> ActionsOnInvoke;

        public PCEventListeners(Action<T> additionalData)
        {
            ActionsOnInvoke = new List<Action<T>> { additionalData };
        }
    }
}
