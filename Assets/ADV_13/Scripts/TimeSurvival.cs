using System;
using System.Collections;
using UnityEngine;

namespace ADV_13
{
    public class TimeSurvival : ICondition
    {
        private const float Time = 5f;
        private readonly MonoBehaviour _coroutineHost;
        
        private float _timer;
        private Coroutine _timerCoroutine;

        public event Action Completed;
        
        public TimeSurvival(MonoBehaviour coroutineHost)
        {
            _coroutineHost = coroutineHost;
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
        
        public void Start()
        {
            if (_coroutineHost is null)
                throw new NullReferenceException("Cant start timer, CoroutineHost is null");
            
            if (_timerCoroutine is not null)
                _coroutineHost.StopCoroutine(_timerCoroutine);
            
            _timerCoroutine = _coroutineHost.StartCoroutine(Timer());
        }

        public void Reset()
        {
            if (_timerCoroutine is not null)
                _coroutineHost.StopCoroutine(_timerCoroutine);

            _timer = 0;
        }
    }
}