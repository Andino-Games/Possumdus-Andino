using _scripts.Interfaces;
using _scripts.Player.Context;
using System.Collections;
using TMPro;
using UnityEngine;

namespace _scripts.Player
{
    public class HungerManager : MonoBehaviour
    {
        [Header("Hunger Manager Configuration")]
        public float sprintDuration;
        public float sacietyDuration;
        [SerializeField] private float _hungrySpeed;
        [SerializeField] private float _sprintSpeed;
        [SerializeField] private float _rechargeStaminaValue;
        [SerializeField] public float _timeUntilHunger;
        [SerializeField] private float _staminaAmount;
        public TextMeshProUGUI statusText; 
        public bool _isHungry = false;
        public bool canRun = true;
        private IPlayerContext _context;
        private Character character;

        #region HungerMethods
        private void OnEnable()
        {
            _staminaAmount = sprintDuration;
            _timeUntilHunger = sacietyDuration;
        }
        void Start()
        {
            character = GetComponent<Character>();
            if (character == null)
                Debug.LogError("Character script not found on Player Prefab.");
            _context = GetComponent<PlayerContext>();
            if (_context == null)
                Debug.LogError("PlayerContext not found on Player Prefab.");
        }

        public void CanSprint()
        {
            if (!_isHungry)
            {
                character._currentSpeed = _sprintSpeed;
                _staminaAmount -= Time.deltaTime;

                if (_staminaAmount <= 0)
                {
                    _staminaAmount = 0;
                    canRun = false;
                    character._currentSpeed = character.normalSpeed;
                }
            }
        }

        public void CantSprint()
        {
            character._currentSpeed = _isHungry ? _hungrySpeed : character.normalSpeed;

            if (_staminaAmount < sprintDuration)
            {
                _staminaAmount += (sprintDuration / _rechargeStaminaValue) * Time.deltaTime;
                if (_staminaAmount >= sprintDuration)
                {
                    _staminaAmount = sprintDuration;
                    canRun = true;
                }
            }
        }

        public void Hunger()
        {
            _timeUntilHunger -= Time.deltaTime;
            if (_timeUntilHunger <= 0 && !_isHungry)
            {
                BecomeHungry();
            }
        }

        public void BecomeHungry()
        {
            _isHungry = true;
            character._currentSpeed = _hungrySpeed;
            canRun = false;
            ShowHungerMessage();
        }

        public void Eat()
        {
            _isHungry = false;
            _timeUntilHunger = sacietyDuration;
            character._currentSpeed = character.normalSpeed;
            canRun = true;
            ShowHungerMessage();
        }

        private void ShowHungerMessage()
        {
            if (_isHungry)
            {
                statusText.text = "You're Hungry!";
                statusText.gameObject.SetActive(true);
                StartCoroutine(HideHungerMessage());
            }
            else
            {
                statusText.text = "You're Full!";
                statusText.gameObject.SetActive(true);
                StartCoroutine(HideHungerMessage());
            }
        }

        private IEnumerator HideHungerMessage()
        {
            yield return new WaitForSeconds(2);
            statusText.gameObject.SetActive(false);
        }
        #endregion

    }
}
