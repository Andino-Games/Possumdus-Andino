using _scripts.TaskSystem.NewTaskSystem;
using UnityEngine;

namespace _scripts.Player
{
    public class Player : MonoBehaviour
    {
        [Header("Control Settings")] 
        [SerializeField] private string horizontal;
        [SerializeField] private string vertical;  
        [SerializeField] private string mouseX;
        [SerializeField] private string mouseY;
        //private bool isActive;
        
        [Header("Other Settings")]
        [SerializeField] private float mouseSensibility;
        [SerializeField] private Character _character;
        [SerializeField] private HungerManager _hungerManager;
        [SerializeField] private LevitateObjects _levitateObjects;
        [SerializeField] private PauseMenu pauseMenu;
        [SerializeField] private TaskView taskView;
        [SerializeField] private GameObject map;
        [SerializeField] private GameObject taskPanel;
        [SerializeField] private bool _mapActive;
        [SerializeField] private bool _tPanelActivated;

        private float _x, _y;
        private float _mX, _mY;
        private void OnEnable()
        {
            //isActive = false;
            _mapActive = false;
            _tPanelActivated = false;
            taskPanel.SetActive(true);
        }
        private void Update()
        {
            _x = Input.GetAxisRaw(horizontal);
            _y = Input.GetAxisRaw(vertical);

            _mX = Input.GetAxis(mouseX) * mouseSensibility;
            _mY = Input.GetAxis(mouseY) * mouseSensibility;

            _character.Rotation(_mX, _mY);
            _hungerManager.Hunger();

            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift) && _hungerManager.canRun &&
                _hungerManager.sprintDuration > 0)
            {
                _hungerManager.CanSprint();
            }
            else
                _hungerManager.CantSprint();

            if (Input.GetMouseButtonDown(1))
            {
                _levitateObjects.ObjectPiked();

            }
            else if (Input.GetMouseButtonUp(1) && _levitateObjects.pikedObject)
            {
                _levitateObjects.ReleaseObject();
            }

            if (Input.GetMouseButtonDown(0))
            {
                _levitateObjects.InteractObject();
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                pauseMenu.OpenMenu();
            }

           /* if (Input.GetKeyDown(KeyCode.T))
            {
                if (isActive)
                {
                    taskView.CloseTaskPanel();
                }
                else
                {
                    taskView.OpenTaskPanel();
                }
                isActive = !isActive;
            }*/

            if (Input.GetKeyDown(KeyCode.M))
            {
                if (_mapActive)
                {
                    map.SetActive(true);
                }
                else
                {
                    map.SetActive(false);
                }

                _mapActive = !_mapActive;
            }
            
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (_tPanelActivated)
                {
                    taskPanel.SetActive(true);
                }
                else
                {
                    taskPanel.SetActive(false);
                }

                _tPanelActivated = !_tPanelActivated;
            }
        }
            
    
        private void FixedUpdate()
        {
            _character.Move(_x, _y);
            if (_levitateObjects.isObjectLevitating && _levitateObjects.pikedObject != null)
            {
                _levitateObjects.LevitateObject();
                _levitateObjects.FollowPlayer();
            }
        }
    }
}
