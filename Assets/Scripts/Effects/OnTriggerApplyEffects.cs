using UnityEngine;
using System.Collections.Generic;

namespace ProjectVietnam
{
    public class OnTriggerApplyEffects : MonoBehaviour
    {
        [SerializeField] List<EffectScriptableObject> effectsToApply = new();

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out EffectTarget effectTarget))
            {
                return;
            }

            Common.ApplyEffectsToTarget(effectsToApply, effectTarget);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out EffectTarget effectTarget))
            {
                return;
            }

            Common.RemoveEffectsFromTarget(effectsToApply, effectTarget);
        }
    }
}
