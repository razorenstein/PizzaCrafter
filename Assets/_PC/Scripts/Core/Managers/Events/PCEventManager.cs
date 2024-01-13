using Assets._PC.Scripts.Core.Data.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Managers.Events
{
    public class PCEventManager<T>
    {
        private Dictionary<PCEventType, PCEventListeners<T>> _eventTypeToListenersData = new();

        public void AddListener(PCEventType eventType, Action<T> additionalData)
        {
            if (_eventTypeToListenersData.TryGetValue(eventType, out var value))
                value.ActionsOnInvoke.Add(additionalData);
            else
                _eventTypeToListenersData[eventType] = new PCEventListeners<T>(additionalData);
        }

        public void RemoveListener(PCEventType eventType, Action<T> actionToRemove)
        {
            if (!_eventTypeToListenersData.TryGetValue(eventType, out var value))
                return;
            
            value.ActionsOnInvoke.Remove(actionToRemove);

            if (!value.ActionsOnInvoke.Any())
                _eventTypeToListenersData.Remove(eventType);
        }

        public void InvokeEvent(PCEventType eventType, T dataToInvoke)
        {
            if (!_eventTypeToListenersData.TryGetValue(eventType, out var value))
                return;

            foreach (var method in value.ActionsOnInvoke)
                method.Invoke(dataToInvoke);
        }
    }
}
