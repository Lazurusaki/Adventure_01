using UnityEngine;

namespace ADV_11
{
    public class BombView : MonoBehaviour
    {
        private const string EmmisiveStrengthKey = "_EmmisiveStrength";
        private const string EmmisiveColorKey = "_Color";

        [SerializeField] private ParticleSystem _detonateEffectPrefab;
        [SerializeField] private Light _light;
        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _activatedColor;
        [SerializeField] private float _activatedMaxLight;
        [SerializeField] private float _activatedMaxEmissive;
        [SerializeField] private float _activatedMaxSize;

        private EmmisiveIncreaser _emmisiveIncreaser;
        private LightIncreaser _lightIncreaser;
        private SizeIncreaser _sizeIncreaser;

        private float _detonateTime;
        private float _timer;
        private float _currentEmmisive;

        private void OnValidate()
        {
            _activatedMaxLight = Mathf.Max(0, _activatedMaxLight);
            _activatedMaxEmissive = Mathf.Max(0, _activatedMaxEmissive);
            _activatedMaxSize = Mathf.Max(0, _activatedMaxSize);
        }

        public void Initialize(float detonateTime)
        {
            if (_detonateEffectPrefab == null)
                throw new System.NullReferenceException("Detonate prefab is not set");

            if (_light == null)
                throw new System.NullReferenceException("Light is not set");

            if (_renderer == null)
                throw new System.NullReferenceException("Renderer is not set");

            _detonateTime = detonateTime;
            _emmisiveIncreaser = new EmmisiveIncreaser(this, _renderer, detonateTime, _activatedMaxEmissive, _activatedColor);
            _lightIncreaser = new LightIncreaser(this, _light, detonateTime, _activatedMaxLight);
            _sizeIncreaser = new SizeIncreaser(this, _renderer, detonateTime, _activatedMaxSize);
            _light.color = _activatedColor;
            _renderer.material.SetColor(EmmisiveColorKey, _activatedColor);
        }

        public void Detonate()
        {
            if (_detonateEffectPrefab == null)
                return;

            Instantiate(_detonateEffectPrefab, transform.position, Quaternion.identity, null);
        }

        public void Activate()
        {
            _emmisiveIncreaser.Activate();
            _lightIncreaser.Activate();
            _sizeIncreaser.Activate();
        }
    }
}
