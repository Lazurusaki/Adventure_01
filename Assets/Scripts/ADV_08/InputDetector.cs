using UnityEngine;

namespace ADV_08
{
    public class InputDetector
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        private const KeyCode RestartButton = KeyCode.R;
        private const KeyCode AbilityButton = KeyCode.F;

        public Vector2 AxisInput { get; private set; }
        public bool IsRestarting { get; private set; } = false;
        public bool IsUsingAbility { get; private set; } = false;

        public void Update()
        {
            AxisInput = new Vector2(Input.GetAxisRaw(HorizontalAxisName), Input.GetAxisRaw(VerticalAxisName)).normalized;

            if (Input.GetKeyDown(RestartButton))
                IsRestarting = true;
            else
                IsRestarting = false;

            if (Input.GetKeyDown(AbilityButton))
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
