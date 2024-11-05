using UnityEngine;

namespace ADV_13
{
    public class Mover
    {
        private readonly Rigidbody _rigidbody;

        public Mover(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
        }

        public void Move(Vector3 velocity)
        {
            if (_rigidbody is null)
                throw new System.NullReferenceException("Rigidbody must be not null");
            
            _rigidbody.velocity = velocity;
        }
    }
}