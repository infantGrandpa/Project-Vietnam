using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace ProjectVietnam
{
    public class RelationshipManager : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        public int CurrentAffinity { get; private set; }
        public int maxAffinity = 100;

        [SerializeField] List<AffinityMilestone> affinityMilestones = new();

        private StatusBarController barController;

        private void Start()
        {
            barController = GetComponent<StatusBarController>();
            barController.SetBarMax(maxAffinity);
            barController.UpdateBarValue(CurrentAffinity);
        }

        public void AdjustAffinity(int adjustBy)
        {
            CurrentAffinity += adjustBy;
            CurrentAffinity = Mathf.Clamp(CurrentAffinity, 0, maxAffinity);
            CheckMilestoneProgress();
            barController.UpdateBarValue(CurrentAffinity);
        }

        public void CheckMilestoneProgress()
        {
            foreach (AffinityMilestone thisMilestone in affinityMilestones)
            {
                
            }
        }
    }
}
