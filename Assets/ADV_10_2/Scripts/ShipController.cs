using System;
using UnityEngine;

public class ShipController
{
    private Ship _ship;
    private Wind _wind;
    private AnimationCurve _windForceCurve;
    private float _axisDeadZone = 0.01f;
    private float _hullWindAffectMltiplier = 0.5f;
    private float _SailWindAffectMultiplier = 1f;

    public ShipController(Ship ship, Wind wind)
    {
        _ship = ship;
        _wind = wind;
    }

    public void ApplyWindForce()
    {
        Vector2 shipDidercion = new Vector2(_ship.transform.forward.x, _ship.transform.forward.z);

        float sailDotProduct = Vector2.Dot(_ship.GetSailDirection(), _wind.Direction);
        sailDotProduct = sailDotProduct > 0 ? sailDotProduct : 0;

        float hullDotProduct = Vector2.Dot(_ship.GetSailDirection(), shipDidercion);
        hullDotProduct  = hullDotProduct > 0 ? hullDotProduct : 0;

        float Speed = sailDotProduct * _SailWindAffectMultiplier  * hullDotProduct * _hullWindAffectMltiplier * _wind.Force;
        _ship.Move(Speed);
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
