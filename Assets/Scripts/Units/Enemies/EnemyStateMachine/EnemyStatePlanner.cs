using UnityEngine;

namespace ProjectVietnam
{
    public class EnemyStatePlanner : MonoBehaviour
    {
        public static EnemyStatePlanner Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(EnemyStatePlanner)) as EnemyStatePlanner;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static EnemyStatePlanner instance;

        public EnemyCommand GetNewCommand()
        {
            EnemyCommand newCommand = new EnemyCommand();
            return newCommand;
        }
    }
}
