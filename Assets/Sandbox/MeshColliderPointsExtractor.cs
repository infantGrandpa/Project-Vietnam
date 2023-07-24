using UnityEngine;
using Pathfinding;

namespace ProjectVietnam
{
    public class MeshColliderPointsExtractor : MonoBehaviour
    {
        // The array to store the collider's points
        Vector3[] colliderPoints;
        private MeshCollider meshCollider;
        private GraphUpdateScene graphUpdateScene;

        private void Awake()
        {
            GetComponents();
        }

        void Start()
        {
            Populate();
        }

        private void GetComponents() {
            graphUpdateScene = GetComponent<GraphUpdateScene>();
            meshCollider = GetComponent<MeshCollider>();
        }

        [ContextMenu("Populate")]
        private void Populate() {
            GetComponents();
            GetColliderPoints();
            PopulateGraphUpdatePoints();
        }

        private void GetColliderPoints()
        {
            if (meshCollider == null)
            {
                DebugHelper.LogMissingComponent(gameObject, meshCollider);
                return;
            }

            // Get the shared mesh of the MeshCollider
            Mesh mesh = meshCollider.sharedMesh;
            if (mesh == null)
            {
                DebugHelper.LogError("Mesh not found. Make sure the MeshCollider has a valid shared mesh.");
                return;
            }

            // Get the vertices (points) of the mesh
            Vector3[] vertices3D = mesh.vertices;

            // Convert the Vector3 vertices to Vector2 points
            colliderPoints = new Vector3[vertices3D.Length];
            for (int i = 0; i < vertices3D.Length; i++)
            {
                colliderPoints[i] = new Vector3(vertices3D[i].x, vertices3D[i].y);
            }
        }

        private void PopulateGraphUpdatePoints()
        {
            if (graphUpdateScene == null)
            {
                DebugHelper.LogMissingComponent(gameObject, graphUpdateScene);
                return;
            }

            graphUpdateScene.points = colliderPoints;
        }

        void OnDrawGizmos()
        {
            if (colliderPoints != null)
            {
                // Set the color of the wire spheres
                Gizmos.color = Color.yellow;

                // Draw a wire sphere at each point in the colliderPoints array
                foreach (Vector2 point in colliderPoints)
                {
                    // Get the world position of the point
                    Vector3 worldPosition = transform.TransformPoint(new Vector3(point.x, point.y, 0f));

                    // Draw the wire sphere at the world position with a radius of 0.25
                    Gizmos.DrawWireSphere(worldPosition, 0.25f);
                }
            }
        }
    }
}
