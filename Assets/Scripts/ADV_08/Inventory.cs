using System.Collections.Generic;
using UnityEngine;

namespace ADV_08
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int _capacity;
        [SerializeField] private Transform _socket;

        private List<Item> _items;
        private int _currentItemIndex;

        private void Awake()
        {
            _items = new List<Item>();
        }

        private void OnValidate()
        {
            if (_capacity < 0)
                _capacity = 0;
        }

        public bool TryAddItem(Item item)
        {
            if (_items.Count < _capacity)
            {
                _items.Add(item);
                _currentItemIndex = _items.Count - 1;
                item.transform.parent = _socket;
                item.transform.position = _socket.position;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool TryGetCurrentItem(out Item item)
        {
            item = null;

            if (_items.Count == 0)
                return false;

            item = _items[_currentItemIndex];
            return true;
        }

        public bool TryRemoveCurrentItem()
        {
            if (_items.Count == 0)
                return false;

            Item item = _items[_currentItemIndex];
            _items.Remove(item);
            Destroy(item.gameObject);
            return true;
        }
    }
}
