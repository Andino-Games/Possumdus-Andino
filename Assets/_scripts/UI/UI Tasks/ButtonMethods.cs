using System.Collections;
using System.Collections.Generic;
using _scripts.TaskSystem.NewTaskSystem;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace _scripts.UI.UI_Tasks
{
    public class ButtonMethods : UIPlayerVerification
    {
        [Header("Button Task Settings")]
        public Outline outline;
        public TextMeshProUGUI statusText;
        public List<ButtonLeds> buttons;
        public List<int> targetValues;
        public List<int> currentValues = new();
        public bool isDone = false;

        #region TaskVerificationMethods
        protected override void Start()
        {
            outline.enabled = true;
            base.Start();
            InitializeButtons();
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

        private void InitializeButtons()
        {
            if (buttons.Count != targetValues.Count)
            {
                Debug.Log("El numero de botones no coincide con el numero de valores objetivo");
                    return;
            }
            currentValues.Clear();
            for (int i = 0; i < buttons.Count; i++)
            {
                int index = i;
                buttons[i].targetState = targetValues[index];
                buttons[i].InitializeState();
                currentValues.Add(buttons[i].state);
                buttons[i].OnStateChanged += (button) =>
                {
                    currentValues[index] = button.state;
                    CheckButtons();
                };
            }
        }

        private void CheckButtons()
        {
            if (!uiTasks.isActive || uiTasks.isCompleted) return;
            if (currentValues.Count != targetValues.Count)
            {
                Debug.Log("Not all buttons are set yet.");
                return;
            }

            for (int i = 0; i < targetValues.Count; i++)
            {
                if (currentValues[i] != targetValues[i])
                {
                    Debug.Log($"Button {i} is not in the current state");
                    return;
                }
            }

            CompleteTask();
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
