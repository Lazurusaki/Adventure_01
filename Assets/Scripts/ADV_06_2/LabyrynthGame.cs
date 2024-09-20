using UnityEngine;

[RequireComponent (typeof(PlatformRotator))]
public class LabyrynthGame : MonoBehaviour
{
    [SerializeField] private PlatformRotator _rotator;
    [SerializeField] private Transform _ball;
    [SerializeField] private Transform _defaultBallTransform;

    private InputDetector _inputDetector;

    private void Awake()
    {
        //_inputDetector = new InputDetector();
    }

    private void Update()
    {
        _inputDetector.Update();
        _rotator.Rotate(_inputDetector.AxisInput);

        if (_inputDetector.IsRestarting)
            Restart();
    }

    private void Restart()
    {
        _rotator.ResetRotation();
        _ball.position = _defaultBallTransform.position;  
    }
}
