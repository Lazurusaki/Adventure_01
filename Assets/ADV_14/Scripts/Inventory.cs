using System;
using System.Collections.Generic;
using System.Linq;

namespace ADV_14
{
    public class Inventory
    {
        private readonly List<Item> _items;

        public IEnumerable<Item> Items => _items;

        public int CurrentSize => _items.Count;
        public int MaxSize { get; }

        public bool IsHaveSpace()
        {
            return CurrentSize < MaxSize;
        }

        public Inventory(List<Item> items, in int maxSize)
        {
            if (maxSize < items.Count)
                throw new ArgumentOutOfRangeException("MaxSize can't be less than items count");

            _items = new List<Item>(items);
            MaxSize = maxSize;
        }

        public void Add(Item item)
        {
            if (IsHaveSpace() == false)
                throw new InventoryFullException("Inventory is full");

            _items.Add(item);
        }

        public bool IsHaveEnoughItems(int id, in int count, out List<Item> items)
        {
            items = _items.Where(item => item.Id == id).Take(count).ToList();

            if (items.Count < count)
                return false;

            foreach (var item in items)
                _items.Remove(item);

            return true;
        }

        public List<Item> GetItemsById(int id, int count)
        {
            if (IsHaveEnoughItems(id, count, out var items) == false)
                throw new InvalidOperationException("No enough items");

            return items;
        }
    }

    public class Item
    {
        public readonly int Id;
    }

    public class InventoryFullException : Exception
    {
        public InventoryFullException(string message) : base(message)
        {
        }
    }
}