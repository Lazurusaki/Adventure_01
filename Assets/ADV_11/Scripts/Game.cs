using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace ADV_11
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private CharacterController _playerPrefab;
        [SerializeField] private Transform _playerStart;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private HealthbarDisplayer _healthbarDisplayer;
        [SerializeField] private NavPointDisplayer _navPointDisplayer;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _playerInactiveTime;
        [SerializeField] private float _aiWanderRadius;
        [SerializeField] private CinemachineVirtualCamera _camera;

        private CharacterController _player;
        private IController _playerController;
        private IController _aiWanderer;
        private IController _currentController;
        private InputDetector _inputDetector;

        private float _timer;
        private bool _isTimerWorking;
        private bool _isInitialized;

        private void OnValidate()
        {
            _maxHealth = Mathf.Max(0, _maxHealth);
            _playerInactiveTime = Mathf.Max(0, _playerInactiveTime);
            _aiWanderRadius = Mathf.Max(0, _aiWanderRadius);
        }

        private void Awake()
        {
            ValidateReferences();

            _inputDetector = new InputDetector();

            Health health = new Health(_maxHealth);

            _player = Instantiate(_playerPrefab, _playerStart.position, Quaternion.identity, null);
            _player.Initialize(health);

            _playerController = new PlayerCharacterController(_player, _inputDetector, _groundLayer);
            _aiWanderer = new AIWanderCharacter(_player, _aiWanderRadius);

            _healthbarDisplayer.Initialize(health, _player.HealthBarSocket);
            _navPointDisplayer.Initialize(_player.GetComponent<NavMeshAgent>());

            _currentController = _playerController;
            _camera.Follow = _player.transform;
            ResetTimer();
            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized)
            {
                _inputDetector.Update();

                if (_inputDetector.IsLMBPressed && _currentController != _playerController)
                    _currentController = _playerController;

                if (_player.IsTargetReached)
                {
                    if (_isTimerWorking == false)
                        _isTimerWorking = true;

                    _timer -= Time.deltaTime;

                    if (_timer <= 0)
                        _currentController = _aiWanderer;
                }
                else if (_isTimerWorking)
                {
                    ResetTimer();
                    _isTimerWorking = false;
                }

                _currentController.Update();
            }
        }

        private void ResetTimer()
        {
            _timer = _playerInactiveTime;
        }

        private void ValidateReferences()
        {
            if (_playerStart == null)
                throw new System.NullReferenceException("Player start is not set");

            if (_playerPrefab == null)
                throw new System.NullReferenceException("Player prefab is not set");

            if (_healthbarDisplayer == null)
                throw new System.NullReferenceException("HealthbarDisplayer is not set");

            if (_navPointDisplayer == null)
                throw new System.NullReferenceException("NavPointDisplayer is not set");

            if (_groundLayer == 0)
                throw new System.NullReferenceException("Ground Layer is not set");

            if (_camera == null)
                throw new System.NullReferenceException("Camera is not set");
        }
    }
}
