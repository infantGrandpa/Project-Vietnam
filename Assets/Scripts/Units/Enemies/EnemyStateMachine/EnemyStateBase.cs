using UnityEngine;

namespace ProjectVietnam
{
    public abstract class EnemyStateBase
    {
        protected EnemyBehaviour enemyBehaviour;
        public Vector3 targetPosition;
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
        public abstract bool IsStateComplete();
        public abstract bool IsEqualToCommandType(EnemyCommandType enemyCommandType);
    }
}
