using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ProjectVietnam
{
    public class NameDataLoader : MonoBehaviour
    {
        public static NameDataLoader Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(NameDataLoader)) as NameDataLoader;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static NameDataLoader instance;

        private List<string> firstNames;
        private List<string> lastNames;

        private void Awake()
        {
            firstNames = CSVReader.ReadToStringList("SoldierGeneration/first-names");
            lastNames = CSVReader.ReadToStringList("SoldierGeneration/last-names");
        }

        public string GetRandomFirstName()
        {
            if (firstNames.Count == 0)
            {
                DebugHelper.LogError("First Name list is empty!");
                return null;
            }

            int randomIndex = Random.Range(0, firstNames.Count);
            return firstNames[randomIndex];
        }

        public string GetRandomLastName()
        {
            if (lastNames.Count == 0)
            {
                DebugHelper.LogError("Last Name list is empty!");
                return null;
            }

            int randomIndex = Random.Range(0, lastNames.Count);
            return lastNames[randomIndex];
        }
    }
}
