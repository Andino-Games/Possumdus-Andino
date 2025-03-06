using _scripts.Interfaces;
using _scripts.Objects.Manager;
using UnityEngine;

namespace _scripts.Objects.Interactable
{
    public class InteractionButton : MonoBehaviour, IObjectsInteract
    {
        public string value;
        public InteractionMethods _manager;
        public Animator animator;
        public bool isInteractable = true;
        public bool isPressed = false;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        public void OnInteract()
        {
            if (isInteractable)
            {
                _manager.SelectButton(this);
                animator.SetTrigger("isPressed");
                Debug.Log($"Seleccionó el boton {value}");
            }
        }

        public void IncorrectCombination()
        {
            animator.SetTrigger("isIncorrect");
        }
        public void DisableButton()
        {
            isInteractable = false;
        }

        public void OnRelease()
        {
        }
    }
}
         
