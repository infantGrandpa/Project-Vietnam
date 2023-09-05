using Pathfinding;
using UnityEngine;

namespace ProjectVietnam
{
    [RequireComponent(typeof(AIPath))]
    public class EnemyBehaviour : MonoBehaviour
    {
        private AIPath myPath;

        IMoveOrder moveOrder;

        private void Awake()
        {
            myPath = GetComponent<AIPath>();
            moveOrder = GetComponent<IMoveOrder>();
        }

        public bool HasArrivedAtDestination()
        {
            return myPath.reachedDestination;
        }

        public void SetNewDestination(Vector3 newPosition)
        {
            if (moveOrder == null)
            {
                return;
            }

            moveOrder.SetMovePosition(newPosition);
        }
    }
}
