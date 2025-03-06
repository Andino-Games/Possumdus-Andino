using _scripts.TaskSystem.NewTaskSystem;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _scripts.UI.UI_Tasks
{
    public class RotatingPipesMethods : UIPlayerVerification
    {
        [Header("Rotating Pipes Task Settings")]
        public Outline outline;
        public TextMeshProUGUI statusText;
        public GameObject pipeHolder; 
        public GameObject[] pipes;
        [SerializeField]private int totalPipes = 0;
        [SerializeField]private int correctPipes = 0;
        [SerializeField] private bool isDone = false;

        #region TaskVerificationMethods

        protected override void Update()
        {
            base.Update();
            if (uiTasks.isCompleted && !isDone)
            {
                base.CloseTask();
                isDone = true;
                
            }
        }
        protected override void Start()
        {
            outline.enabled = true;
            base.Start();
            totalPipes = pipeHolder.transform.childCount;
            pipes = new GameObject[totalPipes];
            for (int i = 0; i < pipes.Length; i++)
            {
                pipes[i] = pipeHolder.transform.GetChild(i).gameObject;
                RotatingPipes rotPipes = pipes[i].GetComponent<RotatingPipes>();
                if (rotPipes != null)
                {
                    rotPipes.OnPipeChanged += CheckPipes;
                    if(rotPipes.isPlaced)
                    {
                        correctPipes++;
                    }
                }
            }
            Debug.Log($"Tuberias totales: {totalPipes}, Correctas Inicialmente:{correctPipes}");
        }

        private void CheckPipes(RotatingPipes rotPipes)
        {
            Debug.Log($"Revisando estado de la tubería: {rotPipes.gameObject.name}, isPlaced: {rotPipes.isPlaced}");
            if (rotPipes.isPlaced && correctPipes < totalPipes)
            {
                correctPipes++;
            }

            else if(!rotPipes.isPlaced && correctPipes > 0)
            {
                correctPipes--;
            }

            Debug.Log($"Tuberías correctas: {correctPipes}/{totalPipes}");

            if (correctPipes == totalPipes)
            {
                CompleteTask();
            }
        }

        private void CompleteTask()
        {
            outline.enabled = false;
            uiTasks.CompleteUITask();
            ShowCompletedTaskMessage();
            Debug.Log("¡Rotating Pipes Task Completed!");
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
