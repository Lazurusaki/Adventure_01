using System.Collections.Generic;
using UnityEngine;

namespace ADV_13
{
    public class CharacterPool
    {
        private readonly List<Transform> _transforms;

        public CharacterPool(CharacterFactory characterFactory)
        {
            _transforms = new List<Transform>();
            characterFactory.CharacterSpawned += character => _transforms.Add(character.transform);
        }
        
        public void Clear(Transform transform)
        {
            if (_transforms.Contains(transform))
                _transforms.Remove(transform);

            Object.Destroy(transform.gameObject);
        }

        public void ClearAll()
        {
            foreach (var transform in _transforms)
            {
                Object.Destroy(transform.gameObject);
            }

            _transforms.Clear();
        }
    }
}