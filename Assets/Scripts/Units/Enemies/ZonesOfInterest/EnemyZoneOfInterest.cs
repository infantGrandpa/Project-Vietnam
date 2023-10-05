using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections;

namespace ProjectVietnam
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class EnemyZoneOfInterest : MonoBehaviour
    {
        [Range(0, 100), SerializeField]
        int baseWeight = 25;

        [ShowInInspector, ReadOnly]
        public int CurrentWeight { get; private set; }

        private readonly float opacity = 0.4f;
        private int weightModifierLayerMask;

        private CircleCollider2D zoneCircle;

        [SerializeField] float secsBetweenRecalc = 4f;

        private void Awake()
        {
            zoneCircle = GetComponent<CircleCollider2D>();
            weightModifierLayerMask = 1 << LayerMask.NameToLayer("WeightModifier");
        }

        private void Start()
        {
            CalculateCurrentWeight();
            StartCoroutine(RecalculateWeightAfterDelay());
        }

        private void OnEnable()
        {
            EnemyStatePlanner.Instance.zonesOfInterest.Add(this);
        }

        private void OnDisable()
        {
            if (EnemyStatePlanner.Instance == null)
            {
                return;
            }
            EnemyStatePlanner.Instance.zonesOfInterest.Remove(this);
        }

        [ContextMenu("Calculate Current Weight")]
        public void CalculateCurrentWeight()
        {
            int newWeight = baseWeight;
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, zoneCircle.radius, weightModifierLayerMask);

            foreach (Collider2D hitCollider in hitColliders)
            {
                ZoneWeightModifier weightModifier = hitCollider.GetComponent<ZoneWeightModifier>();

                if (weightModifier == null) continue;

                newWeight += weightModifier.zoneImpactChange;
            }

            CurrentWeight = newWeight;
        }

        private IEnumerator RecalculateWeightAfterDelay()
        {
            yield return new WaitForSeconds(secsBetweenRecalc);

            CalculateCurrentWeight();

            StartCoroutine(RecalculateWeightAfterDelay());
        }

        void OnDrawGizmos()
        {
            if (zoneCircle == null)
            {
                zoneCircle = GetComponent<CircleCollider2D>();
            }

            float normalizedWeight = Mathf.Clamp01(baseWeight / 100.0f);

            Color zoneColor = Color.Lerp(Color.blue, Color.red, normalizedWeight);
            zoneColor = new Color(zoneColor.r, zoneColor.g, zoneColor.b, opacity);
            Gizmos.color = zoneColor;

            float radius = zoneCircle.radius;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
