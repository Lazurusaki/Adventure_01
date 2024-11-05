using System;
using UnityEngine;

namespace ADV_13
{
    public class ShooterCharacter : Character
    {
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private Transform _shootSocket;
        [SerializeField] private float _shootPower;
        
        private Shooter _shooter;

        public event Action Kill;
        
        public override void Initialize()
        {
            base.Initialize();
            _shooter = new Shooter(_shootPower);
        }

        private void OnBulletHit(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out Character character) == false) return;
            
            if (character.IsDead)
                Kill?.Invoke();
        }

        public void Shoot()
        {
            var bullet = Instantiate(_bulletPrefab, _shootSocket.position, Quaternion.identity, null);
            bullet.Initialize();
            bullet.Hit += OnBulletHit;
            _shooter.Shoot(bullet, transform.forward);
        }
    }
}
