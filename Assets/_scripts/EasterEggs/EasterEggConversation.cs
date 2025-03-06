using DialogueEditor;
using UnityEngine;

namespace _scripts.EasterEggs
{
    public sealed class EasterEggConversation : MonoBehaviour
    {
        public GameObject easterUI;
        public bool toggle;
        public bool isPlayerInRange;
        public Renderer objectMesh;
        public GameObject interactableText;
        public GameObject reticle;
        [SerializeField] private NPCConversation eeConversation;
  

        public void OpenCloseEaster()
        {
            toggle = !toggle; 
            if (toggle)
            {
                easterUI.SetActive(true);
                objectMesh.enabled = false;
                ConversationManager.Instance.StartConversation(eeConversation);
                Debug.Log("Easter Egg opened");
            }
            else
            {
                easterUI.SetActive(false);
                objectMesh.enabled = true;
                ConversationManager.Instance.EndConversation();
                Debug.Log("Easter Egg closed");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = true;
                interactableText.SetActive(true);
                reticle.SetActive(false);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = false;
                interactableText.SetActive(false);
                reticle.SetActive(true);
            }
        }

        private void Update()
        {
            if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
            {
                OpenCloseEaster(); 
            }
        }
    }
}
