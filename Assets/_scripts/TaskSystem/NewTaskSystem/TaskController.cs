using System.Collections.Generic;
using UnityEngine;

namespace _scripts.TaskSystem.NewTaskSystem
{
    public class TaskController : MonoBehaviour
    {
        public List<Tasks> shipTask;
        public List<Tasks> missionTask;
        public TaskView taskView;
        public int missionTasksCompleted;
        public int shipTasksCompleted;
       private void Start()
        {
            UpdateUI(shipTask);
            UpdateUI(missionTask);
            ValidateUniqueIDs(shipTask);
            ValidateUniqueIDs(missionTask);
        }
        private void UpdateUI(List<Tasks> tasksList)
        {
            if (taskView == null)
            {
                taskView = FindObjectOfType<TaskView>();
            }
               
            foreach (var task in tasksList)
            {
                Debug.Log($"Instancia de tarea ID={task.idTask}: {task.GetHashCode()}");
                task.OnReachedTask += () => OnTaskCompleted(task);
                Debug.Log($"Evento conectado para la tarea ID={task.idTask}");
               
            }
        }

        private void ValidateUniqueIDs(List<Tasks> tasksList)
        {
            HashSet<int> idSet = new HashSet<int>();
            foreach (var task in tasksList)
            {
                if (idSet.Contains(task.idTask))
                {
                    Debug.LogError($"¡Colisión de ID! La tarea con ID {task.idTask} ya existe en la lista.");
                }
                else
                {
                    idSet.Add(task.idTask);
                }
            }
        }
        private void OnTaskCompleted(Tasks completedTask)
        {
            if (completedTask == null)
            {
                Debug.LogError("OnTaskCompleted fue llamado con una tarea nula.");
                return;
            }
            if (completedTask.isReached)
            {
                Debug.LogWarning($"La tarea con ID {completedTask.idTask} ya estaba marcada como completada. No se procesará de nuevo.");
                return; 
            }
           
            completedTask.isReached = true;
            Debug.Log($"Task {completedTask.idTask} completed!");

           if (shipTask.Contains(completedTask))
            {
                shipTasksCompleted++;
                Debug.Log($"Tareas de Nave Completadas: {shipTasksCompleted}/{shipTask.Count}");
            }
            else if (missionTask.Contains(completedTask))
            {
                missionTasksCompleted++;
                Debug.Log($"Tareas de Misión Completadas: {missionTasksCompleted}/{missionTask.Count}");
            }
            if (taskView != null)
            {
                Debug.Log($"Notificando al TaskView para la tarea ID={completedTask.idTask}");
                taskView.UpdateTaskText(completedTask);
            }
            else
            {
                Debug.LogError("TaskView es nulo en TaskController.");
            }
        }
        
        /*private void AssignNewGlobalIDs(Tasks tasks) //En caso de expandir este codigo
      {
          if (tasks.idTask == 0)
          {
              tasks.idTask = ++_globalTaskID;
          }
      }
     public void AddTask(Tasks task)
      {
          if(task == null) return;
          AssignNewGlobalIDs(task);
          if (shipTask != null && !shipTask.Contains(task))
          {
              shipTask.Add(task);
              task.OnReachedTask += () => OnTaskCompleted(task);
          }else if (missionTask != null && !missionTask.Contains(task))
          {
              missionTask.Add(task);
              task.OnReachedTask += () => OnTaskCompleted(task);
          }
      }*/
     }
}


