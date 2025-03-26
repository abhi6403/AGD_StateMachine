using StatePattern.Player;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class PatrolManController : EnemyController
    {
        private PatrolManStateMachine stateMachine;
        
        public PatrolManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            CreateStateMachine();
            stateMachine.ChangeState(EnemyStates.IDLE);
        }
        
        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
            {
                return;
            }

            stateMachine.Update();
        }
        
        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(EnemyStates.CHASING);
        }
        
        private void CreateStateMachine() => stateMachine = new PatrolManStateMachine(this);
        
        public override void PlayerExitedRange() => stateMachine.ChangeState(EnemyStates.IDLE);
    }
}
