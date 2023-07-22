using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectVietnam
{
    public class ExperienceTracker : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        public int CurrentExperience { get; private set; }

        [SerializeField, MinMaxSlider(0, 100, true)] Vector2Int startingExpRange;

        private void Awake()
        {
            CurrentExperience = Random.Range(startingExpRange.x, startingExpRange.y);
        }

        public void IncreaseExp(int increaseBy)
        {
            CurrentExperience += increaseBy;
        }
    }
}
