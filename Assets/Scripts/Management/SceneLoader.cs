using UnityEngine.SceneManagement;

namespace SniperProject
{
    public class SceneLoader
    {
        public enum Scene
        {
            MainMenu, GameScene
        }

        public static void LoadScene(Scene sceneToLoad)
        {
            SceneManager.LoadScene(sceneToLoad.ToString());
        }

    }
}
