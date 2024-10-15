using System;
using UnityEngine;

public class ShipController
{
    private Ship _ship;
    private Wind _wind;
    private AnimationCurve _windForceCurve;
    private float _axisDeadZone = 0.01f;
    private float _hullWindAffectMltiplier = 0.2f;

    public ShipController(Ship ship, Wind wind)
    {
        _ship = ship;
        _wind = wind;
    }

    private float CalculateWindAffectMultiplier(Vector2 direction)
    {
        float dotProduct = Vector2.Dot(direction, _wind.Direction);
        float degree = Mathf.Acos(dotProduct / (direction.magnitude * _wind.Direction.magnitude)) * Mathf.Rad2Deg;
        return Mathf.InverseLerp(180, 0, degree);
    }
    public void ApplyWindForce()
    {
        Vector2 shipCurrentDidercion = new Vector2(_ship.transform.forward.x, _ship.transform.forward.z);
        Vector2 hullVelocity = _wind.Direction * _wind.Force * CalculateWindAffectMultiplier(shipCurrentDidercion) * _hullWindAffectMltiplier;
        Vector2 SailVelocity = shipCurrentDidercion * _wind.Force * CalculateWindAffectMultiplier(_ship.GetSailDirection());
        _ship.Move(hullVelocity + SailVelocity);
        _ship.SetFlagDirection(_wind.Direction);
    }

    public void TurnShip(float axis)
    {
        if (MathF.Abs(axis) > _axisDeadZone)
            _ship.Turn(axis);
    }

    public void TurnSail(float axis)
    {
        if (MathF.Abs(axis) > _axisDeadZone)
            _ship.TurnSail(axis);
    }
}
