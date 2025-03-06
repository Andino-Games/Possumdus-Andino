using System.Collections;
using System.Collections.Generic;
using _scripts.TaskSystem.NewTaskSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _scripts.UI.UI_Tasks
{
    public class SliderMethods : UIPlayerVerification
    {
        [Header("Slider Task Settings")]
        public Outline outline;
        public TextMeshProUGUI statusText;
        public List<Slider> sliders;
        public List<Image> sliderBorders;
        public List<float> targetValues;
        public float tolerance;
        public bool isDone = false;
        public Color correctColor = Color.green;
        public Color incorrectColor = Color.red;
        public Sprite correctValueSlider;
        public Sprite standardValueImage;

        #region TaskVerificationMethods

        private void OnEnable()
        {
            outline.enabled = true;
            foreach (var slider in sliders)
            {
                slider.onValueChanged.AddListener(delegate { UpdateSliderImage(); });
                slider.onValueChanged.AddListener(delegate { CheckSliders(); });
            }
        }

        private void OnDisable()
        {
            foreach (var slider in sliders)
            {
                slider.onValueChanged.RemoveListener(delegate { UpdateSliderImage(); });
                slider.onValueChanged.RemoveListener(delegate { CheckSliders(); });
            }
        }
        protected override void Update()
        {
            base.Update();
            if (uiTasks.isCompleted && !isDone)
            {
                base.CloseTask();
                isDone = true;
            }
        }

        private void CheckSliders()
        {
            if (!uiTasks.isActive || uiTasks.isCompleted)
            {
                Debug.Log("Task is either inactive or already completed.");
                return;
            }

            for (int i = 0; i < sliders.Count; i++)
            {
                float difference = Mathf.Abs(sliders[i].value - targetValues[i]);
                Debug.Log($"Checking Slider {i}: Value = {sliders[i].value}, Target = {targetValues[i]}, Difference = {difference}");

                if (difference > tolerance)
                {
                    Debug.Log("Sliders are not in the correct positions.");
                    return;
                }
            }

            CompleteTask();
            Debug.Log($"Task {uiTasks.names} completed!");
        }

        private void UpdateSliderColors()
        {
            for (int i = 0; i < sliders.Count; i++)
            {
                float difference = Mathf.Abs(sliders[i].value - targetValues[i]);
                sliderBorders[i].color = difference <= tolerance ? correctColor : incorrectColor;
            }
        }

        private void UpdateSliderImage()
        {

            for (int i = 0; i < sliders.Count; i++)
            {
                float difference = Mathf.Abs(sliders[i].value - targetValues[i]);
                sliderBorders[i].sprite = difference <= tolerance ? correctValueSlider : standardValueImage;
            }
        }

        private void CompleteTask()
        {
            outline.enabled = false;
            uiTasks.CompleteUITask();
            ShowCompletedTaskMessage();
        }

        private void ShowCompletedTaskMessage()
        {
            if (uiTasks.isCompleted)
            {
                statusText.text = "Task Completed!";
                statusText.gameObject.SetActive(true);
                StartCoroutine(HideCompletedTaskMessage());
            }
            else
            {
                statusText.text = "Task is not completed!";
                statusText.gameObject.SetActive(true);
                StartCoroutine(HideCompletedTaskMessage());
            }
        }

        private IEnumerator HideCompletedTaskMessage()
        {
            yield return new WaitForSeconds(2);
            statusText.gameObject.SetActive(false);
        }

        #endregion
    }
}
             
          
       



