using System;
using System.Collections.Generic;
using System.Linq;

namespace ADV_14
{
    public class Inventory
    {
        private readonly List<Slot> _slots;
        
        public int ItemCount => _slots.Count(slot => slot.IsEmpty == false);
        public int MaxSize { get; }

        public bool IsHaveEmptySlot => _slots.Any(slot => slot.IsEmpty);

        public Inventory(in int maxSize)
        {
            if (maxSize < 0)
                throw new ArgumentOutOfRangeException("MaxSize can't be negative");

            _slots = new List<Slot>();
            MaxSize = maxSize;
            
            for (var i=0; i< MaxSize; i++)
                _slots.Add(new Slot());
        }

        public Inventory(List<Item> items, in int maxSize)
        {
            if (maxSize < items.Count)
                throw new ArgumentOutOfRangeException("MaxSize can't be less than items count");

            _slots  = new List<Slot>();
            MaxSize = maxSize;
            
            for (var i=0; i< MaxSize; i++)
                _slots .Add(new Slot(items[i]));
        }

        public void Add(Item item)
        {
            if (IsHaveEmptySlot == false)
                throw new InventoryFullException("Inventory is full");

            _slots.First(slot => slot.IsEmpty).AddItem(item);
        }
        

        public List<Item> GetItemsById(int id, int count)
        {
            var items = _slots.Where(slot => slot.Item.Id == id).Take(count).Select(slot => slot.Item).ToList();
            
            if (items.Count < count)
                throw new InvalidOperationException("Not enough items");

            return items;
        }

        public void Clear()
        {
            _slots.Where(slot => slot.IsEmpty == false).ToList().ForEach(slot => slot.RemoveItem());
        }
    }
    
    public class Slot
    {
        public Item Item { get; private set; }
        
        public bool IsEmpty => Item is not null;
        
        public Slot()
        {
            Item = null;
        }

        public Slot(Item item)
        {
            Item = item;
        }

        public void AddItem(Item item)
        {
            if (IsEmpty == false)
                throw new InvalidOperationException("Slot is not empty");
            
            Item = item;
        }
        
        public void RemoveItem()
        {
            if (IsEmpty)
                throw new InvalidOperationException("Slot is already empty");
            
            Item = null;
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