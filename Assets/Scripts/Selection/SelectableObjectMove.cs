using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectVietnam
{
    public class SelectableObjectMove : SelectableObject
    {
        private IMoveOrder moveOrder;
        private RotateObject rotateObject;

        private void Awake()
        {
            GetComponents();
        }

        private void GetComponents()
        {
            if (!TryGetComponent(out moveOrder))
            {
                Debug.LogWarning("ERROR SelectableObject GetComponents(): " + gameObject.name + " is missing an IMoveOrder component.");
            }

            rotateObject = GetComponent<RotateObject>();
        }


        public override void MoveToPosition(Vector3 targetPosition)
        {
            if (moveOrder == null)
            {
                return;
            }

            moveOrder.SetMovePosition(targetPosition);

            if (rotateObject == null)
            {
                return;
            }

            rotateObject.RotateTowardsPosition(targetPosition);
        }
    }
}