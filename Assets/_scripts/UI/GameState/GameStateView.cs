using UnityEngine;

namespace _scripts.UI.GameState
{
    public class GameStateView : MonoBehaviour
    {
        [SerializeField] private GameObject firstEndPanel;
        [SerializeField] private GameObject secondEndPanel;
        [SerializeField] private GameObject thirdEndPanel;
                
        public void ShowEnd1()
        {
           firstEndPanel.SetActive(true);
        }

        public void ShowEnd2()
        {
           secondEndPanel.SetActive(true);
        }
        public void ShowEnd3()
        {
           thirdEndPanel.SetActive(true);
        }
    }
}