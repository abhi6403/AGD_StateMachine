using System.Collections;
using System.Collections.Generic;
using StatePattern.Enemy;
using UnityEngine;

namespace StatePattern.StateMachine
{
    public class GenericStateMachine<T> where T : EnemyController
    {
        protected T Owner;
        protected IState currentState;
        
        protected Dictionary<States, IState> States = new Dictionary<States, IState>();
        public GenericStateMachine(T owner) => this.Owner = owner;
        
        public void Update() => currentState?.Update();

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        protected void SetOwner()
        {
            foreach (IState state in States.Values)
            {
                state.Owner = Owner;
            }
        }
        public void ChangeState(States newState) => ChangeState(States[newState]);
    }

    public enum States
    {
        IDLE,
        ROTATING,
        SHOOTING,
        PATROLLING,
        CHASING
    }
}