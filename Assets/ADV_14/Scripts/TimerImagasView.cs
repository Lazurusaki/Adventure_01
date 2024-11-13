using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class TimerImagasView : MonoBehaviour
{
    [SerializeField] private RectTransform _origin;
    [SerializeField] private Image _imagePrefab;
    [SerializeField, Range(1, 10)] private int _maxColumns;
    [SerializeField, Range(0, 20)] private float _spacing;

    private List<Image> _images;
    private int _lastImageIndex;

    public void Initialize(int count)
    {
        _images = new List<Image>();

        var imageSize = _imagePrefab.GetComponent<RectTransform>().rect.width;
        Vector3 position = _origin.position;

        for (int i = 0; i < count; i++)
        {
            if (i % _maxColumns == 0)
                position = new Vector3(_origin.position.x, position.y - imageSize - _spacing, position.z);

            var image = Instantiate(_imagePrefab, position, quaternion.identity, _origin);
            _images.Add(image);

            position = new Vector3(position.x + imageSize + _spacing, position.y, position.z);
        }

        _lastImageIndex = _images.Count - 1;

        Hide();
    }

    public void Show()
    {
        foreach (var image in _images)
        {
            image.gameObject.SetActive(true);
            _lastImageIndex = _images.Count - 1;
        }
    }

    public void Hide()
    {
        foreach (var image in _images)
        {
            image.gameObject.SetActive(false);
        }
    }

    public void Update(float value)
    {
        if (value < _lastImageIndex)
            _images[_lastImageIndex--].gameObject.SetActive(false);
    }
}