using System;
using UnityEngine;
using UnityEngine.UI;

namespace ADV_14
{
    public class TimerSliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;

        public void SetMaxValue(float value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException("Max value can't be negative");

            _slider.maxValue = value;
        }

        public void Show()
        {
            if (_slider.gameObject.activeSelf == false)
                _slider.gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (_slider.gameObject.activeSelf)
                _slider.gameObject.SetActive(false);
        }

        public void SetValue(float value)
        {
            if (value > _slider.maxValue)
                throw new ArgumentOutOfRangeException("Value can't be greater than max value");

            _slider.value = value;
        }
    }
}