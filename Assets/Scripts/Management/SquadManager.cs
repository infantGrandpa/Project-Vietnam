using UnityEngine;
using System.Collections.Generic;

namespace ProjectVietnam
{
    public class SquadManager : MonoBehaviour
    {
        public static SquadManager Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(SquadManager)) as SquadManager;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static SquadManager instance;

        public List<Squad> activeSquads = new();
        private int currentSquadInt = 1;
        [SerializeField] string squadPrefix;

        private List<string> squadNicknames = new()
            {
            "The Ghosts",
            "Bushwackers",
            "Rat Pack",
            "Steel Warriors",
            "Shadow Company",
            "Nightstalkers",
            "Iron Wolves",
            "Thunderbirds",
            "Death Dealers",
            "Savage Serpents"
            };

        private void Awake()
        {
            Common.ShuffleList(squadNicknames);
        }

        public int GetSquadNum()
        {
            int newInt = currentSquadInt;
            currentSquadInt++;
            return newInt;
        }

        public string GetSquadName(int squadNum)
        {
            string squadName = squadPrefix + " " + squadNum;
            
            return squadName;
        }

        
        public string GetSquadNickName()
        {
            string squadNickname = squadNicknames[0];
            
            //Move this nickname to the bottom
            squadNicknames.Remove(squadNickname);
            squadNicknames.Add(squadNickname);

            return squadNickname;
        }
    }
}
