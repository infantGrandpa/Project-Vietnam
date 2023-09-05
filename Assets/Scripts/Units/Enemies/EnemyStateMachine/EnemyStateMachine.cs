using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectVietnam
{
    [RequireComponent(typeof(EnemyBehaviour))]

    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyBehaviour enemyBehaviour;
        private EnemyStateBase currentState;

        [SerializeField, ReadOnly] string currentCommand = "None";

        private void Awake()
        {
            enemyBehaviour = GetComponent<EnemyBehaviour>();
            GetNewInstructions();
            currentState = new EnemyIdleState(enemyBehaviour);
        }

        private void GetNewInstructions()
        {
            EnemyCommand newCommand = EnemyStatePlanner.Instance.GetNewCommand(enemyBehaviour);

            switch (newCommand.commandType)
            {
                case EnemyCommandType.move:
                    NewMoveState(newCommand.targetPosition);
                    break;
                case EnemyCommandType.interact:
                    DebugHelper.Log("Interact State.");
                    break;
                default:
                    NewIdleState();
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

            GetNewInstructions();
        }

        private void NewIdleState()
        {
            if (currentState?.GetType() == typeof(EnemyIdleState))
            {
                DebugHelper.Log("Already in idle state.");
                return;
            }

            currentState = new EnemyIdleState(enemyBehaviour);
            currentState.EnterState();

            currentCommand = "Idle";
        }

        private void NewMoveState(Vector3? targetPosition)
        {
            if (targetPosition == null)
            {
                DebugHelper.LogError("Target position is null.");
                return;
            }

            EnemyGoToState moveState = new(enemyBehaviour);
            moveState.targetPosition = (Vector3)targetPosition;

            currentState = moveState;
            currentState.EnterState();

            currentCommand = "Move to " + targetPosition.ToString();
        }

    }
}
