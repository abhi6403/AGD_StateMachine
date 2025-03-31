using StatePattern.Player;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class ClonemanController : EnemyController
    {
        private ClonemanStateMachine stateMachine;
        public int CloneCountLeft { get; private set; }
        
        public ClonemanController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneCount(enemyScriptableObject.CloneCount);
            enemyView.SetController(this);
            ChangeColor(EnemyColorType.Default);
            CreateStateMachine();
            stateMachine.ChangeState(States.IDLE);
        }
        
        private void CreateStateMachine() => stateMachine = new ClonemanStateMachine(this);
        
        public void SetCloneCount(int cloneCountToSet) => CloneCountLeft = cloneCountToSet;
        
        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            stateMachine.Update();
        }
        
        public override void PlayerEnteredRange(PlayerController targetToSet)
        {
            base.PlayerEnteredRange(targetToSet);
            stateMachine.ChangeState(States.CHASING);
        }
        
        public override void Die()
        {
            if (CloneCountLeft > 0)
            {
                stateMachine.ChangeState(States.CLONING);
            }
            base.Die();
        }
        public override void PlayerExitedRange() => stateMachine.ChangeState(States.IDLE);
        
        public void Teleport() => stateMachine.ChangeState(States.TELEPORTING);

        public void SetDefaultColor(EnemyColorType colorType) => enemyView.SetDefaultColor(colorType);

        public void ChangeColor(EnemyColorType colorType) => enemyView.ChangeColor(colorType);
    }
}
