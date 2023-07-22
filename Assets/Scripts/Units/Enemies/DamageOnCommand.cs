using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectVietnam
{
    public class DamageOnCommand : MonoBehaviour
    {
        [SerializeField] GameObject target;
        [SerializeField, MinMaxSlider(0, 1000, true)] Vector2Int damage;

        [ContextMenu("Damage Target")]
        public void DamageTarget()
        {
            if (target == null)
            {
                DebugHelper.LogError("No target set.");
                return;
            }
            
            if (damage.x <= 0 && damage.y <= 0)
            {
                DebugHelper.LogWarning("Damage range is set to " + damage.ToString());
            }

            if (!target.TryGetComponent(out IDamageable damageable))
            {
                DebugHelper.LogError(target.name + " is missing a damagable component.");
                return;
            }

            damageable.Damage(GetDamage());
        }

        private int GetDamage()
        {
            return Random.Range(damage.x, damage.y);
        }
    }
}
