using UnityEngine;

public class EffectsSwitcher : MonoBehaviour
{
    [SerializeField] private Game _game;
    [SerializeField] private Transform _effects;
    [SerializeField] private Transform _target;

    private bool _isEffectsEnabled = false;

    private void Update()
    {
        if (_target == null || _game == null)
            return;

        HandleEffectsSwitching();
        FollowTarget();
    }

    private void DisableEffects()
    {
        _effects?.gameObject.SetActive(false);
    }

    private void EnableEffects()
    {
        _effects?.gameObject.SetActive(true);
    }

    private void FollowTarget()
    {
        transform.position = _target.transform.position;
    }

    private void HandleEffectsSwitching()
    {
        if (_game.IsTimerRunning && _isEffectsEnabled == false)
        {
            EnableEffects();
            _isEffectsEnabled = true;
        }
        else if (_game.IsTimerRunning == false && _isEffectsEnabled)
        {
            DisableEffects();
            _isEffectsEnabled = false;
        }
    }
}