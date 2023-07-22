using UnityEngine;

namespace ProjectVietnam
{
    public class CanvasBehaviour : MonoBehaviour
    {
        protected Canvas canvas;
        protected RectTransform canvasRectTransform;

        protected virtual void Awake()
        {
            canvas = GetComponent<Canvas>();
            canvasRectTransform = canvas.GetComponent<RectTransform>();
        }

        public virtual Vector2 ConvertWorldToCanvasPosition(Vector3 worldPosition)
        {
            Vector3 screenPosition = LevelManager.Instance.MainCamera.WorldToScreenPoint(worldPosition);
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, screenPosition, LevelManager.Instance.MainCamera, out Vector2 canvasPosition);

            return screenPosition;
        }
    }
}
