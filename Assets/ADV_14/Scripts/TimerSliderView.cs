using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace ADV_14
{
    public class TimerSliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        private Timer _timer;
        private bool _isInitialized;

        public void Initialize(Timer timer)
        {
            if (timer.Duration < 0)
                throw new ArgumentOutOfRangeException("Max value can't be negative");

            _timer = timer;
            _slider.maxValue = timer.Duration;
            
            _timer.Started += Show;
            _timer.Stopped += Hide;
            _timer.Tick += SetValue;

            _isInitialized = true;
        }

        public void Deinitialize()
        {
            CheckInitialized();
            
            _timer.Started -= Show;
            _timer.Stopped -= Hide;
            _timer.Tick -= SetValue;
            
            _isInitialized = false;
            
            Hide();
        }
        
        private void CheckInitialized()
        {
            if (_isInitialized == false)
                throw new InvalidOperationException("View is not initialized yet");
        }
        
        private void SetValue(float value)
        {
            if (value > _slider.maxValue)
                throw new ArgumentOutOfRangeException("Value can't be greater than max value");

            _slider.value = value;
        }
        
        public void Show()
        {
            CheckInitialized();
            
            if (_slider.gameObject.activeSelf == false)
                _slider.gameObject.SetActive(true);
        }

        public void Hide()
        {
            CheckInitialized();
            
            if (_slider.gameObject.activeSelf)
                _slider.gameObject.SetActive(false);
        }
    }
}