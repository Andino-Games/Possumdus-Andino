using System;
using System.Collections;
using _scripts.Audio;
using _scripts.Objects.Oxygen;
using _scripts.TaskSystem.NewTaskSystem;
using _scripts.UI.GameState;
using UnityEngine;
using UnityEngine.UI;

namespace _scripts.Managers
{
    public class GameManager : MonoBehaviour
    {
        public int requiredShipTasks;
        public int requiredMissionTasks;
        public Button exitButton;
        public TaskController taskController;
       
        public enum GameState
        {
            Game,
            FirstEnd,
            SecondEnd,
            ThirdEnd,
            GameOver
        }
        public GameState CurrentState { get; private set; }
        [SerializeField] private GameStateController gameStateController;

        private void OnEnable()
        {
            //Oxygen.OnOxygenOver += HandleGameOver;
            TimeManager.OnTimeOver += HandleEndGameValidation;
        }

        private void Start()
        {
            ChangeState(GameState.Game);
            Time.timeScale = 1;
        } 

        private void EndGame()
        {
            Time.timeScale = 0;
        }
        
        private void HandleEndGameValidation()
        {
            
            if (taskController.shipTasksCompleted >= requiredShipTasks || taskController.shipTasksCompleted > taskController.missionTasksCompleted)
            {
                ChangeState(GameState.FirstEnd);
                gameStateController.End2();
                CursorManager.instance.EnableInteractionMode();
                EndGame();
            }
            else if(taskController.missionTasksCompleted >= requiredMissionTasks || taskController.missionTasksCompleted > taskController.shipTasksCompleted)
            {
                ChangeState(GameState.SecondEnd);
                gameStateController.End1();
                CursorManager.instance.EnableInteractionMode();
                EndGame();
              
            }else if(taskController.missionTasksCompleted < requiredMissionTasks && taskController.shipTasksCompleted < requiredShipTasks)
            {
                ChangeState(GameState.ThirdEnd);
                gameStateController.End3();
                CursorManager.instance.EnableInteractionMode();
                EndGame();
            }
            StartCoroutine(ShowExitButtonWithDelay());
        }
        private void ChangeState(GameState newState)
        {
            CurrentState = newState;
            Debug.Log($"Game State change to: {newState}");
        }
        
        private void OnDisable()
        {
            //Oxygen.OnOxygenOver -= HandleGameOver;
            TimeManager.OnTimeOver -= HandleEndGameValidation;
        }

        private IEnumerator ShowExitButtonWithDelay()
        {
            yield return new WaitForSecondsRealtime(5);
            exitButton.gameObject.SetActive(true);
        }
    }
}