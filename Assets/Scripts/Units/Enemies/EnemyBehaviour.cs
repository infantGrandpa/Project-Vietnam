using Pathfinding;
using UnityEngine;

namespace ProjectVietnam
{
    [RequireComponent(typeof(AIPath))]
    public class EnemyBehaviour : MonoBehaviour
    {
        private AIPath myPath;

        private void Awake()
        {
            myPath = GetComponent<AIPath>();
        }

        public bool HasArrivedAtDestination()
        {
            return myPath == null;
        }
    }
}
