using UnityEngine;

namespace ADV_08
{
    [RequireComponent(typeof(Inventory))]
    [RequireComponent(typeof(Collider))]
    public class ItemCollector : MonoBehaviour
    {
        private Inventory _inventory;

        private void Awake()
        {
            _inventory = GetComponent<Inventory>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Item item))
            {
                if (TryCollect(item) == false)
                {
                    Debug.Log("Inventory is full");
                }
            }
        }

        private bool TryCollect(Item item)
        {
            return _inventory.TryAddItem(item);
        }
    }
}
