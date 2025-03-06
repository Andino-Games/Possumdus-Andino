using System;
using UnityEngine;

namespace _scripts.Objects.Oxygen
{
    public class Oxygen : MonoBehaviour
    {
        [SerializeField] private float oxygenDuration;
        private float _oxygenTimer;
        private bool _hasOxygen = false;
        public static event Action OnOxygenOver;

        private void Awake()
        {
            _oxygenTimer = oxygenDuration;
        }

        private void Update()
        {
            OxygenManager();
        }

        private void OxygenManager()
        {
            _oxygenTimer -= Time.deltaTime;
            if (_oxygenTimer < 0 && !_hasOxygen)
            {
                OutOxygen();
            }
            else _hasOxygen = true;
        }
        
        private static void OutOxygen()
        {
            OnOxygenOver?.Invoke();
        }

        public void RechargeOxygen()
        {
            _hasOxygen = false;
            _oxygenTimer = oxygenDuration;
            
        }
    }
}