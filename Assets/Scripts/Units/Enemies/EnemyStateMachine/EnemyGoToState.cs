using UnityEngine;

namespace ProjectVietnam
{
    public class EnemyGoToState : EnemyStateBase
    {

        public EnemyGoToState(EnemyBehaviour newEnemyBehaviour)
        {
            enemyBehaviour = newEnemyBehaviour;
        }

        public override void EnterState()
        {
            enemyBehaviour.SetNewDestination(targetPosition);
        }

        public override void ExitState()
        {
            DebugHelper.Log("Exiting go to state.");
        }

        public override bool IsEqualToCommandType(EnemyCommandType enemyCommandType)
        {
            return enemyCommandType == EnemyCommandType.move;
        }

        public override bool IsStateComplete()
        {
            return enemyBehaviour.HasArrivedAtDestination();
        }

        public override void UpdateState()
        {
            DebugHelper.Log("In go to state.");
        }
    }
}
