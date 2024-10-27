using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace ADV_11
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private CharacterController _playerPrefab;
        [SerializeField] private Transform _playerStart;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private HealthBar _healthbarPrefab;
        [SerializeField] private NavPointDisplayer _navPointDisplayer;
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _playerInactiveTime;
        [SerializeField] private float _aiWanderRadius;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private Transform _bombsContainer;
        [SerializeField] private Bomb _bombPrefab;
        [SerializeField] private AudioSource _bombsAudioSource;

        private Game _game;
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

            InputDetector inputDetector = new InputDetector();
            Health health = new Health(_maxHealth);

            CharacterController player = Instantiate(_playerPrefab, _playerStart.position, Quaternion.identity, null);
            player.Initialize(health);

            IController playerController = new PlayerCharacterController(player, inputDetector, _groundLayer);
            IController aiWanderer = new AIWanderCharacter(player, _aiWanderRadius);

            HealthBar healthbar = Instantiate(_healthbarPrefab, Vector3.zero, Quaternion.identity, null);
            HealthbarDisplayer healthbarDisplayer = new HealthbarDisplayer(healthbar, health, player.HealthBarSocket);

            _navPointDisplayer.Initialize(player.GetComponent<NavMeshAgent>());

            _game = new Game(player, playerController, aiWanderer, inputDetector, _playerInactiveTime, healthbar, healthbarDisplayer);
     
            _camera.Follow = player.transform;

            BombsSpawner bombsSpawner = new BombsSpawner(_bombsContainer, _bombPrefab, _bombsAudioSource);
            bombsSpawner.SpawnBombs();

            _isInitialized = true;
        }

        private void Update()
        {
            if (_isInitialized)
                _game.Update();
        }

        private void ValidateReferences()
        {
            if (_playerStart == null)
                throw new System.NullReferenceException("Player start is not set");

            if (_playerPrefab == null)
                throw new System.NullReferenceException("Player prefab is not set");

            if (_healthbarPrefab == null)
                throw new System.NullReferenceException("HealthBarPrefab is not set");

            if (_navPointDisplayer == null)
                throw new System.NullReferenceException("NavPointDisplayer is not set");

            if (_groundLayer == 0)
                throw new System.NullReferenceException("Ground Layer is not set");

            if (_camera == null)
                throw new System.NullReferenceException("Camera is not set");

            if (_bombsContainer == null)
                throw new System.NullReferenceException("Bombs is not set");

            if (_bombPrefab == null)
                throw new System.NullReferenceException("Bomb prefab is not set");

            if (_bombsAudioSource == null)
                throw new System.NullReferenceException("Bombs audiosource is not set");
        }
    }
}
