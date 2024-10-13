using UnityEngine;

namespace ADV_10_1
{
    public class InputDetector
    {
        const int LMBKey = 0;
        const int RMBKey = 1;

        public bool IsLMBPressed;
        public bool IsLMBReleased;

        public bool IsRMBPressed;

        public Vector2 MousePosition;
        public void Update()
        {
            MousePosition = Input.mousePosition;

            IsLMBPressed = Input.GetMouseButtonDown(LMBKey);
            IsLMBReleased = Input.GetMouseButtonUp(LMBKey);
            IsRMBPressed = Input.GetMouseButtonDown(RMBKey);
        }
    }
}
