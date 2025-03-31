using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class TeleportingState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        
        public TeleportingState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            TeleportToRandomPosition();
            stateMachine.ChangeState(States.CHASING);
        }

        public void Update() { }

        public void OnStateExit() { }
        
        private void TeleportToRandomPosition() => Owner.Agent.Warp(GetRandomNavMeshPoint());
        
        private Vector3 GetRandomNavMeshPoint()
        {
            Vector3 randomDirection = Random.insideUnitSphere * 5f + Owner.Position;
            UnityEngine.AI.NavMeshHit hit;
            
            if (UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out hit, 5f, UnityEngine.AI.NavMesh.AllAreas))
                return hit.position;
    
            return Owner.Data.SpawnPosition;
        }
    }
}
