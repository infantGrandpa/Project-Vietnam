using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;

namespace ProjectVietnam
{
    public class HiddenEnemy : MonoBehaviour
    {
        public List<GameObject> objectsToHide = new();

        [AssetsOnly]
        public List<EffectScriptableObject> effectsWhenHidden = new();

        private EffectTarget effectTarget;

        private void Awake()
        {
            if (!TryGetComponent(out effectTarget))
            {
                DebugHelper.LogMissingComponent(gameObject, effectTarget);
            }            
        }

        private void Start()
        {
            HideEnemy();
        }

        [ContextMenu("Hide Enemy")]
        public void HideEnemy()
        {
            foreach(GameObject thisObject in objectsToHide)
            {
                thisObject.SetActive(false);
            }
            
            Common.ApplyEffectsToTarget(effectsWhenHidden, effectTarget);
        }

        [ContextMenu("Show Enemy")]
        public void ShowEnemy()
        {
            foreach (GameObject thisObject in objectsToHide)
            {
                thisObject.SetActive(true);
            }

            Common.RemoveEffectsFromTarget(effectsWhenHidden, effectTarget);
        }
    }
}
