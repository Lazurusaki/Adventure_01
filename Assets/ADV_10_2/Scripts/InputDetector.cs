using UnityEngine;

namespace ADV_10_2
{
    public class InputDetector
    {
        const string ShipTurnAxisName = "Horizontal";
        const string SailTurnAxisName = "Vertical";

        public float ShipTurnAxis;
        public float SailTurnAxis;

        public void Update()
        {
            ShipTurnAxis = Input.GetAxisRaw(ShipTurnAxisName);
            SailTurnAxis = Input.GetAxisRaw(SailTurnAxisName);
        }
    }
}
