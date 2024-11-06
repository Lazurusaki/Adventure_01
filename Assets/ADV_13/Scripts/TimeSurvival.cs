using System;
using System.Collections;
using UnityEngine;

namespace ADV_13
{
    public class TimeSurvival : ICondition
    {
        private const float Time = 5f;

        private Coroutine _timerCoroutine;
        private float _timer;

        public event Action Completed;
        
        
        
        public void Initialize(MonoBehaviour coroutineHost)
        {
            if (_timerCoroutine is not null)
                coroutineHost.StopCoroutine(_timerCoroutine);
            
            _timer = 0;
            
            _timerCoroutine = coroutineHost.StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            var wait = new WaitForSeconds(1);

            while (_timer < Time)
            {
                _timer++;
                yield return wait;
            }

            Completed?.Invoke();
        }
    }
}