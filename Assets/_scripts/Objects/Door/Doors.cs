using System;
using UnityEngine;

namespace _scripts.Objects.Door
{
    public class Doors : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string animationName;
        private void Awake()
        {
            animator.GetComponent<Animator>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                Debug.Log("Entro al animator puerta");
                animator.Play(animationName);
            }
        }
    }
}