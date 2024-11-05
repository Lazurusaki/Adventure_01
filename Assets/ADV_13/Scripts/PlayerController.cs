using UnityEngine;

namespace ADV_13
{
    public class PlayerController
    {
        private Character _character;

        private bool _isEnabled;

        public PlayerController(InputDetector inputDetector)
        {
            inputDetector.AxisChanged += OnAxisInputChanged;
            inputDetector.ShootPressed += OnShoot;
        }

        private void OnDied()
        {
            Disable();
        }

        private void OnAxisInputChanged(Vector2 axis)
        {
            if (_character is null)
                throw new System.NullReferenceException("Character is null");

            if (_isEnabled == false)
                return;

            var direction = new Vector3(axis.x, 0, axis.y);
            _character.Move(direction);

            if (axis != Vector2.zero)
                _character.StartRotation(direction);
            else
                _character.StopRotation();
        }

        private void OnShoot()
        {
            if (_character is null)
                throw new System.NullReferenceException("Character is null");

            if (_isEnabled && _character is ShooterCharacter shooter)
                shooter.Shoot();
        }
        
        public void SetCharacter(Character character)
        {
            _character = character;
            _character.Died += OnDied;
        }

        public void Enable()
        {
            if (_isEnabled == false)
                _isEnabled = true;
        }

        public void Disable()
        {
            if (_isEnabled)
            {
                _isEnabled = false;
                _character.Disable();
            }
        }
    }
}