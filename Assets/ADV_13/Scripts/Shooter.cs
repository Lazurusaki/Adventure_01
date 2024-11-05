using UnityEngine;

namespace ADV_13
{
    public class Shooter
    {
        private readonly float _power;

        public Shooter(float power)
        {
            _power = power;
        }

        public void Shoot(Bullet bullet, Vector3 direction)
        {
            bullet.GetComponent<Rigidbody>().velocity = direction * _power;
        }
    }
}