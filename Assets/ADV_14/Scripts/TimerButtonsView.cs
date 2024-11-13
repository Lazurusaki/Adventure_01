using UnityEngine;
using UnityEngine.UI;

namespace ADV_14
{
    public class TimerButtonsView : MonoBehaviour
    {
        [SerializeField] private Button _start;
        [SerializeField] private Button _stop;
        [SerializeField] private Button _pause;
        [SerializeField] private Button _resume;

        public void Initialize(Timer timer)
        {
            _start.onClick.AddListener(timer.Start);
            _stop.onClick.AddListener(timer.Stop);
            _pause.onClick.AddListener(timer.Pause);
            _resume.onClick.AddListener(timer.Resume);
        }
    }
}
