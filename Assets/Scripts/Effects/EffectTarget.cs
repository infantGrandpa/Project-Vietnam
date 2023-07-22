using UnityEngine;
using System.Collections.Generic;

namespace ProjectVietnam
{
    public class EffectTarget : MonoBehaviour
    {
        private List<EffectScriptableObject> activeEffects = new();

        public void AddEffect(EffectScriptableObject effect)
        {
            activeEffects.Add(effect);
            effect.ApplyEffect(this);
        }

        public void RemoveEffect(EffectScriptableObject effect)
        {
            if (activeEffects.Contains(effect))
            {
                effect.RemoveEffect(this);
                activeEffects.Remove(effect);
            }
        }
    }
}
