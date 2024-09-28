using UnityEngine;

namespace ADV_08
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Inventory))]
    public class PlayerController : MonoBehaviour
    {
        private InputDetector _inputDetector;
        private CharacterController _characterController;
        private CharacterAnimationController _characterAnimationController;
        private Inventory _inventory;

        private Vector3 _axisDirection;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            if (_inputDetector == null)
            {
                Debug.LogError("InputDetector is not assigned");
                return;
            }

            if (_inputDetector.AxisInput.magnitude > 0)
            {
                _axisDirection = new Vector3(_inputDetector.AxisInput.x, 0, _inputDetector.AxisInput.y);
                _characterController.MoveStart(_axisDirection);
            }
            else
            {
                _characterController.MoveEnd();
            }

            if (_inputDetector.IsUsingAbility)
                ActivateItem();
        }

        private void ActivateItem()
        {
            if (_inventory.TryGetCurrentItem(out Item item))
            {
                item.Activate(transform);
                _inventory.TryRemoveCurrentItem();
            }
            else
            {
                Debug.Log("Inventory is Empty");
            }
        }

        public void SetInputDetector(InputDetector inputDetector)
        {
            _inputDetector = inputDetector;
        }
    }
}
