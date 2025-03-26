using System.Collections.Generic;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine
    {
        private OnePunchManController Owner;
        private IState currentState;
        protected Dictionary<EnemyStates, IState> States = new Dictionary<EnemyStates, IState>();

        public OnePunchManStateMachine(OnePunchManController Owner)
        {
            this.Owner = Owner;
            CreateStates();
            SetOwner();
        }

        private void CreateStates()
        {
            States.Add(EnemyStates.IDLE, new IdleState(this));
            States.Add(EnemyStates.ROTATING, new RotatingState(this));
            States.Add(EnemyStates.SHOOTING, new ShootingState(this));
        }

        private void SetOwner()
        {
            foreach(IState state in States.Values)
            {
                state.Owner = Owner;
            }
        }

        public void Update() => currentState?.Update();

        protected void ChangeState(IState newState)
        {
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }

        public void ChangeState(EnemyStates newState) => ChangeState(States[newState]);
    }
}