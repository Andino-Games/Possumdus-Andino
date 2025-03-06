using _scripts.Player;
using _scripts.Player.Context;
using UnityEngine;

namespace _scripts.Objects.Food
{
    public class Food : MonoBehaviour
    {
        public Outline outline;
        [SerializeField] private PlayerContext pContext;

        private void Start()
        {
            pContext = FindObjectOfType<PlayerContext>();
            outline.enabled = true;
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                pContext.SetFood();
                outline.enabled = false;
                Destroy(this.gameObject);
            }
        }
    }
}

