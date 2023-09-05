using UnityEngine;

namespace ProjectVietnam
{
    public class EnemyInteractState : EnemyStateBase
    {
        public EnemyInteractState(EnemyBehaviour newEnemyBehaviour)
        {
            enemyBehaviour = newEnemyBehaviour;
        }

        public override void EnterState()
        {
            DebugHelper.Log("Entering idle state.");
        }

        public override void ExitState()
        {
            DebugHelper.Log("Exiting idle state.");
        }

        public override bool IsStateComplete()
        {
            return true;
        }

        public override void UpdateState()
        {
            DebugHelper.Log("In idle state.");
        }
    }
}
