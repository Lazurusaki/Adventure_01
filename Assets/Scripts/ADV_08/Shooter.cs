using UnityEngine;

namespace ADV_08
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private float _shootPower;

        private void OnValidate()
        {
            if (_shootPower < 0)
                _shootPower = 0;
        }

        public void Shoot(Bullet bullet, Vector3 Direction)
        {
            Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();

            if (rigidBody != null)
            {
                rigidBody.AddForce(Direction * _shootPower);
            }
            else
            {
                Debug.LogError("Rigidbody has not found");
            }
        }
    }
}
