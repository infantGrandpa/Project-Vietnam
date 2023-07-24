using UnityEngine;

namespace ProjectVietnam
{
    public class Draw2DColliderGizmo : MonoBehaviour
    {

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            DrawCollider();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            DrawCollider();
        }

        private void DrawCollider()
        {
            Collider2D myCollider = GetComponent<Collider2D>();

            switch (myCollider)
            {
                case BoxCollider2D boxCollider:
                    DrawBoxCollider(boxCollider);
                    break;
                case CircleCollider2D circleCollider:
                    DrawCircleCollider(circleCollider);
                    break;
                // Add other specific collider types here if needed.

                default:
                    DrawDefaultCollider(myCollider);
                    break;
            }
        }

        private void DrawBoxCollider(BoxCollider2D boxCollider)
        {
            Vector3 center = transform.TransformPoint(boxCollider.offset);
            Vector3 size = boxCollider.size * transform.localScale;
            Gizmos.DrawWireCube(center, size);
        }

        private void DrawCircleCollider(CircleCollider2D circleCollider)
        {
            Vector3 center = transform.TransformPoint(circleCollider.offset);
            float radius = circleCollider.radius * Mathf.Max(transform.localScale.x, transform.localScale.y);
            Gizmos.DrawWireSphere(center, radius);
        }

        private void DrawDefaultCollider(Collider2D collider)
        {
            Bounds bounds = collider.bounds;
            Gizmos.DrawWireCube(bounds.center, bounds.size);
        }
    }

}
