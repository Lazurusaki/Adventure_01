using UnityEngine;

namespace ADV_07
{
    public class CameraRotator : MonoBehaviour
    {
        [SerializeField] private float _mouseSensetivity;
        [SerializeField] private float _minYAngle = -20;
        [SerializeField] private float _maxYAngle = 90;

        private InputDetector _inputDetector;
        private float _rotationX;
        private float _rotationY;

        public void SetInputDetector(InputDetector inputDetector)
        {
            _inputDetector = inputDetector;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void LateUpdate()
        {
            if (_inputDetector != null)
            {
                _rotationX -= _inputDetector.MouseAxisInput.y * _mouseSensetivity;
                _rotationY += _inputDetector.MouseAxisInput.x * _mouseSensetivity;
                _rotationX = Mathf.Clamp(_rotationX, _minYAngle, _maxYAngle);
                transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
            }
            else
            {
                Debug.Log("InputDetector is not assigned");
            }
        }

        public float GetCamraRotationY()
        {
            return transform.rotation.eulerAngles.y;
        }
    }
}
