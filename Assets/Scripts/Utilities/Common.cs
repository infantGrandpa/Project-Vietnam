using UnityEngine;
using System.Collections.Generic;

namespace ProjectVietnam
{
    public static class Common
    {
        public static bool IsValidFloat(float value)
        {
            return !float.IsNaN(value) && !float.IsInfinity(value);
        }

        public static void ShuffleList<T>(List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = UnityEngine.Random.Range(0, n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static void ApplyEffectsToTarget(List<EffectScriptableObject> effectsToApply, EffectTarget effectTarget)
        {
            foreach (EffectScriptableObject thisEffect in effectsToApply)
            {
                thisEffect.ApplyEffect(effectTarget);
            }
        }

        public static void RemoveEffectsFromTarget(List<EffectScriptableObject> effectsToRemove, EffectTarget effectTarget)
        {
            foreach (EffectScriptableObject thisEffect in effectsToRemove)
            {
                thisEffect.RemoveEffect(effectTarget);
            }
        }
    }
}
