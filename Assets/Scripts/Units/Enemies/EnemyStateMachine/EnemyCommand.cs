using UnityEngine;

namespace ProjectVietnam
{
    public class EnemyCommand
    {
        public EnemyCommandType commandType;
        public Vector3 targetPosition;

        public EnemyCommand(EnemyCommandType type, Vector3 position)
        {
            commandType = type;
            targetPosition = position;
        }

    }
}
