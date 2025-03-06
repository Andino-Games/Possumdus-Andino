using UnityEngine;

namespace _scripts.Objects.Oxygen
{
    public class OxygenButton : MonoBehaviour
    {
        [SerializeField] private Oxygen oxygen;
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                oxygen.RechargeOxygen();
            }
        }
    }
}