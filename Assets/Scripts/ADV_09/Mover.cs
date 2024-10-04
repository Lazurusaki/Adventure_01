using UnityEngine;

namespace ADV_09
{
    public class Mover
    {
        private const float Speed = 2f;
        public void Move(Transform transform, Vector3 direction)
        {
            transform.Translate(direction * Speed * Time.deltaTime, Space.World);
        }
    }
}
