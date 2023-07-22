using UnityEngine;
using Pathfinding;

namespace ProjectVietnam
{
    public class MoveOrderPathfinding : MonoBehaviour, IMoveOrder
    {
        private Transform targetTransform;
        private AIDestinationSetter destinationSetter;

        private void Awake()
        {
            destinationSetter = GetComponent<AIDestinationSetter>();
        }

        private void Start()
        {
            CreateMoveTarget();
            SetMovePosition(transform.position);
        }

        private void OnDestroy()
        {
            if (targetTransform == null)
            {
                return;
            }

            Destroy(targetTransform.gameObject);
            targetTransform = null;
        }

        private void CreateMoveTarget()
        {
            targetTransform = new GameObject().transform;
            targetTransform.name = gameObject.name + "_target";
            targetTransform.parent = LevelManager.Instance.DynamicTransform;

            destinationSetter.target = targetTransform;
        }

        public void SetMovePosition(Vector3 newMovePosition)
        {
            if (targetTransform == null)
            {
                return;
            }

            targetTransform.position = newMovePosition;
        }

        private void OnDrawGizmosSelected()
        {
            if (targetTransform == null)
            {
                return;
            }

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetTransform.position, 0.25f);
        }
    }
}
