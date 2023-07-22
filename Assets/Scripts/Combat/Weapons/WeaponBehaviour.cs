using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace ProjectVietnam
{
    public class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] float secsBetweenBursts;
        [SerializeField, MinValue(1)] int shotsPerBurst;
        [SerializeField] float secsBetweenShotsInBursts;

        [SerializeField] GameObject bulletPrefab;

        private bool canFireBurst;

        [SerializeField] List<Transform> bulletSpawnPositions = new();

        private void Start()
        {
            canFireBurst = true;
        }

        public void FireWeapon(Transform target, int squadMembers = 1)
        {
            if (!canFireBurst)
            {
                return;
            }

            Coroutine newCoroutine = StartCoroutine(StartBurstFireCoroutine(target));
        }

        private IEnumerator StartBurstFireCoroutine(Transform target)
        {
            canFireBurst = false;

            int shotsFiredThisBurst = 0;
            while (shotsFiredThisBurst < shotsPerBurst)
            {
                yield return StartCoroutine(FireBullet(target));
                shotsFiredThisBurst++;
            }

            yield return new WaitForSeconds(secsBetweenBursts);

            canFireBurst = true;
        }

        private IEnumerator FireBullet(Transform target)
        {
            CreateBullet(target);
            yield return new WaitForSeconds(secsBetweenShotsInBursts);
        }

        private void CreateBullet(Transform target)
        {
            GameObject newBullet = Instantiate(bulletPrefab, GetBulletSpawnPosition(), Quaternion.identity);
            newBullet.transform.SetParent(LevelManager.Instance.DynamicTransform);
            
            if (!newBullet.TryGetComponent(out BulletBehaviour bulletBehaviour))
            {
                DebugHelper.LogMissingComponent(newBullet, bulletBehaviour);
                return;
            }

            bulletBehaviour.SetTarget(target);
        }

        private Vector2 GetBulletSpawnPosition()
        {
            if (bulletSpawnPositions.Count == 0)
            {
                DebugHelper.LogError("Bullet spawn positions is blank.");
                return Vector2.zero;
            }

            int randomIndex = Random.Range(0, bulletSpawnPositions.Count);
            Transform randomTransform = bulletSpawnPositions[randomIndex];
            return randomTransform.position;
        }
    }
}
