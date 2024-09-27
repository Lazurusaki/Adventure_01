using UnityEngine;

namespace ADV_08
{
    [RequireComponent(typeof(Mover))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed;

        private Mover _mover;

        private void Awake()
        {
            _mover = GetComponent<Mover>();
        }

        private void LookToMoving(Vector3 direction)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            float step = _rotationSpeed * Time.deltaTime;

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
        }

        public void Move(Vector3 moveDirection)
        {
            LookToMoving(moveDirection);
            _mover.Move(moveDirection);
        }
    }
}
