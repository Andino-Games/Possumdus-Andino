using _scripts.Interfaces;
using _scripts.Managers;
using DialogueEditor;
using UnityEngine;

namespace _scripts.NPCs.States
{
    public class TalkingState : INpcState
    {
        private readonly NPCConversation _myConversation;

        public TalkingState(NPCConversation myConversation)
        {
            _myConversation = myConversation;
        }
        public void StartState()
        {
            Debug.Log("Entró al estado de Talking");
            CursorManager.instance.EnableInteractionMode();
            ConversationManager.Instance.StartConversation(_myConversation);
        }
        
        public void UpdateState()
        {

        }

        public void ExitState()
        {
            Debug.Log("Salio al estado de Talking");
            CursorManager.instance.DisableInteractionMode();
            ConversationManager.Instance.EndConversation();
        }
    }
}