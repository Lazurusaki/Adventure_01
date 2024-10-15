using UnityEngine;

namespace ADV_10_2
{
    public class ShipGame : MonoBehaviour
    {
        [SerializeField] private Ship _ship;
        [SerializeField] private Wind _wind;
        [SerializeField] private AnimationCurve _windForceCurve;

        private InputDetector _inputDetector;
        private ShipController _shipController;

        private void Awake()
        {
            if (_ship == null)
                throw new System.NullReferenceException("Ship is not set");

            if (_wind == null)
                throw new System.NullReferenceException("Wind is not set");

            _inputDetector = new InputDetector();
            _shipController = new ShipController(_ship, _wind);
        }

        private void Update()
        {
            _inputDetector.Update();
        }

        private void FixedUpdate()
        {
            _shipController.TurnShip(_inputDetector.ShipTurnAxis);
            _shipController.TurnSail(_inputDetector.SailTurnAxis);
            _shipController.ApplyWindForce();
        }
    }
}
