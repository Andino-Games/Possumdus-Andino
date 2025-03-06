using System;
using UnityEngine;

namespace _scripts.Managers
{
    public class TimeManager : MonoBehaviour
    {
        [SerializeField] private float timeElapse;
        public UIManager uiManager;
        public static event Action OnTimeOver;
        
        private void Update()
        {
            timeElapse -= Time.deltaTime;
            uiManager.UpdateTime(timeElapse);
            
            if (timeElapse <= 0 )
            {
                InvokeEndTimeEvent();
            }
        }

        public void InvokeEndTimeEvent()
        {
            OnTimeOver?.Invoke();
        }
    }
}