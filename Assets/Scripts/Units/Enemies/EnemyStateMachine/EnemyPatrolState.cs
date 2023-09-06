using UnityEngine;

namespace ProjectVietnam
{
    public class EnemyPatrolState : EnemyStateBase
    {

        private float secsToPatrol;
        private float secsPatrolling;

        public EnemyPatrolState(EnemyBehaviour newEnemyBehaviour)
        {
            enemyBehaviour = newEnemyBehaviour;
        }

        public override void EnterState()
        {
            Vector3 patrolPosition = GetPatrolPosition();
            enemyBehaviour.SetNewDestination(patrolPosition);
            StartPatrolCountdown();
        }

        public override void ExitState()
        {
            DebugHelper.Log("Exiting patrol state.");
        }

        public override bool IsEqualToCommandType(EnemyCommandType enemyCommandType)
        {
            return enemyCommandType == EnemyCommandType.patrol;
        }

        public override bool IsStateComplete()
        {
            return secsPatrolling >= secsToPatrol;
        }

        public override void UpdateState()
        {
            secsPatrolling += Time.deltaTime;

            if (!enemyBehaviour.HasArrivedAtDestination())
            {
                return;
            }

            Vector3 patrolPosition = GetPatrolPosition();
            enemyBehaviour.SetNewDestination(patrolPosition);
        }

        private Vector3 GetPatrolPosition()
        {
            Vector2 randomPoint = Random.insideUnitCircle;
            randomPoint *= EnemyStatePlanner.Instance.patrolRadius;
            randomPoint += (Vector2)targetPosition;

            DebugHelper.Log("Patrolling at " + randomPoint.ToString() + " (near " + targetPosition.ToString() + ")");
            return randomPoint;
        }

        private void StartPatrolCountdown()
        {
            secsToPatrol = EnemyStatePlanner.Instance.secsToPatrolBeforeNewCommand;
            secsPatrolling = 0;
        }

    }
}
