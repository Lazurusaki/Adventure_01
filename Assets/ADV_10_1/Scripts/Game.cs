using UnityEngine;

namespace ADV_10_1
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Handle _handle;
        [SerializeField] private LayerMask _draggableLayer;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Bomb _bombPrefab;

        private InputDetector _inputDetector;
        private MouseHitDetector _mouseHitDetector;
        private BombSpawner _bombSpawner;
        private HandleController _handleController;

        private void Awake()
        {
            _inputDetector = new InputDetector();
            _mouseHitDetector = new MouseHitDetector();

            if (_handle == null)
                throw new System.NullReferenceException("Handle is not set");

            _handleController = new HandleController(_handle, _mouseHitDetector, _draggableLayer, _groundLayer);

            if (_bombPrefab == null)
                throw new System.NullReferenceException("Bomb is not set");

            _bombSpawner = new BombSpawner(_bombPrefab);
        }

        private void Update()
        {
            _inputDetector.Update();

            if (_handle == null)
                return;

            _handleController.Update();

            if (_inputDetector.IsLMBPressed)
                _handleController.TryPick();

            if (_inputDetector.IsLMBReleased)
                _handleController.Release();

            if (_bombPrefab == null)
                return;

            if (_inputDetector.IsRMBPressed)
            {
                if (_mouseHitDetector.TryHit(_groundLayer, out RaycastHit hit))
                    _bombSpawner.Spawn(hit.point);
            }
        }
    }
}
