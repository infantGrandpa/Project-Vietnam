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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            EnemyCommand other = (EnemyCommand)obj;
            bool isSameCommandType = commandType == other.commandType;
            bool isSamePosition = targetPosition == other.targetPosition;

            return isSameCommandType && isSamePosition;
        }

        public override int GetHashCode()
        {
            return commandType.GetHashCode() ^ targetPosition.GetHashCode();
        }

    }
}
