using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using Sirenix.OdinInspector;

namespace ProjectVietnam
{
    public class BulletBehaviour : MonoBehaviour
    {
        private Transform target;
        [SerializeField] float moveSpeed;
        [SerializeField] TrailRenderer trail;

        [Header("On Hit")]
        [SerializeField, MinMaxSlider(0, 1000, true)] Vector2Int damageToDeal;
        [SerializeField] GameObject damagePrefab;
        [SerializeField] UnityEvent onHitTargetEvent;

        private Vector2 lastTargetPosition;

        public void SetTarget(Transform newTarget)
        {
            target = newTarget;
        }

        private void Start()
        {
            StartCoroutine(MoveBulletCoroutine());
        }

        private IEnumerator MoveBulletCoroutine()
        {
            Vector2 startPosition = transform.position;
            Vector2 targetPosition = GetTargetPosition();

            float initialDistance = Vector2.Distance(startPosition, targetPosition);

            float duration = initialDistance / moveSpeed;
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                targetPosition = GetTargetPosition();
                transform.position = Vector2.Lerp(startPosition, targetPosition, t);

                elapsedTime += Time.deltaTime;
                yield return null;
            }

            transform.position = targetPosition;
            HitTarget();

            DestroyAfterDelay();
        }

        private Vector2 GetTargetPosition()
        {
            if (target == null)
            {
                return lastTargetPosition;    
            }

            Vector2 newPosition = target.position;
            lastTargetPosition = newPosition;
            return newPosition;

        }

        #region Hit Target

        private void HitTarget()
        {
            CreateDamageObject();
            onHitTargetEvent?.Invoke();
        }

        private void CreateDamageObject()
        {
            if (target == null)
            {
                return;
            }

            GameObject instance = Instantiate(damagePrefab, LevelManager.Instance.DynamicTransform);
            instance.transform.position = transform.position;

            if (!instance.TryGetComponent(out DamageTargetOnCommand damage))
            {
                DebugHelper.LogMissingComponent(instance, damage);
                return;
            }

            damage.SetVars(target.gameObject, GetDamage());
        }

        private int GetDamage()
        {
            return Random.Range(damageToDeal.x, damageToDeal.y);
        }

        private void DestroyAfterDelay()
        {
            float delay = trail == null ? 0 : trail.time;
            Destroy(gameObject, delay);
        }

        #endregion
    }
}
