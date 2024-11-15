using System;
using System.Collections;
using UnityEngine;

namespace ADV_14
{
    public class Timer
    {
        private readonly MonoBehaviour _coroutineHost;
        private readonly WaitForSeconds _waitInterval;
        
        private Coroutine _timerCoroutine;
        private bool _isPaused;
        
        public float Duration { get; }

        public event Action Started;
        public event Action Stopped;
        public event Action<float> Tick;
        
        public Timer(MonoBehaviour coroutineHost, float duration)
        {
            ValidateDuration(duration);

            _coroutineHost = coroutineHost;
            Duration = duration;
            _waitInterval = new WaitForSeconds(Time.fixedDeltaTime);
        }

        private IEnumerator TimerCoroutine()
        {
            var time = Duration;
            _isPaused = false;

            Started?.Invoke();

            while (time > 0)
            {
                while (_isPaused)
                {
                    yield return null;
                }

                time -= Time.fixedDeltaTime;
                Tick?.Invoke(time);

                yield return _waitInterval;
            }

            Stop();
        }

        private void ValidateDuration(float time)
        {
            if (time < 0)
                throw new ArgumentOutOfRangeException("Time can't be negative");
        }

        public void Start()
        {
            if (_timerCoroutine is not null)
            {
                Debug.Log("Timer already running");
                return;
            }

            _timerCoroutine = _coroutineHost.StartCoroutine(TimerCoroutine());
            Started?.Invoke();
        }

        public void Stop()
        {
            if (_timerCoroutine is null)
            {
                Debug.Log("Timer is not running");
                return;
            }

            _coroutineHost.StopCoroutine(_timerCoroutine);
            _timerCoroutine = null;

            Stopped?.Invoke();
        }

        public void Pause()
        {
            if (_timerCoroutine is null)
            {
                Debug.Log("No paused timer found");
                return;
            }

            if (_isPaused)
                Debug.Log("Timer already paused");
            else
                _isPaused = true;
        }

        public void Resume()
        {
            if (_timerCoroutine is null)
            {
                Debug.Log("No paused timer found");
                return;
            }

            if (_isPaused == false)
                Debug.Log("Timer already running");
            else
                _isPaused = false;
        }
    }
}