using System.Collections.Generic;
using _scripts.Interfaces;

namespace _scripts.NPCs
{
    public class NpcStateMachine
    {
        private readonly Stack<INpcState> stateStack = new Stack<INpcState>();
        private INpcState currentState;

        public NpcStateMachine(INpcState initialState)
        {
            currentState = initialState;
        }
        
        public void StartStateMachine()
        {
            currentState.StartState();
        }
        
        public void UpdateStateMachine()
        {
            currentState.UpdateState();
        }
        
        public void ChangeState(INpcState nextState)
        {
            stateStack.Push(currentState);
            currentState.ExitState();
            currentState = nextState;
            currentState.StartState();
        }

        public void ReverseState()
        {
            if (stateStack.Count <= 0) return;
            currentState.ExitState();
            currentState = stateStack.Pop();
            currentState.StartState();
        }
    }
}