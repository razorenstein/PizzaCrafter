using Assets._PC.Scripts.Core.Components;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._PC.Scripts.Gameplay.Componenets.Orders
{
    public class TimerComponent : PCMonoBehaviour
    {
        private Guid _orderId;
        [SerializeField]
        private Slider _timerSlider;

        public void Initialize(Guid orderId, int durationInSeconds)
        {
            _orderId = orderId;
            if (!gameObject.activeInHierarchy)
            {
                Debug.LogWarning("GameObject is inactive! Coroutine won't start.");
                return; // Exit the method to avoid attempting to start the coroutine
            }
            StartTimer(durationInSeconds);
        }

        private void StartTimer(int durationInSeconds)
        {
            ResetSlider();
            StartCoroutine(RunTimer(durationInSeconds));
        }

        private IEnumerator RunTimer(int durationInSeconds)
        {
            float elapsedTime = 0f;

            while (elapsedTime < durationInSeconds)
            {
                elapsedTime += Time.deltaTime;
                UpdateSlider(elapsedTime / durationInSeconds);
                yield return null;
            }

            TimerComplete();
        }

        private void ResetSlider()
        {
            if (_timerSlider != null)
                _timerSlider.value = 0f;        
        }

        private void UpdateSlider(float value)
        {
            if (_timerSlider != null)          
                _timerSlider.value = value;          
        }

        private void TimerComplete()
        {
            UpdateSlider(1f);
            Manager.OrdersManager.RemoveExpiredOrder(_orderId);
        }
    }
}
