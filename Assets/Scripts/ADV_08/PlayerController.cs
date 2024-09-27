using UnityEngine;

namespace ADV_08
{
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(CharacterAnimationController))]
    [RequireComponent(typeof(Inventory))]
    public class PlayerController : MonoBehaviour
    {
        private InputDetector _inputDetector;
        private CharacterController _characterController;
        private CharacterAnimationController _characterAnimationController;
        private Inventory _inventory;

        private float _axisInputDeadZone = 0.01f;
        private Vector3 _axisDirection;

        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            _characterAnimationController = GetComponent<CharacterAnimationController>();
            _inventory = GetComponent<Inventory>();
        }

        private void Update()
        {
            if (_inputDetector == null)
            {
                Debug.LogError("InputDetector is not assigned");
                return;
            }

            _characterAnimationController.SetMoving(isAxisActive());

            if (isAxisActive())
            {
                Move();
            }

            if (_inputDetector.IsUsingAbility)
            {
                ActivateItem();
            }
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

        private bool isAxisActive()
        {
            return _inputDetector.AxisInput.magnitude > _axisInputDeadZone;
        }

        private void Move()
        {
            _axisDirection = new Vector3(_inputDetector.AxisInput.x, 0, _inputDetector.AxisInput.y);
            _characterController.Move(_axisDirection);
        }

        public void SetInputDetector(InputDetector inputDetector)
        {
            _inputDetector = inputDetector;
        }
    }
}
