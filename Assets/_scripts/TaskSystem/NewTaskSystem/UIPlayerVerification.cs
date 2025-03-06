using _scripts.Managers;
using _scripts.Player;
using _scripts.UI;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _scripts.TaskSystem.NewTaskSystem
{
    public class UIPlayerVerification : MonoBehaviour
    {
        [Header("General UI Task Settings")] 
        public UITasks uiTasks;
        public ProgressSlider proSlider;
        public Character character;
        public GameObject interactablePanel;
        public GameObject interactableText;
        public GameObject reticle;
        public float requiredHoldTime;
        public bool isInteracting;
        public bool isPlayerInRanges;
        public bool toggle;

        #region PlayerDetectionMethods

        protected virtual void Start()
        {
             character = FindObjectOfType<Character>();
            if (character == null)
                Debug.LogError("Character script not found in the scene.");
        }

        protected virtual void Update()
        {
            if (isPlayerInRanges)
            {
                if (Input.GetKey(KeyCode.E) && !isInteracting)
                {
                    proSlider.gameObject.SetActive(true);
                    bool isComplete = proSlider.IncrementProgress(Time.deltaTime);
                    if (isComplete)
                    {
                        StartCoroutine(DisableProSlider());
                        isInteracting = true;
                       OpenTask();
                       
                    }
                   
                } 
                else if (Input.GetKeyDown(KeyCode.E))
                { 
                    CloseTask();
                    
                    isInteracting = false;
                   proSlider.ResetProgress();
                }
                
            }

            
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !uiTasks.isCompleted)
            {
                isPlayerInRanges = true;
                proSlider.ResetProgress();
                interactableText.SetActive(true);
                proSlider.InitializeSlider(requiredHoldTime);
                reticle.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRanges = false;
                interactableText.SetActive(false);
                proSlider.gameObject.SetActive(false);
                proSlider.ResetProgress();
                reticle.SetActive(true);
            }
        }

        private void OpenTask()
        {
            toggle = true;
            interactablePanel.SetActive(true);
            interactableText.SetActive(false);
            proSlider.gameObject.SetActive(false);
            uiTasks.isActive = true;
            CursorManager.instance.EnableInteractionMode();
            Debug.Log($"{uiTasks.names} Task opened");
        }

        public void CloseTask()
        {
            toggle = false;
            interactablePanel.SetActive(false);
            interactableText.SetActive(false);
            proSlider.gameObject.SetActive(false);
            uiTasks.isActive = false;
            CursorManager.instance.DisableInteractionMode();
            Debug.Log($"{uiTasks.names} Task closed");
        }

        private IEnumerator DisableProSlider()
        {
            yield return new WaitForSecondsRealtime(0.5f);
            proSlider.gameObject.SetActive(false);

        }

       
        /* private void OpenCloseTask()
         {
             toggle = !toggle;
             if (toggle)
             {
                 interactablePanel.SetActive(true);
                 uiTasks.isActive = true;
                 CursorManager.instance.EnableInteractionMode();
                 Debug.Log($"{uiTasks.names} Task opened");
             }
             else if (!toggle)
             {
                 interactablePanel.SetActive(false);
                 uiTasks.isActive = false;
                 CursorManager.instance.DisableInteractionMode();
                 Debug.Log($"{uiTasks.names} Task closed");
             }
         }*/

        #endregion
    }

}
