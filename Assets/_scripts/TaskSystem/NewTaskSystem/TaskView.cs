using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _scripts.TaskSystem.NewTaskSystem
{
    public class TaskView : MonoBehaviour
    {
        public TaskController taskController; 
        public Transform taskPanelParent; 
        public GameObject taskUIPrefab;
            
        private Dictionary<int, TextMeshProUGUI> taskTextElements = new Dictionary<int, TextMeshProUGUI>();

        private void Start()
        {
            InitializeUI();
        }
      
        private void InitializeUI()
        {
            if (taskController == null || taskUIPrefab == null || taskPanelParent == null)
            {
                Debug.LogError("Una o m�s referencias en TaskView no est�n asignadas.");
                return;
            }
            InitializeTaskList(taskController.shipTask);
            InitializeTaskList(taskController.missionTask);
        }

        private void InitializeTaskList(List<Tasks> tasksList)
        {
            foreach (var task in tasksList)
            {
                if (task == null)
                {
                    Debug.LogWarning("Se encontr� una tarea nula en la lista de tareas.");
                    continue;
                }
                if (taskTextElements.ContainsKey(task.idTask))
                {
                    Debug.LogWarning($"La tarea con ID {task.idTask} ya est� registrada en el diccionario.");
                    continue;
                } 
                GameObject taskUI = Instantiate(taskUIPrefab, taskPanelParent);
                TextMeshProUGUI taskText = taskUI.GetComponentInChildren<TextMeshProUGUI>();
                if (taskText != null)
                {
                    taskText.text = $"{task.description}";
                    taskTextElements.Add(task.idTask, taskText);
                    Debug.Log($"UI para tarea {task.idTask} registrada correctamente.");
                    task.OnReachedTask += () =>
                    {
                        Debug.Log($"OnReachedTask disparado para tarea ID={task.idTask}");
                        UpdateTaskText(task);
                    };
                }
                else
                {
                    Debug.LogError("El prefab taskUIPrefab no tiene un componente TextMeshProUGUI en su jerarqu�a.");
                }
                Debug.Log($"Tarea registrada: ID={task.idTask}, Descripción={task.description}");
            }
        }
   
        public void UpdateTaskText(Tasks tasks_)
        {
            if (tasks_ == null)
            {
                Debug.LogError("UpdateTaskText fue llamado con una tarea nula.");
                return;
            }
            if (taskTextElements.TryGetValue(tasks_.idTask, out var taskText))
            {
                if (tasks_.isReached)
                {
                    taskText.text = $"¡{tasks_.reachedTask}";
                    taskText.color = Color.green;
                    Debug.Log($"UI actualizada para tarea ID={tasks_.idTask}, texto={taskText.text}");
                }
                else
                {
                    taskText.text = $"{tasks_.description}";
                    Debug.LogWarning($"La tarea ID={tasks_.idTask} no está completada (isReached=false).");
                }
            }
            else
            {
                Debug.LogWarning($"No se encontró UI para la tarea {tasks_.idTask}. Verifique que la tarea esté correctamente registrada en InitializeUI.");
            }
        }

        public void OpenTaskPanel()
        {
            taskPanelParent.gameObject.SetActive(true);
        }         
         public void CloseTaskPanel()
        {
            taskPanelParent.gameObject.SetActive(false);
        }           
    }
}


