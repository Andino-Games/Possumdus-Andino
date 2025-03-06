using UnityEngine;

namespace _scripts.UI
{
    public class SphereHandAnimation : MonoBehaviour
    {
        public Animator animator; 
        [SerializeField] private bool toggle;
        [SerializeField] private bool isHandUp;
        void Start()
        {
            animator = GetComponent<Animator>();
            if (animator == null)
            {
                Debug.LogWarning("No hay animator en la mano");
            }
        }
  
        void Update()
        {
            if (animator != null)
            {
                if (Input.GetKeyDown(KeyCode.Q) && !toggle)
                {
                    InHandAnim();
                    toggle = true;
                }
                else if (Input.GetKeyDown(KeyCode.Q) && toggle)
                {
                    OutHandAnim();
                    toggle = false;
                }
            }
// esta es la parte del script en caso de que estén las animaciones de los artistas (tres estados)
            /* if (animator != null)
             {
                 if (Input.GetKeyDown(KeyCode.Q) && !toggle)
                 {
                     InHandAnim();
                     toggle = true;
                     isHandUp = true;
                     if (isHandUp)
                     {
                         IdleHandAnim();
                     }

                 }else if (Input.GetKeyDown(KeyCode.Q) && toggle && isHandUp)
                 {
                     OutHandAnim();
                     toggle = false;
                     isHandUp = false;
                 }
             }*/
        }

        private void InHandAnim()
        {
            animator.SetTrigger("inHand");
        }
 //Este es el metodo para el segundo estado q es el idle
       /* private void IdleHandAnim()
        {
            animator.SetTrigger("idleHandY");
        }*/

        private void OutHandAnim()
        {
            animator.SetTrigger("outHand");
        }
    }
}
