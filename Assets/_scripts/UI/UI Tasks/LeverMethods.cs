using System.Collections;
using System.Collections.Generic;
using _scripts.TaskSystem.NewTaskSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _scripts.UI.UI_Tasks
{
    public class LeverMethods : UIPlayerVerification
    {
        [Header("Lever Task Settings")]
        public Outline outline;
        public TextMeshProUGUI statusText;
        public List<Slider> levers; 
        private int _currentLeverIndex = 0; 
        private bool _isTaskFailed = false;
        public bool isDone = false;

        #region TaskVerificationMethods

        private new void Start()
        {
            outline.enabled = true;
        }
        protected override void Update()
        {
            
            base.Update();
            if (_isTaskFailed)
            {
                ResetLevers();
            }
        
            if (uiTasks.isCompleted && !isDone)
            {
                base.CloseTask();
                isDone = true;
            }
        
    }

        public void OnValueChanged(Slider lever)
        {
           if (lever == levers[_currentLeverIndex])
           { 
               if (lever.value >= lever.maxValue)
               {
                   lever.value = lever.maxValue;
                   _currentLeverIndex++;

                   if (_currentLeverIndex >= levers.Count)
                   {
                       CompleteTask();
                       Debug.Log("lever task done");
                   }
               }
           }
           else
           {
               FailTask();
           }
        }

        public void OnPointUp(Slider lever)
        {
            if (levers.Count <= _currentLeverIndex) return;
            if (lever == levers[_currentLeverIndex])
            {
                if (lever.value < lever.maxValue)
                {
                    FailTask();
                }
            }
            
        }

        private void ResetLevers()
        {
            foreach(var lever in levers)
            {
                lever.value = lever.minValue; 
            }

            _isTaskFailed = false;
            _currentLeverIndex = 0;
        }
        private void CompleteTask()
        {
            outline.enabled = false;
            uiTasks.CompleteUITask();
            ShowCompletedTaskMessage();
        }

        private void FailTask()
        {
            Debug.Log("Task Failed!");
            ShowCompletedTaskMessage();
            _isTaskFailed = true;
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
