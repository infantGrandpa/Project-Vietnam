using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System;

namespace ProjectVietnam
{
    public class SquadInfoBox : MonoBehaviour
    {

        #region Properties and Variables
        [Header("Squad Info")]
        [SerializeField] TMP_Text squadName;
        [SerializeField] TMP_Text squadNickname;

        [Header("Health and Morale")]
        [SerializeField] StatusBarBehaviour healthBar;

        private Squad squad;

        [Header("Squad Members")]
        [SerializeField] Transform squadMemberListParent;
        [SerializeField] GameObject squadMemberNamePrefab;

        private List<SquadMemberNamePlate> squadMemberNamePlates = new();

        #endregion


        #region Start and Destroy
        public void InitializeBox(Squad newSqaud)
        {
            squad = newSqaud;

            UpdateSquadNames();
            PopulateSquadMembers();
        }

        public void PopulateSquadMembers()
        {
            if (squad == null)
            {
                DebugHelper.LogError("Squad is null.");
                return;
            }

            for (int i = 0; i < squad.maxSquadMembers; i++)
            {
                SquadMemberNamePlate newNamePlate = CreateNewNamePlate();
                newNamePlate.AssignSoldier(squad.squadMembers[i]);
            }
        }

        private SquadMemberNamePlate CreateNewNamePlate()
        {
            GameObject namePlateObject = Instantiate(squadMemberNamePrefab, squadMemberListParent);

            if (!namePlateObject.TryGetComponent(out SquadMemberNamePlate namePlate))
            {
                DebugHelper.LogMissingComponent(namePlateObject, namePlate);
                return null;
            }

            namePlate.UnassignSoldier();

            squadMemberNamePlates.Add(namePlate);

            return namePlate;
        }

        #endregion


        #region Update Info
        public void UpdateHealthBar(int currentHealth, int maxHealth)
        {
            healthBar.UpdateBar(currentHealth, maxHealth);
        }

        public void UpdateSquadNames()
        {
            if (squad == null)
            {
                DebugHelper.LogError("Squad is null.");
                return;
            }

            string name = squad.SquadName;
            string nickname = "\"" + squad.SquadNickname + "\"";

            squadName.text = name;
            squadNickname.text = nickname;

            gameObject.name = "Squad Info Box - " + name + " (" + nickname + ")";
        }

        public void UpdateSquadMembers()
        {
            if (squadMemberNamePlates.Count <= 0)
            {
                DebugHelper.LogError("Name plates list is blank.");
                return;
            }

            foreach(SquadMemberNamePlate namePlate in squadMemberNamePlates)
            {
                namePlate.UpdateName();
            }

            SortSquadMembers();
        }

        #endregion


        #region Squad Member Sorting

        [ContextMenu("Sort Squad Members")]
        private void SortSquadMembers()
        {

            squadMemberNamePlates.Sort(CompareByPaygrade);
            squadMemberNamePlates.Reverse();
            UpdateNamePlateOrder();
        }

        [ContextMenu("Shuffle Squad Members")]
        private void ShuffleSquadMembers()
        {
            Common.ShuffleList(squadMemberNamePlates);
            UpdateNamePlateOrder();
        }

        private void UpdateNamePlateOrder()
        {
            for (int i = 0; i < squadMemberNamePlates.Count; i++)
            {
                squadMemberNamePlates[i].transform.SetAsLastSibling();
            }
        }

        private int CompareByPaygrade(SquadMemberNamePlate plate1, SquadMemberNamePlate plate2)
        {
            int payGrade1 = 0;
            if (plate1 != null && plate1.MySquadMember != null && plate1.MySquadMember.soldierBehaviour != null)
            {
                payGrade1 = plate1.MySquadMember.soldierBehaviour == null ? 0 : plate1.MySquadMember.soldierBehaviour.SoldierRank.payGrade;
            }

            int payGrade2 = 0;
            if (plate2 != null && plate2.MySquadMember != null && plate2.MySquadMember.soldierBehaviour != null)
            {
                payGrade2 = plate2.MySquadMember.soldierBehaviour == null ? 0 : plate2.MySquadMember.soldierBehaviour.SoldierRank.payGrade;
            }

            if (payGrade1 != payGrade2)
            {
                return payGrade1.CompareTo(payGrade2);
            }

            return CompareByExperience(plate1, plate2);


        }

        private int CompareByExperience(SquadMemberNamePlate plate1, SquadMemberNamePlate plate2)
        {
            int experience1 = 0;
            if (plate1 != null && plate1.MySquadMember != null && plate1.MySquadMember.experienceTracker != null)
            {
                experience1 = plate1.MySquadMember.experienceTracker.CurrentExperience;
            }

            int experience2 = 0;
            if (plate2 != null && plate2.MySquadMember != null && plate2.MySquadMember.experienceTracker != null)
            {
                experience2 = plate2.MySquadMember.experienceTracker.CurrentExperience;
            }

            if (experience1 != experience2)
            {
                return experience1.CompareTo(experience2);
            }

            return CompareByName(plate1, plate2);
        }

        private int CompareByName(SquadMemberNamePlate plate1, SquadMemberNamePlate plate2)
        {
            string name1 = string.Empty;
            if (plate1 != null && plate1.MySquadMember != null && plate1.MySquadMember.soldierBehaviour != null)
            {
                name1 = plate1.MySquadMember.soldierBehaviour.DisplayName;
            }

            string name2 = string.Empty;
            if (plate2 != null && plate2.MySquadMember != null && plate2.MySquadMember.soldierBehaviour != null)
            {
                name2 = plate2.MySquadMember.soldierBehaviour.DisplayName;
            }

            return name1.CompareTo(name2);
        }

        [ContextMenu("Print Squad Names")]
        private void PrintList()
        {
            List<string> namesToPrint = new();

            foreach (SquadMemberNamePlate namePlate in squadMemberNamePlates)
            {
                if (namePlate.MySquadMember == null)
                {
                    namesToPrint.Add("---");
                    continue;
                }
                namesToPrint.Add(namePlate.MySquadMember.soldierBehaviour.DisplayName);
            }

            DebugHelper.Log(string.Join(Environment.NewLine, namesToPrint));
        }

        #endregion
    }
}
