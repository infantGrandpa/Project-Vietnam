using UnityEngine;
using UnityEngine.Events;

namespace ProjectVietnam
{
    public class DamageTargetOnCommand : MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField] int damageToDeal;

        [SerializeField] int damageToTakeOnCollision = 0;

        private HealthSystem myHealthSystem;

        [SerializeField] UnityEvent onDamageEvent;

        private void Awake()
        {
            myHealthSystem = GetComponent<HealthSystem>();
        }

        public void SetVars(GameObject newTarget, int newDamage)
        {
            target = newTarget;
            damageToDeal = newDamage;
        }

        public void DamageTarget()
        {
            if (target == null)
            {
                DebugHelper.LogError("Target is null.");
                return;
            }

            if (!target.TryGetComponent(out IDamageable damageableObject))
            {
                DebugHelper.LogWarning(target.name + " is missing an IDamageable component.");
                return;
            }

            damageableObject.Damage(damageToDeal);
            DamageSelf();
            onDamageEvent?.Invoke();
        }

        private void DamageSelf()
        {
            if (myHealthSystem == null)
            {
                return;
            }

            myHealthSystem.Damage(damageToTakeOnCollision);
        }

    }
}
