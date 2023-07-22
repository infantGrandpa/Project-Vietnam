using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace ProjectVietnam
{
    public class Squad : MonoBehaviour
    {
        [Header("Squad Identifiers")]
        private int squadNum;
        public string SquadName { get; private set; }
        public string SquadNickname { get; private set; }
        [SerializeField] TMP_Text squadText;

        [Header("Squad Members")]
        public int maxSquadMembers;
        [SerializeField] GameObject soldierPrefab;

        public List<SquadMember> squadMembers = new();

        public List<Rank> ranksInSquad = new();

        public delegate void SquadMembersUpdatedEventHandler();
        public event SquadMembersUpdatedEventHandler SquadMembersUpdated;

        private void Awake()
        {
            PopulateSquad();
        }
        private void Start()
        {
            GetSquadIdentifiers();
        }

        private void OnEnable()
        {
            SquadManager.Instance.activeSquads.Add(this);
        }

        private void OnDisable()
        {
            if (SquadManager.Instance == null)
            {
                return;
            }

            SquadManager.Instance.activeSquads.Remove(this);
        }

        [ContextMenu("Get New Squad Names")]
        private void GetSquadIdentifiers()
        {
            GetSquadNum();
            GetSquadName();
            GetSquadNickname();
            SquadMembersUpdated?.Invoke();
        }

        private void GetSquadNum()
        {
            squadNum = SquadManager.Instance.GetSquadNum();

            if (squadText == null)
            {
                return;
            }

            squadText.text = squadNum.ToString();
        }

        private void GetSquadName()
        {
            SquadName = SquadManager.Instance.GetSquadName(squadNum);

            if (SquadName == "")
            {
                SquadName = gameObject.name;
                return;
            }

            gameObject.name = SquadName;
        }

        private void GetSquadNickname()
        {
            SquadNickname = SquadManager.Instance.GetSquadNickName();
        }


        private void PopulateSquad()
        {
            while (squadMembers.Count < maxSquadMembers)
            {
                GameObject newSoldier = Instantiate(soldierPrefab, transform);
                if (!AddSquadMember(newSoldier))
                {
                    break;
                }
            }

            SetRanks();
        }

        private bool AddSquadMember(GameObject newSoldier)
        {
            SquadMember newSquadMember = new();

            if (!newSoldier.TryGetComponent(out SoldierBehaviour soldierBehaviour))
            {
                DebugHelper.LogMissingComponent(newSoldier, soldierBehaviour);
                return false;
            }

            newSquadMember.soldierBehaviour = soldierBehaviour;
            soldierBehaviour.OnSoldierDeath += OnSquadMemberDeath;

            newSquadMember.experienceTracker = newSoldier.GetComponent<ExperienceTracker>();
            newSquadMember.healthSystem = newSoldier.GetComponent<HealthSystem>();

            squadMembers.Add(newSquadMember);
            SquadMembersUpdated?.Invoke();

            return true;
        }

        private void SetRanks()
        {
            SortRanksByPayGrade();
            AssignMissingRanks();

        }

        public void SortRanksByPayGrade()
        {
            ranksInSquad = ranksInSquad.OrderBy(rank => rank.payGrade).ToList();
        }


        public void AssignMissingRanks()
        {
            // Get the count of soldiers per rank
            Dictionary<int, int> rankCount = new Dictionary<int, int>();
            foreach (Rank rank in ranksInSquad)
            {
                //Reset all to 0
                rankCount[rank.payGrade] = 0;
            }

            Rank lowestRank = ranksInSquad.OrderBy(rank => rank.payGrade).FirstOrDefault();

            // Count the number of soldiers per rank
            foreach (SquadMember squadMember in squadMembers)
            {
                SoldierBehaviour soldier = squadMember.soldierBehaviour;

                if (soldier.SoldierRank == null)
                {
                    soldier.SetRank(lowestRank);
                }

                rankCount[soldier.SoldierRank.payGrade]++;
            }


            foreach (KeyValuePair<int, int> kvp in rankCount)
            {
                int payGrade = kvp.Key;
                int count = kvp.Value;
            }

            // Get the ranks with available slots
            List<Rank> availableRanks = ranksInSquad
                .Where(rank => rankCount[rank.payGrade] < rank.minPerSquad)
                .OrderByDescending(rank => rank.payGrade)
                .ToList();

            foreach (Rank thisRank in availableRanks)
            {
                SquadMember squadMemberToPromote = GetHighestExperience(thisRank.payGrade);
                squadMemberToPromote.soldierBehaviour.SetRank(thisRank);
            }
        }

        private SquadMember GetHighestExperience(int thisPayGrade)
        {
            SquadMember squadMemberWithHighestExperience = null;
            int highestExperience = int.MinValue;

            foreach (SquadMember squadMember in squadMembers)
            {
                ExperienceTracker tracker = squadMember.experienceTracker;

                if (tracker == null || tracker.CurrentExperience <= highestExperience)
                {
                    continue;
                }

                if (squadMember.soldierBehaviour.SoldierRank.payGrade >= thisPayGrade)
                {
                    continue;
                }

                highestExperience = tracker.CurrentExperience;
                squadMemberWithHighestExperience = squadMember;
            }

            return squadMemberWithHighestExperience;
        }

        private void OnSquadMemberDeath(SoldierBehaviour soldier)
        {
            SquadMember squadMember = GetSquadMemberFromSoldier(soldier);
            squadMembers.Remove(squadMember);

            string logText = soldier.DisplayName + " of " + SquadName + " has died.";
            EventLogBehaviour.Instance.AddEvent(logText);

            SquadMembersUpdated?.Invoke();
        }

        private SquadMember GetSquadMemberFromSoldier(SoldierBehaviour soldier)
        {
            foreach (SquadMember squadMember in squadMembers)
            {
                if (squadMember.soldierBehaviour != soldier)
                {
                    continue;
                }

                return squadMember;
            }

            return null;
        }
    }
}
