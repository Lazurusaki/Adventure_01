using Unity.VisualScripting;
using UnityEngine;

namespace ADV_11
{
    public class Game
    {
        private CharacterController _player;
        private IController _playerController;
        private IController _aiWanderer;
        private IController _currentController;
        private InputDetector _inputDetector;
        private HealthBar _healthBar;
        private HealthbarDisplayer _healthBarDisplayer;

        private float _playerInactiveTime;
        private float _timer;
        private bool _isTimerWorking;
        private bool _isInitialized;

        public Game(CharacterController player,
                    IController playerController,
                    IController aiController,
                    InputDetector inputDetector,
                    float playerInactiveTime,
                    HealthBar healthBar,
                    HealthbarDisplayer helthbarDisplayer)
        {
            _player = player;
            _playerController = playerController;
            _aiWanderer = aiController;
            _inputDetector = inputDetector;
            _playerInactiveTime = playerInactiveTime;
            _healthBar = healthBar;
            _healthBarDisplayer = helthbarDisplayer;

            _currentController = _playerController;
            ResetTimer();
        }

        private void ResetTimer()
        {
            _timer = _playerInactiveTime;
        }

        public void Update()
        {
            _inputDetector.Update();
            _currentController.Update();

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

            
            

            if (_player != null && _player.IsDeathComplete)
            {
                Object.Destroy(_player.gameObject);
                Object.Destroy(_healthBar.gameObject);
            }
        }

        public void LateUpdate()
        {
            _healthBarDisplayer.Update();
        }
    }
}
