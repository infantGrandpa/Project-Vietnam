using UnityEngine;
using UnityEngine.Events;

namespace ProjectVietnam
{
    public class ObjectEventSystem : MonoBehaviour
    {
        [SerializeField] UnityEvent onAwakeEvent;
        [SerializeField] UnityEvent onStartEvent;
        [SerializeField] UnityEvent onUpdateEvent;
        [SerializeField] UnityEvent onDestroyEvent;
        [SerializeField] UnityEvent onEnableEvent;
        [SerializeField] UnityEvent onDisableEvent;

        private void Awake()
        {
            onAwakeEvent?.Invoke();
        }

        private void Start()
        {
            onStartEvent?.Invoke();
        }

        private void Update()
        {
            onUpdateEvent?.Invoke();
        }

        private void OnDestroy()
        {
            onDestroyEvent?.Invoke();
        }

        private void OnEnable()
        {
            onEnableEvent?.Invoke();
        }

        private void OnDisable()
        {
            onDisableEvent?.Invoke();
        }
    }
}
