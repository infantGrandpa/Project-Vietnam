using UnityEngine;

namespace ProjectVietnam
{
    [CreateAssetMenu(fileName = "New Sight Range Effect", menuName = "Effects/Sight Range Effect")]

    public class SightRangeEffect : EffectScriptableObject
    {
        public float sightRangeChange;

        public override void ApplyEffect(EffectTarget target)
        {
            if (!target.TryGetComponent(out WeaponHandler weaponHandler))
            {
                return;
            }

            weaponHandler.AffectWeaponRange(sightRangeChange);
        }

        public override void RemoveEffect(EffectTarget target)
        {
            if (!target.TryGetComponent(out WeaponHandler weaponHandler))
            {
                return;
            }

            weaponHandler.AffectWeaponRange(-sightRangeChange);
        }
    }
}
