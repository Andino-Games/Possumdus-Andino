using _scripts.Interfaces;
using UnityEngine;

namespace _scripts.Player
{
    public class LevitateObjects : MonoBehaviour
    {
        [Header("Objects Configuration")]
        [SerializeField] private float rayDistance;
        [SerializeField] private float holdDistance;
        [SerializeField] private float attractionForce;
        [SerializeField] private float levitationForce;
        [SerializeField] private float followSpeed;
        [HideInInspector] public bool isObjectLevitating = false;
        [HideInInspector] public GameObject pikedObject;
        [HideInInspector] public Rigidbody pikedObjectRb;
        [SerializeField] private LayerMask interactableLayer;
        private IObjectsInteract interactable;
        public Character character;

        private void Start()
        {
            character = FindObjectOfType<Character>();
            if (character == null)
                Debug.LogError("Character script not found in the scene.");
          
        }
        public void ObjectPiked()
        {
            var ray = new Ray(character._playerCamera.position, character._playerCamera.forward);
            Debug.DrawRay(character._playerCamera.position, character._playerCamera.forward * rayDistance, Color.red);


            if (Physics.Raycast(ray, out var hit, rayDistance, interactableLayer.value))
            {
                interactable = hit.collider.GetComponent<IObjectsInteract>();

                if (interactable != null)
                {
                    pikedObject = hit.collider.gameObject;
                    pikedObjectRb = pikedObject.GetComponent<Rigidbody>();

                    if (pikedObjectRb != null)
                    {
                        pikedObjectRb.useGravity = false;
                        pikedObjectRb.constraints = RigidbodyConstraints.FreezeAll;

                        interactable.OnInteract();

                        isObjectLevitating = true;
                        Debug.Log("objeto detectado: " + pikedObject.name);
                    }
                    else
                    {
                        Debug.Log("El objecto no tiene un rigidbody asignado");
                    }
                }
                else
                {
                    Debug.LogWarning("El objecto no implementa la interfaz");
                }
            }
            else
            {
                Debug.LogWarning("El objeto no es interactuable");
            }
        }
        public void LevitateObject()
        {
            if (pikedObjectRb != null && isObjectLevitating)
            {
                Vector3 levitateDirection = Vector3.up * levitationForce - pikedObjectRb.velocity * 0.5f;
                pikedObjectRb.AddForce(levitateDirection, ForceMode.Acceleration);
            }
        }

        public void FollowPlayer()
        {
            if (pikedObject != null)
            {
                Vector3 targetPosition = character._playerCamera.position + character._playerCamera.forward * holdDistance;
                float distance = Vector3.Distance(pikedObject.transform.position, targetPosition);

                pikedObject.transform.position = Vector3.MoveTowards(pikedObject.transform.position, targetPosition, Time.deltaTime * followSpeed);
                if (distance < 0.1f)
                {
                    pikedObjectRb.velocity = Vector3.zero;
                }
            }
        }

        public void ReleaseObject()
        {
            if (pikedObject != null)
            {
                interactable.OnRelease();

                pikedObjectRb.useGravity = true;
                pikedObjectRb.constraints = RigidbodyConstraints.None;

                isObjectLevitating = false;
                pikedObject = null;
                pikedObjectRb = null;
                interactable = null;
                Debug.Log("Solto el objeto");
            }
        }

        public void InteractObject()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                IObjectsInteract objectsInteract = hit.collider.GetComponent<IObjectsInteract>();
                if (objectsInteract != null)
                {
                    objectsInteract.OnInteract();
                }
            }
        }
    }
}
