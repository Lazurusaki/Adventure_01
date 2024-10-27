using System.Collections;
using UnityEngine;

namespace ADV_11
{
    public class EmmisiveFlicker
    {
        private const string EmmisiveStrengthKey = "_EmmisiveStrength";
        private const string EmmisiveColorKey = "_Color";

        private MonoBehaviour _owner;
        private Renderer _renderer;
        private float _frequency;
        private Color _color;

        private float _maxEmission = 1f;
        private float _timer;
        private float _currentFlash;
        private Coroutine _showFlashCoroutine;
        private bool _isActivated;

        public EmmisiveFlicker(MonoBehaviour owner, Renderer renderer, float frequency, Color color)
        {
            _owner = owner;
            _renderer = renderer;
            _frequency = frequency;
            _color = color;
            renderer.material.SetColor(EmmisiveColorKey, _color);
        }

        private IEnumerator Flick()
        {
            _timer = 0;
            _isActivated = true;

            while (_isActivated)
            {
                _timer += Time.deltaTime;
                _currentFlash = _maxEmission + Mathf.Sin(_frequency * _timer - Mathf.PI / 2) * _maxEmission;
                _renderer.material.SetFloat(EmmisiveStrengthKey, _currentFlash);
                yield return 0;
            }
        }

        public void Activate()
        {
            if (_showFlashCoroutine != null)
                _owner.StopCoroutine(_showFlashCoroutine);

            _showFlashCoroutine = _owner.StartCoroutine(Flick());
        }

        public void Deactivate()
        {
            _isActivated = false;
            _renderer.material.SetFloat(EmmisiveStrengthKey, 0);
        }
    }
}
