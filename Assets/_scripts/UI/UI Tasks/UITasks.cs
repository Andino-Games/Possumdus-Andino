using System.Collections.Generic;
using UnityEngine;

namespace _scripts.TaskSystem
{

    [CreateAssetMenu(menuName = "TaskScriptable/UITask", fileName = "UITasks", order = 2)]
    public class UITasks : Tasks
    {
        public bool isActive = false;
        public bool isCompleted = false;

#if UNITY_EDITOR
        private void OnEnable()
        {
            isCompleted = false;
            isActive = false;
            base.isReached = false;
        }
#endif

        public void CompleteUITask()
        {
            base.InvokeReachedEvent();
            isCompleted = true;
            isActive = false;
        }
    }
}
         
           
   
