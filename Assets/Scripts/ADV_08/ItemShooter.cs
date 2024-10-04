using UnityEngine;

namespace ADV_08
{
    [RequireComponent(typeof(Shooter))]
    public class ItemShooter : Item
    {
        [SerializeField] private Bullet _bulletPrefab;

        public override void Activate(Transform owner)
        {
            if (_bulletPrefab == null)
            {
                Debug.LogError("Bullet prefab not assigned");
                return;
            }

            base.Activate(owner);
            Shooter shooter = GetComponent<Shooter>();
            Bullet bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity, null);
            shooter.Shoot(bullet, transform.parent.forward);
            Debug.Log($"Shoot activated");
        }
    }
}
