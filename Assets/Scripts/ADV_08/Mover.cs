using UnityEngine;

namespace ADV_08
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void OnValidate()
        {
            if (_speed < 0)
                _speed = 0;
        }

        public void Move(Vector3 direction)
        {
            transform.Translate(direction * _speed * Time.deltaTime, Space.World);
        }

        public void AddSpeed(float speed)
        {
            _speed = Mathf.Max(0, _speed + speed);
        }
    }
}