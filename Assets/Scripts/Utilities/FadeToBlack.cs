using UnityEngine;

namespace ProjectVietnam
{
    public class FadeToBlack : MonoBehaviour
    {
        [SerializeField] GameObject fadeToBlackObject;
        public void StartFadeToBlack()
        {
            if (fadeToBlackObject == null)
            {
                DebugHelper.LogError("Fade to Black Object is null");
                return;
            }

            fadeToBlackObject.SetActive(true);
        }
    }
}
