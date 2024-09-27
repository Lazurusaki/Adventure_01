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

        public void Shoot(Bullet bullet)
        {
            Rigidbody rigidBody = bullet.GetComponent<Rigidbody>();

            if (rigidBody != null)
            {
                rigidBody.AddForce(transform.parent.forward * _shootPower);
            }
            else
            {
                Debug.LogError("Rigidbody has not found");
            }
        }
    }
}
