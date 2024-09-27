using UnityEngine;

namespace ADV_08
{
    public abstract class Item : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        public virtual void Activate(Transform owner)
        {
            if (_particleSystem != null)
                Instantiate(_particleSystem, transform.position, Quaternion.identity, transform.parent);
            else
                Debug.LogWarning("Particle System not assigned");

            Debug.Log("Item Activated");
        }
    }
}