using _scripts.Interfaces;
using UnityEngine;
using UnityEngine.AI;

namespace _scripts.NPCs.States
{
    public class WalkingState : INpcState
    {
        private int _currentWaypoint = 0;
        private readonly NavMeshAgent _agent;
        private readonly Transform[] _waypoints;

        public WalkingState(NavMeshAgent agent, Transform[] waypoints)
        {
            _agent = agent;
            _waypoints = waypoints;
        }
        public void StartState()
        {
            Debug.Log("Entró al estado de Walking"); 
            //TODO: Animation
        }

        public void UpdateState()
        {
            if (_agent.remainingDistance <= 0.5f && !_agent.pathPending)
            {
                MoveToNextWaypoint();
            }
        }

        public void ExitState()
        {
            Debug.Log("Salió del estado Walking");
        }
        
        private void MoveToNextWaypoint()
        {
            if (_waypoints.Length == 0) return;
            _agent.SetDestination(_waypoints[_currentWaypoint].position);
            
            _currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
        }
    }
}