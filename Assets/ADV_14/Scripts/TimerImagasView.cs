using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

namespace ADV_14
{
    public class TimerImagasView : MonoBehaviour
    {
        [SerializeField] private RectTransform _origin;
        [SerializeField] private Image _imagePrefab;
        [SerializeField, Range(1, 10)] private int _maxColumns;
        [SerializeField, Range(0, 20)] private float _spacing;

        private Timer _timer;
        private List<Image> _images;
        private int _lastImageIndex;
        private bool _isInitialized;

        public void Initialize(Timer timer)
        {
            if (timer.Duration < 0)
                throw new ArgumentOutOfRangeException("Max value can't be negative");

            _timer = timer;

            _images = new List<Image>();

            var imageSize = _imagePrefab.GetComponent<RectTransform>().rect.width;
            Vector3 position = _origin.position;

            for (int i = 0; i < (int)timer.Duration; i++)
            {
                if (i % _maxColumns == 0)
                    position = new Vector3(_origin.position.x, position.y - imageSize - _spacing, position.z);

                var image = Instantiate(_imagePrefab, position, quaternion.identity, _origin);
                _images.Add(image);

                position = new Vector3(position.x + imageSize + _spacing, position.y, position.z);
            }

            _lastImageIndex = _images.Count - 1;

            _timer.Tick += Update;
            _timer.Stopped += Hide;
            _timer.Started += Show;

            _isInitialized = true;

            Hide();
        }

        public void Deinitialize()
        {
            CheckInitialized();

            _timer.Tick -= Update;
            _timer.Stopped -= Hide;
            _timer.Started -= Show;

            _isInitialized = false;

            Hide();
        }

        private void CheckInitialized()
        {
            if (_isInitialized == false)
                throw new InvalidOperationException("View is not initialized yet");
        }

        private void Update(float value)
        {
            if (value < _lastImageIndex)
                _images[_lastImageIndex--].gameObject.SetActive(false);
        }

        public void Show()
        {
            CheckInitialized();

            foreach (var image in _images)
            {
                image.gameObject.SetActive(true);
                _lastImageIndex = _images.Count - 1;
            }
        }

        public void Hide()
        {
            CheckInitialized();

            foreach (var image in _images)
            {
                image.gameObject.SetActive(false);
            }
        }
    }
}