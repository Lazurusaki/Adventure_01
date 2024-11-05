using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace ADV_13
{
    public class LookRotator
    {
        private const float RotationDeadZone = 0.1f;
        private readonly Rigidbody _rigidbody;
        private readonly float _speed;
        private readonly MonoBehaviour _owner;
        
        private Coroutine _rotationCoroutine;

        public LookRotator(Rigidbody rigidbody, float speed)
        {
            _owner = rigidbody.GetComponent<MonoBehaviour>();
            _rigidbody = rigidbody;
            _speed = speed;
        }

        public void StartRotation(Vector3 direction)
        {
            if (_rotationCoroutine is not null)
                
                _owner.StopCoroutine(_rotationCoroutine);
            
            _rotationCoroutine = _owner.StartCoroutine(Rotate(direction));
        }

        public void StopRotation()
        {
            if (_rotationCoroutine is not null)
                _owner.StopCoroutine(_rotationCoroutine);
        }

        private IEnumerator Rotate(Vector3 direction)
        {
            var wait = new WaitForFixedUpdate();
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            //Debug.Log(_rigidbody.transform.position + "   " + _rigidbody.rotation.eulerAngles + "   " + targetRotation.eulerAngles);
            
            while (Quaternion.Angle(_rigidbody.rotation, targetRotation) > RotationDeadZone)
            {
                
                _rigidbody.rotation = Quaternion.RotateTowards(_rigidbody.rotation, targetRotation, _speed);
                yield return wait;
            }
            
            _rigidbody.rotation = targetRotation;
        }
    }
}