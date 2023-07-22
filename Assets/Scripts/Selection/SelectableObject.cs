using UnityEngine;

namespace ProjectVietnam
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class SelectableObject : MonoBehaviour
    {
        [SerializeField] protected GameObject selectIndicator;

        public virtual void MoveToPosition(Vector3 targetPosition)
        {
            //Do Nothing
        }

        public void SelectObject()
        {
            selectIndicator.SetActive(true);
        }

        public void DeselectObject()
        {
            if (selectIndicator == null)
            {
                return;
            }

            selectIndicator.SetActive(false);
        }
    }
}
