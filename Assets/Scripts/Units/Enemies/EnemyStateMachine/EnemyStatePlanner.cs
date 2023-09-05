using UnityEngine;

namespace ProjectVietnam
{
    public class EnemyStatePlanner : MonoBehaviour
    {
        public static EnemyStatePlanner Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(EnemyStatePlanner)) as EnemyStatePlanner;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static EnemyStatePlanner instance;

        [SerializeField] Vector3 targetTestPosition;

        public EnemyCommand GetNewCommand(EnemyBehaviour enemyToCommand)
        {
            if (!IsUnitAtTargetPosition(enemyToCommand.transform))
            {
                return CreateMoveToTestPositionCommand();
            }


            EnemyCommand newCommand = new EnemyCommand();
            return newCommand;
        }

        private bool IsUnitAtTargetPosition(Transform unitTransform)
        {
            float distanceToTargetPosition = Vector3.Distance(unitTransform.position, targetTestPosition);
            return distanceToTargetPosition <= 3f;

        }

        private EnemyCommand CreateMoveToTestPositionCommand()
        {
            EnemyCommand newCommand = new EnemyCommand(EnemyCommandType.move, targetTestPosition);
            return newCommand;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetTestPosition, 0.5f);
        }

    }
}
