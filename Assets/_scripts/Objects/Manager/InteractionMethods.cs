using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _scripts.Objects.Interactable;
using _scripts.TaskSystem.NewTaskSystem;
using TMPro;
using UnityEngine;

namespace _scripts.Objects.Manager
{

    public class InteractionMethods : MonoBehaviour
    {
        public Outline outline;
        public TextMeshProUGUI statusText;
        public InteractionTask intTask;
        public List<InteractionButton> buttons = new();
        private List<string> currentCombination = new();
        private readonly List<string> defaultCombination = new() { "A", "B", "C", "D" };

        private void Start()
        {
          
            outline.enabled = true;
            AssignDefaultValuesToButtons();
        }

        public void SelectButton(InteractionButton button)
        {
            if (!currentCombination.Contains(button.value))
            {
                currentCombination.Add(button.value);
            }

            if (currentCombination.Count > intTask.correctCombination.Count)
            {
                ResetCombination();
            }

            if (currentCombination.Count == intTask.correctCombination.Count)
            {
                if (ValidateCombination())
                {
                    Debug.Log("Combinacion Correcta");
                    intTask.InvokeReachedEvent();
                    ShowCompletedTaskMessage();
                    DisableButtonInteractions();
                    outline.enabled = false;
                }
                else
                {
                    Debug.Log("Combinacion incorrecta, intenta de nuevo");
                    ShowCompletedTaskMessage();
                    ResetCombination();
                    IncorrectAnimation();
                    outline.enabled = true;
                    
                }
            }
        }

        private bool ValidateCombination()
        {
                if (currentCombination.SequenceEqual(intTask.correctCombination) &&
                !currentCombination.SequenceEqual(defaultCombination))
            {
                return true;
            }

            return false;

        }

        private void ResetCombination()
        {
            currentCombination.Clear();
        }
        private void IncorrectAnimation()
        {
            foreach (InteractionButton button in buttons)
            {
                button.IncorrectCombination();
            }
        }

        private void DisableButtonInteractions()
        {
            foreach (InteractionButton button in buttons)
            {
                button.DisableButton();
            }
        }

        private void AssignDefaultValuesToButtons()
        {
            if (buttons.Count == 4)
            {
                for (int i = 0; i < buttons.Count; i++)
                {
                    buttons[i].value = defaultCombination[i];
                }
            }
            else
            {
                Debug.LogError("Se requieren 4 botones para la tarea.");
            }
        }

        private void ShowCompletedTaskMessage()
        {
            if (intTask.isReached)
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
    }
}