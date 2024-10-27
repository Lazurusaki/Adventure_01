using System.Collections;
using UnityEngine;

public class Fader
{
    private const string FadeKey = "_Edge";

    private MonoBehaviour _owner;
    private Renderer _renderer;
    private float _duration;

    private float _timer;
    private float _currentFade;
    private Coroutine _fadeCoroutine;

    public bool IsCompleted { get; private set; }

    public Fader(MonoBehaviour owner, Renderer renderer, float duration)
    {
        _owner = owner;
        _renderer = renderer;
        _duration = duration;
    }

    private IEnumerator Fade()
    {
        IsCompleted = false;
        _timer = 0;
        _currentFade = 0;

        while (_timer < _duration)
        {
            _timer += Time.deltaTime;
            _currentFade = _timer / _duration;
            _renderer.material.SetFloat(FadeKey, _currentFade);
            yield return null;
        }

        _renderer.material.SetFloat(FadeKey, 1);
        IsCompleted= true;
    }

    public void Activate()
    {
        if (_fadeCoroutine != null)
            _owner.StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = _owner.StartCoroutine(Fade());
    }
}
