using _scripts.UI.TimeManager;
using UnityEngine;

namespace _scripts.Managers
{
    public class UIManager : MonoBehaviour
    {
        [Header("Time Configurations")]
        [SerializeField] private TimeController timeController;

        public void OnEnable()
        {
            timeController.Initialize();
        }

        public void UpdateTime(float timeElapse)
        {
            timeController.UpdateTime(timeElapse);
        }

        public void OnDisable()
        {
            timeController.Close();
        }
    }
}