using UnityEngine;

namespace _scripts.UI.GameState
{
    public class GameStateController : MonoBehaviour
    {
        [SerializeField] private GameStateView stateView;
        [SerializeField] private GameObject nameRoom;

        public void End1()
        {
            stateView.ShowEnd1();
            nameRoom.gameObject.SetActive(false);
        }

        public void End2()
        {
            stateView.ShowEnd2();
            nameRoom.gameObject.SetActive(false);
        }

        public void End3()
        {
            stateView.ShowEnd3();
            nameRoom.gameObject.SetActive(false);
        }
    }
}