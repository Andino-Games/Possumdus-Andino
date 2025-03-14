using TMPro;
using UnityEngine;

namespace _scripts.UI.TimeManager
{
    public class TimeView : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeText;

        private int _minutes, _seconds;

        public void Initialize()
        {
            timeText.gameObject.SetActive(true);
        }

        public void SetTime(float timeElapsed)
        {
            _minutes = (int)(timeElapsed / 60f);
            _seconds = (int)(timeElapsed - _minutes * 60f);
            
            var timeFormat = $"{_minutes:00}:{_seconds:00}";
            timeText.text = timeFormat;
        }

        public void Close()
        {
            timeText.gameObject.SetActive(false);
        }
    }
}