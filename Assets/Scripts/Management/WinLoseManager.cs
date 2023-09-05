using UnityEngine;

namespace ProjectVietnam
{
    public class WinLoseManager : MonoBehaviour
    {
        public static WinLoseManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(WinLoseManager)) as WinLoseManager;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static WinLoseManager instance;

        public void WinGame()
        {

        }

        public void LoseGame() {
            
        }
    }

}
