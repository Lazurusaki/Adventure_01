using System;
using System.Collections;
using UnityEngine;

namespace ADV_13
{
    public class TimeSurvival : ICondition
    {
        private const float Time = 5f;

        private readonly MonoBehaviour _owner;
        
        private float _timer;

        public event Action Completed;

        public TimeSurvival(MonoBehaviour owner)
        {
            _owner = owner;
            _owner.StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            WaitForSeconds wait = new WaitForSeconds(1);

            while (_timer < Time)
            {
                _timer++;
                yield return wait;
            }

            Completed?.Invoke();
        }
    }
}