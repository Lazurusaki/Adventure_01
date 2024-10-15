using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] private float _forceMin;
    [SerializeField] private float _forceMax;
    [SerializeField] private float _changeIntervalMin;
    [SerializeField] private float _changeIntervalMax;
    [SerializeField] private float _directionChangeSpeed;

    public float Force { get; private set; }
    public Vector2 Direction { get; private set; }
    private float _timer;
    private Vector2 _targetDirection;

    private void Start()
    {
        Generate();
    }

    private void Update()
    {
        if (_timer <= 0)
            Generate();

        Direction = Vector2.Lerp(Direction, _targetDirection, Time.deltaTime * _directionChangeSpeed);
        _timer -= Time.deltaTime;
    }

    private void Generate()
    {
        Force = Random.Range(_forceMin, _forceMax);
        float randomAngle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        _targetDirection = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
        _timer = Random.Range(_changeIntervalMin, _changeIntervalMax);
    }
}
