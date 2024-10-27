using UnityEngine;
using UnityEngine.EventSystems;

namespace ADV_11
{
    public class PlayerCharacterController : IController
    {
        private CharacterController _characterController;
        private InputDetector _inputDetector;
        private LayerMask _groundLayer;

        private float maxRayDistance = 100f;

        public PlayerCharacterController(CharacterController CharacterController, InputDetector inputDetector, LayerMask groundLayer)
        {
            _characterController = CharacterController;
            _inputDetector = inputDetector;
            _groundLayer = groundLayer;
        }

        public void Update()
        {
            if (_inputDetector.IsLMBPressed && EventSystem.current.IsPointerOverGameObject() == false)
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
