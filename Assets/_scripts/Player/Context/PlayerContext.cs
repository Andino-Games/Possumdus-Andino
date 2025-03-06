using _scripts.Interfaces;
using UnityEngine;

namespace _scripts.Player.Context
{
    public class PlayerContext : MonoBehaviour, IPlayerContext
    {
        [SerializeField] private HungerManager _hungerManager;
        
        public float GetEnergy()
        {
            return _hungerManager.sprintDuration;
        }

        public void SetEnergy(float amount)
        {
            _hungerManager.sprintDuration = amount;
        }

        public float GetFood()
        {
            return _hungerManager.sacietyDuration;
        }

        public void SetFood()
        {
            _hungerManager.Eat();
        }
    }
}
