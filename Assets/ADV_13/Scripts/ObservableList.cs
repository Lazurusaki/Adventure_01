using System;
using System.Collections.Generic;

namespace  ADV_13
{
    public class ObservableList<T>
    {
        public event Action<int> Changed;
        
        private readonly List<T> _elements = new();
        
        public void Add(T element)
        {
            _elements.Add(element);
            Changed?.Invoke(_elements.Count);
        }

        public void Remove(T element)
        {
            _elements.Remove(element);
            Changed?.Invoke(_elements.Count);
        }

        public void Clear()
        {
            _elements.Clear();
            Changed?.Invoke(0);
        }
    }
}


