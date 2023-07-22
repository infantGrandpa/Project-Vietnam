using UnityEngine;
using UnityEngine.Events;
using Sirenix.OdinInspector;

namespace ProjectVietnam
{
    public class HealthSystem : MonoBehaviour, IDamageable
    {
        public int maxHealth;
        [SerializeField] UnityEvent onTakeDamageEvent;

        [SerializeField] bool destroyOn0Health = true;

        [ShowInInspector, ReadOnly]
        public int CurrentHealth { get; private set; }

        [SerializeField] UnityEvent onDeathEvent;

        [SerializeField] bool isInvulnerable;

        private void Awake()
        {
            CurrentHealth = maxHealth;
        }

        public void Damage(int damageTaken)
        {
            if (isInvulnerable)
            {
                return;
            }

            CurrentHealth -= damageTaken;

            onTakeDamageEvent?.Invoke();

            if (CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            onDeathEvent?.Invoke();

            if (destroyOn0Health)
            {
                Destroy(gameObject);
            }
        }
    }
}
