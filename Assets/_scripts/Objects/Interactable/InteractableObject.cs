using System;
using _scripts.Interfaces;
using UnityEngine;

namespace _scripts.Objects.Interactable
{
    [Serializable]
    [RequireComponent(typeof(Rigidbody))]
    public class InteractableObject : MonoBehaviour, IObjectsInteract
    {
        [HideInInspector] public Rigidbody rb;
        [HideInInspector] public Outline outline;
        public bool isInteracted;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            outline = GetComponent<Outline>();
            rb.useGravity = false;
            outline.enabled = true;
        }

        public void OnInteract()
        {
            rb.useGravity = false;
            outline.enabled = false;
            isInteracted = true;
        }

        public void OnRelease()
        {
            rb.useGravity = true;
            outline.enabled = true;
            isInteracted = false;
        }
    }
}
