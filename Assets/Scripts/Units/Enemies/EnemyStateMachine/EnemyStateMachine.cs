using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectVietnam
{
    [RequireComponent(typeof(EnemyBehaviour))]

    public class EnemyStateMachine : MonoBehaviour
    {
        private EnemyBehaviour enemyBehaviour;
        private EnemyStateBase currentState;

        [SerializeField, ReadOnly] string currentStateName = "None";

        private void Awake()
        {
            enemyBehaviour = GetComponent<EnemyBehaviour>();
            GetNewInstructions();
            currentState = new EnemyIdleState(enemyBehaviour);
        }

        private void GetNewInstructions()
        {
            EnemyCommand newCommand = EnemyStatePlanner.Instance.GetNewCommand();

            switch (newCommand.commandType)
            {
                case EnemyCommandType.move:
                    DebugHelper.Log("Move State.");
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

            currentStateName = "Idle";
        }

    }
}
