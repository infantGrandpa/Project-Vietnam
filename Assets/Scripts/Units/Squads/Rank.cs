using UnityEngine;

namespace ProjectVietnam
{
    [CreateAssetMenu(fileName = "New Rank", menuName = "Rank")]
    public class Rank : ScriptableObject
    {
        public string rankName;
        public string rankAbbreviation;

        public int minPerSquad;
        public int payGrade;
    }
}
