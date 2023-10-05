using System.Collections.Generic;
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

        [Header("Patrol Orders")]
        [SerializeField] Vector3 targetPatrolHome;
        public float patrolRadius;
        public float secsToPatrolBeforeNewCommand;

        [Header("Zones of Interest")]
        public List<EnemyZoneOfInterest> zonesOfInterest = new();

        [Header("Testing & Debugging")]
        [SerializeField] bool skipMoveCommand;

        public EnemyCommand GetNewCommand(EnemyBehaviour enemyToCommand)
        {
            if (skipMoveCommand || enemyToCommand.IsAtPosition(targetTestPosition))
            {
                return CreateMoveToTestPositionCommand();
            }

            return CreatePatrolCommand();
        }

        private EnemyCommand CreateMoveToTestPositionCommand()
        {
            EnemyCommand newCommand = new EnemyCommand(EnemyCommandType.move, targetTestPosition);
            return newCommand;
        }

        private EnemyCommand CreatePatrolCommand()
        {
            EnemyCommand newCommand = new EnemyCommand(EnemyCommandType.patrol, targetPatrolHome);
            return newCommand;
        }

        

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetTestPosition, 0.5f);

            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(targetPatrolHome, 0.5f);
            Gizmos.DrawWireSphere(targetPatrolHome, patrolRadius);
        }

    }
}
