using System;
using System.Collections;
using UnityEngine;

namespace ADV_13
{
    public class InputDetector
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        private const KeyCode ShootKeyKeyboard = KeyCode.F;
        private const KeyCode ShootKeyGamepad = KeyCode.Joystick1Button0;

        private float _xAxis;
        private float _yAxis;

        public event Action<Vector2> AxisChanged;
        public event Action ShootPressed;
        
        public InputDetector(MonoBehaviour owner)
        {
            owner.StartCoroutine(Update());
        }

        private IEnumerator Update()
        {
            while (true)
            {
                float xAxis = Input.GetAxisRaw(HorizontalAxisName);
                float yAxis = Input.GetAxisRaw(VerticalAxisName);

                if (xAxis != _xAxis || yAxis != _yAxis)
                {
                    _xAxis = xAxis;
                    _yAxis = yAxis;
                    AxisChanged?.Invoke(new Vector2(xAxis, yAxis).normalized);
                }

                if (Input.GetKeyDown(ShootKeyKeyboard) || Input.GetKeyDown(ShootKeyGamepad))
                    ShootPressed?.Invoke();
                
                yield return null;
            }
        }
    }
}