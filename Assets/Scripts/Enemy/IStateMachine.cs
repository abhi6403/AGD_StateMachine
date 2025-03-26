using UnityEngine;

namespace StatePattern.Enemy
{
    public interface IStateMachine
    {
        public void ChangeState(EnemyStates newState);
    }
}
