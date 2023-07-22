using UnityEngine;
using System.Collections.Generic;
using TMPro;

namespace ProjectVietnam
{
    public class EventLogBehaviour : MonoBehaviour
    {
        public static EventLogBehaviour Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(EventLogBehaviour)) as EventLogBehaviour;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static EventLogBehaviour instance;

        private List<TMP_Text> logEntries = new();
        public GameObject logPrefab;

        public int maxLines = 10;

        private void Start()
        {
            CreateLogObjects();
        }

        private void CreateLogObjects()
        {

            for(int i = 0; i < maxLines; i++)
            {
                GameObject newLogObject = Instantiate(logPrefab, transform);
                if (!newLogObject.TryGetComponent(out TMP_Text text))
                {
                    DebugHelper.LogMissingComponent(newLogObject, text);
                    return;
                }

                text.text = "";
                logEntries.Add(text);
            }
        }


        public void AddEvent(string eventString)
        {
            TMP_Text thisEntry = logEntries[0];
            thisEntry.text = eventString;
            thisEntry.transform.SetAsLastSibling();

            //Move to bottom of list
            logEntries.Remove(thisEntry);
            logEntries.Add(thisEntry);
        }

        [ContextMenu("Test Log")]
        private void TestAddEvent()
        {
            string text = GetRandomLoremIpsum();
            AddEvent(text);
        }

        private string GetRandomLoremIpsum()
        {
            string[] strings = {
                "Quisque sed suscipit metus, in scelerisque quam.",
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                "Donec faucibus semper condimentum.",
                "Praesent facilisis bibendum justo et interdum.",
                "Aenean in mauris."
            };

            int index = Random.Range(0, strings.Length);
            return strings[index];
        }
    }
}
