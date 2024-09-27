using UnityEngine;

namespace ADV_08
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private PlayerController _playerController;

        private InputDetector _inputDetector;

        private void Awake()
        {
            if (_playerController == null)
            {
                Debug.LogError("PlayerController not assigned");
                return;
            }

            _inputDetector = new InputDetector();
            _playerController.SetInputDetector(_inputDetector);
        }

        private void Update()
        {
            _inputDetector.Update();
        }
    }
}
