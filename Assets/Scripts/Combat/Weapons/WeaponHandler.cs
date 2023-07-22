using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace ProjectVietnam
{
    [RequireComponent(typeof(FactionBehaviour))]
    public class WeaponHandler : MonoBehaviour
    {
        [ValidateInput("IsGreaterThan0", "This value should be greater than 0.", InfoMessageType.Warning)]
        public float baseWeaponRange;
        private float affectedWeaponRange = 0f;

        private FactionBehaviour factionBehaviour;

        [SerializeField] WeaponBehaviour weaponBehaviour;

        private void Awake()
        {
            if (!TryGetComponent(out factionBehaviour))
            {
                DebugHelper.LogMissingComponent(gameObject, factionBehaviour);
            }
        }

        private void Update()
        {
            Transform target = CheckForEnemies();
            FireAtTarget(target);
        }


        #region Target Acquisition
        private Transform CheckForEnemies()
        {
            Collider2D[] nearbyColliders = Physics2D.OverlapCircleAll(transform.position, GetTotalWeaponRange());

            float minDistanceToEnemy = Mathf.Infinity;
            Transform closestEnemy = null;

            foreach (Collider2D thisCollider in nearbyColliders)
            {
                Transform enemyTransform = thisCollider.transform;

                if (!IsEnemyTarget(enemyTransform))
                {
                    continue;
                }

                if (!IsDamageable(enemyTransform))
                {
                    continue;
                }


                float distanceToEnemy = Vector3.Distance(transform.position, enemyTransform.position);
                if (distanceToEnemy > minDistanceToEnemy)
                {
                    continue;
                }


                if (!CanSeeTarget(enemyTransform))
                {
                    continue;
                }
                
                closestEnemy = enemyTransform;
                minDistanceToEnemy = distanceToEnemy;
            }

            return closestEnemy;
        }


        private bool IsEnemyTarget(Transform targetTransform)
        {
            if (!targetTransform.TryGetComponent(out FactionBehaviour otherFaction))
            {
                return false;
            }

            bool isAllied = factionBehaviour.IsAllied(otherFaction);
            if (isAllied)
            {
                return false;
            }

            return true;
        }

        private bool IsDamageable(Transform targetTransform)
        {
            if (!targetTransform.TryGetComponent(out IDamageable damageable))
            {
                 return false;
            }

            return true;
        }

        private bool CanSeeTarget(Transform targetTransform)
        {
            return true;

            ////Check vision cone
            //Vector3 vectorToEnemy = targetTransform.position - transform.position;

            ////Check if there's an obstacle between us and enemy
            //LayerMask obstacleLayer = LayerMask.GetMask("Obstacles");
            //if (Physics2D.Raycast(transform.position, vectorToEnemy, vectorToEnemy.magnitude, obstacleLayer))
            //{
            //    return false;
            //}

            //return true;
        }

        #endregion

        #region WeaponRange
        public void AffectWeaponRange(float affectBy)
        {
            affectedWeaponRange += affectBy;
        }

        private float GetTotalWeaponRange()
        {
            float totalWeaponRange = baseWeaponRange + affectedWeaponRange;
            totalWeaponRange = Mathf.Max(totalWeaponRange, 0f);
            return totalWeaponRange;
        }

        #endregion

        private void FireAtTarget(Transform target)
        {
            if (target == null)
            {
                return;
            }

            weaponBehaviour.FireWeapon(target);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, GetTotalWeaponRange());
        }

        private bool IsGreaterThan0(float value)
        {
            return value > 0;
        }
    }
}
