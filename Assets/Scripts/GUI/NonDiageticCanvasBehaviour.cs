using UnityEngine;
using System.Collections.Generic;

namespace ProjectVietnam
{
    public class NonDiageticCanvasBehaviour : CanvasBehaviour
    {
        public static NonDiageticCanvasBehaviour Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(NonDiageticCanvasBehaviour)) as NonDiageticCanvasBehaviour;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static NonDiageticCanvasBehaviour instance;

        [SerializeField] Transform squadList;
        [SerializeField] GameObject squadInfoBoxPrefab;

        [HideInInspector] public List<SoldierDropdownBehaviour> soldierDropdowns = new();

        public SquadInfoBox CreateNewSquadBox(Squad squad)
        {
            GameObject newBox = Instantiate(squadInfoBoxPrefab, squadList);

            if (!newBox.TryGetComponent(out SquadInfoBox squadInfoBox))
            {
                DebugHelper.LogMissingComponent(newBox, squadInfoBox);
                return null;
            }

            squadInfoBox.InitializeBox(squad);

            return squadInfoBox;
        }

        [ContextMenu("Hide All Soldiers")]
        public void HideAllSoldiers() {
            foreach(SoldierDropdownBehaviour thisDropdown in soldierDropdowns) {
                if (thisDropdown == null) {
                    continue;
                }

                thisDropdown.HideSoldierList();
            }
        }
    }
}
