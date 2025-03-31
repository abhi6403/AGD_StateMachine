using StatePattern.Main;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class CloningState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        
        public CloningState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;
        
        public void OnStateEnter()
        {
            CreateAClone();
            CreateAClone();
        }
        public void Update() { }

        public void OnStateExit() { }

        private void CreateAClone()
        {
            ClonemanController clonedman = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as ClonemanController;
            clonedman.SetCloneCount((Owner as ClonemanController).CloneCountLeft - 1);
            clonedman.Teleport();
            clonedman.SetDefaultColor(EnemyColorType.Clone);
            clonedman.ChangeColor(EnemyColorType.Clone);
            GameService.Instance.EnemyService.AddEnemy(clonedman);
        }
    }
}
