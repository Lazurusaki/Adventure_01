using UnityEngine;

namespace ADV_10_1
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Handle _handle;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private float _explosionRadius;
        [SerializeField] private float _explosionPower;
        [SerializeField] private ParticleSystem _explosionPrefab;

        private InputDetector _inputDetector;
        private HandleController _handleController;
        private ExplosionShooter _shooter;

        private void Awake()
        {
            _inputDetector = new InputDetector();

            if (_handle == null)
                throw new System.NullReferenceException("Handle is not set");

            _handleController = new HandleController(_handle, _groundLayer);

            if (_explosionPrefab == null)
                throw new System.NullReferenceException("Explosion prefab is not set");

            _shooter = new ExplosionShooter(_explosionPrefab,_explosionPower,_explosionRadius);
        }

        private void Update()
        {
            _inputDetector.Update();

            if (_handle == null)
                return;

            _handleController.Update();

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_inputDetector.IsLMBPressed)
                _handleController.TryPick(ray);

            if (_inputDetector.IsLMBReleased)
                _handleController.Release();

            if (_explosionPrefab == null)
                return;

            if (_inputDetector.IsRMBPressed)
            {
                _shooter.Shoot(ray);
            }
        }
    }
}
