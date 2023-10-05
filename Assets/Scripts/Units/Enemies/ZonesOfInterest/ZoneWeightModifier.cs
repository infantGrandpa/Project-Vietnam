using UnityEngine;

namespace ProjectVietnam
{
    public abstract class ZoneWeightModifier : MonoBehaviour
    {
        public int zoneImpactChange;

        public abstract float ModifyWeight(EnemyZoneOfInterest zone);

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, Vector3.one);
        }
    }
}
