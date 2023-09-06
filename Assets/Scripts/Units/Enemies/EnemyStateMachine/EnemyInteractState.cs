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
            DebugHelper.Log("Entering interact state.");
        }

        public override void ExitState()
        {
            DebugHelper.Log("Exiting interact state.");
        }

        public override bool IsEqualToCommandType(EnemyCommandType enemyCommandType)
        {
            return enemyCommandType == EnemyCommandType.interact;
        }

        public override bool IsStateComplete()
        {
            return true;
        }

        public override void UpdateState()
        {
            DebugHelper.Log("In interact state.");
        }
    }
}
