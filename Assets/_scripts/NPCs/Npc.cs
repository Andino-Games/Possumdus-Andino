using _scripts.Interfaces;
using _scripts.NPCs.States;
using DialogueEditor;
using UnityEngine;
using UnityEngine.AI;

namespace _scripts.NPCs
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public abstract class Npc : MonoBehaviour, IObjectsInteract
    {
        private NpcStateMachine npcStateMachine;
        [SerializeField] protected Transform[] waypoints;
        private NavMeshAgent agent;
        [SerializeField] protected NPCConversation npcConversation;
        [SerializeField] protected Animator npcAnimation;
        
        private bool isTalking = false;
        private bool isPlayerInRange = false;
        private WalkingState walkingState;
        private TalkingState talkingState;

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            npcAnimation = GetComponent<Animator>();
            
            walkingState = new WalkingState(agent, waypoints);
            talkingState = new TalkingState(npcConversation);
            npcStateMachine = new NpcStateMachine(walkingState);
        }

        public virtual void Start()
        {
            npcStateMachine.StartStateMachine();
        }

        public virtual void Update()
        {
            npcStateMachine.UpdateStateMachine();
        }

        public virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = true;
                agent.isStopped = true;
                npcAnimation.SetBool("IsIdle", true);
            }
        }

        public virtual void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInRange = false;
                agent.isStopped = false;
                npcAnimation.SetBool("IsIdle", false);
            }
        }

        public virtual void OnInteract()
        {
            if (!isPlayerInRange) return;
            
            if (!isTalking)
            {
                isTalking = true;
                npcStateMachine.ChangeState(talkingState);
            }
            else
            {
                isTalking = false;
                npcStateMachine.ChangeState(walkingState);
            }
        }

        public virtual void OnRelease()
        {
            
        }
    }
}
