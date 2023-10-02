using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectVietnam
{
    [RequireComponent(typeof(EnemyBehaviour))]

    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyBehaviour enemyBehaviour;
        private EnemyStateBase currentState;
        private EnemyCommand currentCommand;

        [SerializeField, ReadOnly] string currentCommandName = "None";

        private void Awake()
        {
            enemyBehaviour = GetComponent<EnemyBehaviour>();
            GetNewInstructions();
            currentState = new EnemyPatrolState(enemyBehaviour);
        }

        private void GetNewInstructions()
        {
            EnemyCommand newCommand = EnemyStatePlanner.Instance.GetNewCommand(enemyBehaviour);

            if (currentCommand == null || !currentCommand.Equals(newCommand))
            {
                currentCommand = newCommand;
            }

            ExecuteCommand();
        }

        private void ExecuteCommand()
        {
            if (currentCommand == null)
            {
                DebugHelper.LogError("Current command is null.");
                return;
            }

            switch (currentCommand.commandType)
            {
                case EnemyCommandType.move:
                    NewMoveState(currentCommand.targetPosition);
                    break;
                case EnemyCommandType.interact:
                    DebugHelper.Log("Interact State.");
                    break;
                default:
                    MoveToPatrolArea();
                    break;
            }
        }

        private void Update()
        {
            currentState.UpdateState();
            CheckStateStatus();
        }

        private void CheckStateStatus()
        {
            if (currentState == null)
            {
                return;
            }

            if (!currentState.IsStateComplete())
            {
                return;
            }

            if (!currentState.IsEqualToCommandType(currentCommand.commandType))
            {
                ExecuteCommand();
                return;
            }

            GetNewInstructions();
        }

        private void NewPatrolState(Vector3 targetPosition)
        {
            if (currentState?.GetType() == typeof(EnemyPatrolState))
            {
                DebugHelper.Log("Already in patrol state.");
                return;
            }

            currentState = new EnemyPatrolState(enemyBehaviour)
            {
                targetPosition = targetPosition
            };
            currentState.EnterState();

            currentCommandName = "Patrolling around " + targetPosition.ToString();
        }

        private void NewMoveState(Vector3 targetPosition)
        {
            EnemyGoToState moveState = new(enemyBehaviour)
            {
                targetPosition = targetPosition
            };

            currentState = moveState;
            currentState.EnterState();

            currentCommandName = "Move to " + targetPosition.ToString();
        }

        private void MoveToPatrolArea()
        {
            Vector3 targetPosition = currentCommand.targetPosition;

            if (!enemyBehaviour.IsAtPosition(targetPosition))
            {
                NewMoveState(targetPosition);
            }
            else
            {
                NewPatrolState(targetPosition);
            }
        }

    }
}
