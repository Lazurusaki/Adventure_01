using UnityEngine;

namespace ADV_08
{
    public class InputDetector
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        private const KeyCode RestartButton = KeyCode.R;
        private const KeyCode UseAbilityButton = KeyCode.F;

        private float _axisInputDeadZone = 0.01f;

        public Vector2 AxisInput { get; private set; }
        public bool IsRestarting { get; private set; } = false;
        public bool IsUsingAbility { get; private set; } = false;

        private void DetectAxisInput()
        {
            AxisInput = new Vector2(Input.GetAxisRaw(HorizontalAxisName), Input.GetAxisRaw(VerticalAxisName)).normalized;
            AxisInput = AxisInput.magnitude > _axisInputDeadZone ? AxisInput : Vector2.zero;
        }

        public void Update()
        {
            DetectAxisInput();

            if (Input.GetKeyDown(RestartButton))
                IsRestarting = true;
            else
                IsRestarting = false;

            if (Input.GetKeyDown(UseAbilityButton))
                IsUsingAbility = true;
            else
                IsUsingAbility = false;
        }

        public KeyCode GetRestartKey()
        {
            return RestartButton;
        }
    }
}
