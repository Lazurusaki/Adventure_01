using System.Collections;
using UnityEngine;

namespace ADV_11
{
    public class EmmisiveIncreaser
    {
        private const string EmmisiveStrengthKey = "_EmmisiveStrength";
        private const string EmmisiveColorKey = "_Color";

        private MonoBehaviour _owner;
        private Renderer _renderer;
        private float _duration;
        private float _maxEmissive;

        private float _timer;
        private float _currentEmmisive;
        private Coroutine _emmiseveIncreaserCoroutine;

        public EmmisiveIncreaser(MonoBehaviour owner, Renderer renderer, float duration, float maxEmmisive, Color color)
        {
            _owner = owner;
            _renderer = renderer;
            _duration = duration;
            _maxEmissive = maxEmmisive;
            renderer.material.SetColor(EmmisiveColorKey, color);
        }

        private IEnumerator EmissiveIncrease()
        {
            _timer = 0;

            while (_timer < _duration)
            {
                _timer += Time.deltaTime;
                _currentEmmisive = _timer / _duration * _maxEmissive;
                _renderer.material.SetFloat(EmmisiveStrengthKey, _currentEmmisive);
                yield return null;
            }
        }

        public void Activate()
        {
            if (_emmiseveIncreaserCoroutine != null)
                _owner.StopCoroutine(_emmiseveIncreaserCoroutine);

            _emmiseveIncreaserCoroutine = _owner.StartCoroutine(EmissiveIncrease());
        }
    }
}
