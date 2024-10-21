using UnityEngine;

namespace ADV_11
{
    public class LightFlicker
    {
        private Light _light;
        private float _frequency;

        private bool _isEnabled;
        private float _amplitudeHalf;

        public LightFlicker(Light light, float maxIntensity, float frequency)
        {
            _light = light;
            _amplitudeHalf = maxIntensity / 2;
            _frequency = frequency;
        }

        public void update()
        {
            if (_isEnabled)
                _light.intensity = _amplitudeHalf * Mathf.Sin(Time.time * _frequency) + _amplitudeHalf;
        }

        public void Enable()
        {
            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
        }
    }
}
