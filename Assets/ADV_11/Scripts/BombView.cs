using UnityEngine;

namespace ADV_11
{
    public class BombView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _detonateEffectPrefab;
        [SerializeField] private Light _light;
        [SerializeField] private float _flickerFrequency;
        [SerializeField] private float _maxIntensity;

        private LightFlicker _lightFlicker;

        private void OnValidate()
        {
            _flickerFrequency = Mathf.Max(0, _flickerFrequency);
            _maxIntensity = Mathf.Max(0, _maxIntensity);
        }

        private void Awake()
        {
            if (_detonateEffectPrefab == null)
                throw new System.NullReferenceException("Detonate prefab is not set");

            if (_light == null)
                throw new System.NullReferenceException("Light is not set");

            _lightFlicker = new LightFlicker(_light, _maxIntensity, _flickerFrequency);
        }

        private void Update()
        {
            if (_light.enabled == true)
            {
                _lightFlicker.update();
            }
        }

        public void Detonate()
        {
            if (_detonateEffectPrefab == null)
                return;

            Instantiate(_detonateEffectPrefab, transform.position, Quaternion.identity, null);
        }

        public void Activate()
        {
            if (_light == null)
                return;

            _light.enabled = true;
            _lightFlicker.Enable();
        }
    }
}
