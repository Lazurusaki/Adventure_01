using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ADV_13
{
    public class ResultView : MonoBehaviour
    {
        private const string WinText = "YOU WIN";
        private const string LooseText = "YOU LOOSE";
        
        [SerializeField] private TMP_Text _resultText;
        [SerializeField] private float _time;

        private Coroutine _displayCoroutine;
        private WaitForSeconds _wait;

        public event Action Completed;

        private void Awake()
        {
            _wait = new WaitForSeconds(_time);
        }

        public void Show(Results result)
        {
            switch (result)
            {
                case Results.Win:
                    _resultText.text = WinText;
                    break;
                case Results.Loose:
                    _resultText.text = LooseText;
                    break;
            }
            
            if (_displayCoroutine is not null)
                StopCoroutine(_displayCoroutine);
            _displayCoroutine = StartCoroutine(ShowDuration());
            
            
        }

        private IEnumerator ShowDuration()
        {
            _resultText.enabled = true;
            yield return _wait;
            _resultText.enabled = false;
            Completed?.Invoke();
        }
    }
}
