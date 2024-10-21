using UnityEngine;

namespace ADV_11
{
    public class PlayerController
    {
        private CharacterController _characterController;
        private InputDetector _inputDetector;
        private LayerMask _groundLayer;

        private float maxRayDistance = 100f;

        public PlayerController(CharacterController playerCharacterController, InputDetector inputDetector, LayerMask groundLayer)
        {
            _characterController = playerCharacterController;
            _inputDetector = inputDetector;
            _groundLayer = groundLayer;
        }

        public void Update()
        {
            if (_inputDetector.IsLMBPressed)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, maxRayDistance, _groundLayer))
                {
                    _characterController.MoveTo(hit.point);
                }
            }
        }
    }
}
