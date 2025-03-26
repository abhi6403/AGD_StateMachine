using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class PatrolManStateMachine : IStateMachine
    {
        private PatrolManController Owner;
        private IState currentState;
        protected Dictionary<EnemyStates, IState> States = new Dictionary<EnemyStates, IState>();
        
        public PatrolManStateMachine(PatrolManController Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        public void Update() => currentState?.Update();
        private void CreateStates()
        {
            States.Add(EnemyStates.IDLE, new IdleState(this));
            States.Add(EnemyStates.PATROLLING, new PatrollingState(this));
            States.Add(EnemyStates.CHASING, new ChasingState(this));
            States.Add(EnemyStates.SHOOTING, new ShootingState(this));
            States.Add(EnemyStates.ROTATING, new RotatingState(this));
        }

        private void SetOwner()
        {
            foreach (IState state in States.Values)
            {
                state.Owner = Owner;
            }
        }
        
        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(EnemyStates newState) => ChangeState(States[newState]);
    }
}
