using System;
using System.Collections;
using UnityEngine;

namespace ADV_13
{
    public class TimeSurvival : ICondition
    {
        private const float Time = 5f;
        
        private float _timer;

        public event Action Completed;
        
        public void Initialize(MonoBehaviour coroutineHost)
        {
            coroutineHost.StartCoroutine(Timer());
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