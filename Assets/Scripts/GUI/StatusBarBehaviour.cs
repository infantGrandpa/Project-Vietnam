using UnityEngine;
using UnityEngine.UI;
using CodeMonkey.Utils;
using DG.Tweening;

namespace ProjectVietnam
{
    public class StatusBarBehaviour : MonoBehaviour
    {
        [SerializeField] Image filledBarPart;
        private float maxWidth;
        private RectTransform barRectTransform;

        public bool isStatic;

        [Header("Tweening")]
        [SerializeField] float secsToTween = 1f;
        [SerializeField] Ease easeType;
        private Tween tween;


        private void Awake()
        {
            maxWidth = GetComponent<RectTransform>().sizeDelta.x;
            
            if (!filledBarPart.TryGetComponent(out barRectTransform))
            {
                DebugHelper.LogMissingComponent(filledBarPart.gameObject, barRectTransform);
            }
        }

        public void UpdateBar(float newPercentage)
        {
            newPercentage = Mathf.Clamp01(newPercentage);
            float newWidth = maxWidth * newPercentage;

            if (!Common.IsValidFloat(newWidth))
            {
                DebugHelper.LogError("Invalid newWidth value: " + newWidth + " | " + maxWidth.ToString() + " * " + newPercentage.ToString());
                return;
            }

            TweenToWidth(newWidth);
        }

        public void UpdateBar(float currentValue, float maxValue)
        {
            float percentage = currentValue / maxValue;
            if (!Common.IsValidFloat(percentage))
            {
                DebugHelper.LogError("Invalid percentage value: " + percentage + " | " + currentValue.ToString() + " / " + maxValue.ToString());
                return;
            }
            UpdateBar(percentage);
        }

        private void TweenToWidth(float newWidth)
        {

            float height = barRectTransform.sizeDelta.y;
            Vector2 newSizeDelta = new(newWidth, height);

            if (!ValidateTweenValues(newWidth, height))
            {
                return;
            }

            if (tween != null)
            {
                tween.Kill();
            }

            tween = barRectTransform.DOSizeDelta(newSizeDelta, secsToTween, true);
            tween.SetEase(easeType);
            tween.SetAutoKill(true);
        }

        public void SetBarPosition(Vector2 worldPosition)
        {
            if (isStatic)
            {
                return;
            }

            transform.position = SpatialCanvasBehaviour.Instance.ConvertWorldToCanvasPosition(worldPosition);
        }

        private bool ValidateTweenValues(float width, float height)
        {
            if (!Common.IsValidFloat(width))
            {
                DebugHelper.LogError("Invalid width value: " + width);
                return false;
            }

            if (!Common.IsValidFloat(height))
            {
                DebugHelper.LogError("Invalid height value: " + height);
                return false;
            }

            if (barRectTransform == null)
            {
                DebugHelper.LogError("barRectTransform is not assigned.");
                return false;
            }

            return true;
        }
                

        [ContextMenu("Test Update")]
        private void TestUpdate()
        {
            float newPercent = Random.value;
            UpdateBar(newPercent);
        }
    }
}
