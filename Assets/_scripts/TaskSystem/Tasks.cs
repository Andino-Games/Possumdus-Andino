using System;
using UnityEngine;

namespace _scripts.TaskSystem
{
  public class Tasks : ScriptableObject
    {
        [Header("General Task Atributes")]
        public string names;
        public string description;
        public int idTask;
        public bool isReached = false;
        public string reachedTask;
        public TaskSubType taskType;
        public event Action OnReachedTask;

        public void InvokeReachedEvent()
        {
            OnReachedTask?.Invoke();
            isReached = true;
        }

        [System.Serializable]
        public enum TaskSubType
        {
            Interaction,
            Delivery,
            UITask
        }

    



    }
}
