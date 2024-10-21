using UnityEngine;

namespace ADV_11
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private CharacterController _playerCharacterController;
        [SerializeField] private LayerMask _groundLayer;

        private PlayerController _playerController;
        private InputDetector _inputDetector;

        private void Awake()
        {
            if (_playerCharacterController == null)
                throw new System.NullReferenceException("Player Character Controller is not set");

            if (_groundLayer == 0)
                throw new System.NullReferenceException("Ground Layer is not set");

            _inputDetector = new InputDetector();
            _playerController = new PlayerController(_playerCharacterController, _inputDetector, _groundLayer);
        }

        private void Update()
        {
            _inputDetector.Update();
            _playerController.Update();
        }
    }
}
