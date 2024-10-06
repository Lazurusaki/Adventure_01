using UnityEngine;

namespace ADV_09
{
    public class Mover : MonoBehaviour
    {
        private const float Speed = 2f;
        public void Move(Vector3 direction)
        {
            transform.Translate(direction * Speed * Time.deltaTime, Space.World);
        }
    }
}
