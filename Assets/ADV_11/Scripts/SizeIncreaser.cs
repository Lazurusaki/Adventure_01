using System.Collections;
using UnityEngine;

namespace ADV_11
{
    public class SizeIncreaser
    {
        private const string SizeKey = "_Size";

        private MonoBehaviour _owner;
        private Renderer _renderer;
        private float _maxSize;
        private float _duration;

        private float _timer;
        private float _currentSize;
        private Coroutine _SizeIncreaserCoroutine;

        public SizeIncreaser(MonoBehaviour owner, Renderer renderer, float duration, float maxSize)
        {
            _owner = owner;
            _renderer = renderer;
            _duration = duration;
            _maxSize = maxSize;
        }

        private IEnumerator SizeIncrease()
        {
            float defaultSize = 1;
            _timer = 0;

            while (_timer < _duration)
            {
                _timer += Time.deltaTime;
                _currentSize = defaultSize + (_timer / _duration) * (_maxSize - defaultSize);
                _renderer.material.SetFloat(SizeKey, _currentSize);
                yield return null;
            }
        }

        public void Activate()
        {
            if (_SizeIncreaserCoroutine != null)
                _owner.StopCoroutine(_SizeIncreaserCoroutine);

            _SizeIncreaserCoroutine = _owner.StartCoroutine(SizeIncrease());
        }
    }
}
