using UnityEngine;

namespace ProjectVietnam
{
    public class FactionBehaviour : MonoBehaviour
    {
        public Faction faction;

        public bool IsAllied(Faction factionToCheck)
        {
            if (factionToCheck == Faction.Neutral || faction == Faction.Neutral)
            {
                return true;
            }
            
            if (factionToCheck == faction)
            {
                return true;
            }

            return false;
        }

        public bool IsAllied(FactionBehaviour factionBehaviourToCheck)
        {
            return IsAllied(factionBehaviourToCheck.faction);
        }
    }
}
