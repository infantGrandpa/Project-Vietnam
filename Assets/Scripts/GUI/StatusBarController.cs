using UnityEngine;

namespace ProjectVietnam
{
    public class StatusBarController : MonoBehaviour
    {
        private float maxValue;

        [SerializeField] GameObject barPrefab;
        private StatusBarBehaviour barBehaviour;

        [SerializeField] Vector2 barOffset;


        private void Awake()
        {
            CreateBar();
        }

        private void Update()
        {
            Vector2 barWorldPosition = (Vector2)transform.position + barOffset;
            barBehaviour.SetBarPosition(barWorldPosition);
        }

        public void SetBarMax(float newMaxValue)
        {
            maxValue = newMaxValue;
        }

        private void CreateBar()
        {
            GameObject newBar = Instantiate(barPrefab);
            newBar.transform.SetParent(SpatialCanvasBehaviour.Instance.transform);

            if (!newBar.TryGetComponent(out barBehaviour))
            {
                DebugHelper.LogMissingComponent(newBar, barBehaviour);
                return;
            }
        }

        public void UpdateBarValue(float newValue)
        {
            if (maxValue == 0)
            {
                DebugHelper.LogError("Max Value is 0. Make sure to SetBarMax before updating the value.");
                return;
            }

            barBehaviour.UpdateBar(newValue, maxValue);
        }

        private void OnEnable()
        {
            if (barBehaviour == null)
            {
                return;
            }
            barBehaviour.gameObject.SetActive(true);
        }

        private void OnDisable()
        {
            if (barBehaviour == null)
            {
                return;
            }
            barBehaviour.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            if (barBehaviour == null)
            {
                return;
            }
            Destroy(barBehaviour.gameObject);
        }
    }
}
