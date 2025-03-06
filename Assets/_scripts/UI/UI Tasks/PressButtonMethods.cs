using _scripts.TaskSystem.NewTaskSystem;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _scripts.UI.UI_Tasks
{
    public class PressButtonMethods : UIPlayerVerification
    {
        [Header("Press Button Task Settings")]
        public Outline outline;
        public TextMeshProUGUI statusText;
        public Image fillImage;
        public float fillSpeed;
        public float drainSpeed;
        public bool isDone = false;
        [SerializeField] private bool isHolding = false;

        #region TaskVerificationMethods
        protected override void Start()
        {
            outline.enabled = true;
            base.Start();
            fillImage = base.interactablePanel.transform.Find("FillImage").GetComponent<Image>();
       
            if (fillImage == null)
            {
                Debug.LogError("No se encontr� una imagen de relleno dentro del panel.");
                return;
            }
        }
        protected override void Update()
        {
            base.Update();
            FillImages();

            if (uiTasks.isCompleted && !isDone)
            {
                base.CloseTask();
                isDone = true;
            }        
        
    }

        public void FillImages()
        {
            if (fillImage == null|| uiTasks.isCompleted) return;

            if (isHolding)
            {
                fillImage.fillAmount += fillSpeed * Time.deltaTime;
                if (fillImage.fillAmount >= 1f)
                {
                    CompleteTask();
                    isHolding = false;
                }
            }
            else
            {
                fillImage.fillAmount -= drainSpeed * Time.deltaTime;
                fillImage.fillAmount = Mathf.Clamp(fillImage.fillAmount, 0f, 1f);
            }
        }

        public void OnHoldButtonPress()
        {
            isHolding = true;
            Debug.Log("Boton Presionado");
        }

        public void OnHoldButtonRelease()
        {
            isHolding = false;
            Debug.Log("Boton Suelto");
        }
        private void CompleteTask()
        {
            Debug.Log("�Button Press Task Completed!");
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
