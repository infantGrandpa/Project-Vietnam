using UnityEngine;
using System.Collections;

namespace ProjectVietnam
{
    public class AffinityIncreaser : MonoBehaviour
    {
        public float secsToIncrease;
        public int amountToIncrease;

        private RelationshipManager currentRelationshipManager;
        private Coroutine increaseCoroutine;

        private IEnumerator StartIncreaseCountdown()
        {
            yield return new WaitForSeconds(secsToIncrease);

            if (currentRelationshipManager == null)
            {
                yield break;
            }
            currentRelationshipManager.AdjustAffinity(amountToIncrease);

            StartCoroutine(StartIncreaseCountdown());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            RelationshipManager relationshipManager = collision.GetComponentInParent<RelationshipManager>();
            if (relationshipManager == null)
            {
                return;
            }

            currentRelationshipManager = relationshipManager;
            increaseCoroutine = StartCoroutine(StartIncreaseCountdown());
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            RelationshipManager relationshipManager = collision.GetComponentInParent<RelationshipManager>();
            if (relationshipManager == null || relationshipManager != currentRelationshipManager)
            {
                return;
            }

            StopCoroutine(increaseCoroutine);
            increaseCoroutine = null;

            currentRelationshipManager = null;
        }
    }
}
