using UnityEngine;

[RequireComponent(typeof(CharacterInput))]

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _targetMinDistance;
    [SerializeField] private float _targetMaxDistance;

    private CharacterInput _characterInput;

    private Vector3 _currentTargetPosition;
    private float _targetDeadZone = 0.04f;

    public bool IsWorking { get; set; }

    private void Awake()
    {
        _characterInput = GetComponent<CharacterInput>();
    }

    private void Start()
    {
        DefineNewTarget();
    }

    private void Update()
    {
        if (IsWorking == false)
            return;

        if (IsTargetAquired())
        {
            DefineNewTarget();
        }

        UpdateInput();
    }

    private void DefineNewTarget()
    {
        float distance = Random.Range(_targetMinDistance, _targetMaxDistance);
        float rotation = Random.rotation.eulerAngles.y;

        _currentTargetPosition = new Vector3(Mathf.Sin(rotation * Mathf.Deg2Rad) * distance,
                                          0, Mathf.Cos(rotation * Mathf.Deg2Rad) * distance);
    }

    private bool IsTargetAquired()
    {
        return Vector3.Distance(transform.position, _currentTargetPosition) <= _targetDeadZone;
    }

    private void UpdateInput()
    {
        _characterInput.AxisInput = (_currentTargetPosition - transform.position).normalized;
    }

    public void ResetInput()
    {
        _characterInput.AxisInput = Vector3.zero;
    }
}
