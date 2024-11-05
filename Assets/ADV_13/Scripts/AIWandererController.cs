using System.Collections;
using UnityEngine;

namespace ADV_13
{
    public class AIWandererController
    {
        private readonly float _changeDirectionFrequency;
        private readonly Character _character;
        private float _timer;
        private Coroutine _wandererCoroutine;

        private bool _isActivated;

        public AIWandererController(Character character, float changeDirectionFrequency)
        {
            _changeDirectionFrequency = changeDirectionFrequency;
            _character = character;
        }

        private void OnDied()
        {
            _isActivated = false;
            _character.Died -= OnDied;
        }

        private IEnumerator MoveToDirection()
        {
            WaitForSeconds wait = new WaitForSeconds(_changeDirectionFrequency);

            while (_isActivated)
            {
                Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
                _character.Move(randomDirection);
                _character.StartRotation(randomDirection);
                _character.Died += OnDied;
                yield return wait;
            }
        }

        public void Activate()
        {
            _isActivated = true;

            if (_wandererCoroutine is not null)
                _character.StopCoroutine(_wandererCoroutine);

            _wandererCoroutine = _character.StartCoroutine(MoveToDirection());
        }

        public void Deactivate()
        {
            _isActivated = false;
        }
    }
}