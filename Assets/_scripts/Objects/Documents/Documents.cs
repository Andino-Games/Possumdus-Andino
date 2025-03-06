using _scripts.Interfaces;
using _scripts.Player.Context;
using UnityEngine;

namespace _scripts.Objects.Documents
{
    public class Documents : MonoBehaviour, IObjectsInteract
    {
        // public void Interact(IPlayerContext context)
        // {
        //     Debug.Log("Interactuaste conmigo");
        //     Destroy(gameObject);
        // }

        public void OnInteract()
        {
            throw new System.NotImplementedException();
        }

        public void OnRelease()
        {
            throw new System.NotImplementedException();
        }
    }
}
