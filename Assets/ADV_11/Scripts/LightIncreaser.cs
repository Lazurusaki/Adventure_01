using System.Collections;
using UnityEngine;

namespace ADV_11
{
    public class LightIncreaser
    {
        private MonoBehaviour _owner;
        private Light _light;
        private float _duration;
        private float _maxLight;

        private float _timer;
        private float _currentLight;
        private Coroutine _lightIncreaserCoroutine;

        public LightIncreaser(MonoBehaviour owner, Light light, float duration, float maxLight)
        {
            _owner = owner;
            _light = light;
            _duration = duration;
            _maxLight = maxLight;
        }

        private IEnumerator EmissiveIncrease()
        {
            _timer = 0;

            while (_timer < _duration)
            {
                _timer += Time.deltaTime;
                _currentLight = _timer / _duration * _maxLight;
                _light.intensity = _currentLight;
                yield return null;
            }
        }

        public void Activate()
        {
            if (_lightIncreaserCoroutine != null)
                _owner.StopCoroutine(_lightIncreaserCoroutine);

            _lightIncreaserCoroutine = _owner.StartCoroutine(EmissiveIncrease());
        }
    }
}
