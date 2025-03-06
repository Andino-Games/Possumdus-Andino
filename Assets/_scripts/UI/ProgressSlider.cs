using UnityEngine;
using UnityEngine.UI;

namespace _scripts.UI
{
    public class ProgressSlider : MonoBehaviour
    {
        [SerializeField] private float currentValue = 0f;
        [SerializeField] private float maxValue = 1f;
        [SerializeField] private Slider progressSlider;

        private void Awake()
        {
            progressSlider = GetComponent<Slider>();
            if (progressSlider == null)
            {
                Debug.LogWarning("ProgressSlider: No se encontró un componente Slider en este GameObject.");
            }
        }

        public void InitializeSlider(float duration)
        {
            maxValue = Mathf.Max(duration, 1f); 
            currentValue = 0;
            UpdateSliderUI();
        }

        public bool IncrementProgress(float delta)
        {
            currentValue = Mathf.Clamp(currentValue + delta, 0, maxValue); 
            UpdateSliderUI();

            if (currentValue >= maxValue)
            {
                currentValue = maxValue;
                return true; 
            }
            return false; 
        }

  
        public void ResetProgress()
        {
            currentValue = 0;
            UpdateSliderUI();
        }

        private void UpdateSliderUI()
        {
            if (progressSlider != null)
            {
                progressSlider.value = currentValue / maxValue;
            }
        }

        public bool IsComplete()
        {
            return currentValue >= maxValue;
        }
    }
}
