using UnityEngine;

namespace ProjectVietnam
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] GameObject objectToRotate;
        [SerializeField] float secsToCompleteFullRotation;

        private Vector3 targetPosition;

        private void Awake()
        {
            if (objectToRotate == null)
            {
                objectToRotate = gameObject;
            }
        }

        private void Start()
        {
            RotateTowardsPosition(objectToRotate.transform.forward);
        }

        public void RotateTowardsPosition(Vector3 newTargetPosition)
        {
            targetPosition = newTargetPosition;
        }

        private void Update()
        {
            Vector3 dir = targetPosition - objectToRotate.transform.position;
            Quaternion rot = Quaternion.LookRotation(Vector3.forward, dir);
            objectToRotate.transform.rotation = Quaternion.Lerp(objectToRotate.transform.rotation, rot, Time.deltaTime * secsToCompleteFullRotation);
        }
    }
}
