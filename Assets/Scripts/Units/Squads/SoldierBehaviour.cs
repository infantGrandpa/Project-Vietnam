using UnityEngine;

using Sirenix.OdinInspector;
using System;

namespace ProjectVietnam
{
    public class SoldierBehaviour : MonoBehaviour
    {
        private string firstName;
        private string lastName;

        [ShowInInspector, ReadOnly]
        public Rank SoldierRank { get; private set; }
        [ShowInInspector, ReadOnly]
        public string DisplayName { get; private set; }

        public event Action<SoldierBehaviour> OnSoldierDeath;

        private void Awake()
        {
            GetName();
            UpdateDisplayName();
        }

        private void GetName()
        {
            firstName = NameDataLoader.Instance.GetRandomFirstName();
            lastName = NameDataLoader.Instance.GetRandomLastName();
        }

        private void UpdateDisplayName()
        {
            if (SoldierRank == null)
            {
                DisplayName = firstName + " " + lastName;
            } 
            else
            {
                DisplayName = SoldierRank.rankAbbreviation + ". " + firstName + " " + lastName;
            }

            RenameObject();
        }

        private void RenameObject()
        {
            gameObject.name = DisplayName;
        }

        public void SetRank(Rank newRank)
        {
            SoldierRank = newRank;
            UpdateDisplayName();
        }

        public void Die()
        {
            OnSoldierDeath?.Invoke(this);
        }

        
    }
}
