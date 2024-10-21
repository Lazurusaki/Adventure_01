using UnityEngine;

namespace ADV_11
{
    public class InputDetector
    {
        const int LMBKey = 0;

        public bool IsLMBPressed;

        public void Update()
        {
            IsLMBPressed = Input.GetMouseButtonDown(LMBKey);
        }
    }
}
