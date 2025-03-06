using _scripts.TaskSystem.NewTaskSystem;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _scripts
{
    public class DeliveryMethods : MonoBehaviour
    {
        public DeliveryTasks delTask;
        public TextMeshProUGUI statusText;
        public Outline outline;

        private void Start()
        {
            outline.enabled = true;
        } 

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(delTask.objectTag))
            {
                delTask.ProgressUpdate();
                Destroy(other.gameObject);
            }
        }

        private void Update()
        {
            if(delTask.isReached)
            {
                ShowCompletedTaskMessage();
                outline.enabled = false;
            }
        }

        private void ShowCompletedTaskMessage()
        {
            if (delTask.isReached)
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
