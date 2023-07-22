using UnityEngine;

namespace SniperProject
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] SceneLoader.Scene nextScene;

        public void LoadNextScene()
        {
            SceneLoader.LoadScene(nextScene);
        }
    }
}
