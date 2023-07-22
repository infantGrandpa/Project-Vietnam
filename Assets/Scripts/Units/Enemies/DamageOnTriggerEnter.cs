using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace ProjectVietnam
{
    [RequireComponent(typeof(FactionBehaviour))]
    public class DamageOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] float secsBeforeCanDamage;
        [SerializeField, MinMaxSlider(0, 1000, true)] Vector2Int damageToDeal;
        [SerializeField, MinMaxSlider(0, 1000, true)] Vector2Int damageToTake = Vector2Int.zero;

        private HealthSystem myHealthSystem;
        private FactionBehaviour myFactionBehaviour;

        [SerializeField] UnityEvent onDamageEvent;

        private bool canDamage;

        private void Awake()
        {
            myHealthSystem = GetComponent<HealthSystem>();
            myFactionBehaviour = GetComponent<FactionBehaviour>();
        }

        private void Start()
        {
            canDamage = false;
            StartCoroutine(DelayDamageCoroutine());
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            DamageObject(collision.gameObject);
        }

        private void DamageObject(GameObject objectToDamage)
        {
            if (!canDamage)
            {
                return;
            }

            
            if (!IsEnemy(objectToDamage))
            {
                return;
            }
            

            if (!objectToDamage.TryGetComponent(out IDamageable damageableObject))
            {
                return;
            }

            damageableObject.Damage(GetDamage(damageToDeal));
            DamageSelf();
            onDamageEvent?.Invoke();
        }

        private void DamageSelf()
        {
            if (myHealthSystem == null)
            {
                return;
            }

            myHealthSystem.Damage(GetDamage(damageToTake));
        }

        private bool IsEnemy(GameObject objectToDamage)
        {
            if (!objectToDamage.TryGetComponent(out FactionBehaviour factionBehaviour))
            {
                return false;
            }

            if (myFactionBehaviour.IsAllied(factionBehaviour))
            {
                return false;
            }

            return true;
        }

        private int GetDamage(Vector2Int damageRange)
        {
            return Random.Range(damageRange.x, damageRange.y);
        }

        private IEnumerator DelayDamageCoroutine()
        {
            yield return new WaitForSeconds(secsBeforeCanDamage);
            canDamage = true;
        }
    }
}
