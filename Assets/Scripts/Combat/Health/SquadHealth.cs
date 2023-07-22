using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace ProjectVietnam
{
    [RequireComponent(typeof(Squad))]
    public class SquadHealth : MonoBehaviour, IDamageable
    {
        [ShowInInspector, ReadOnly]
        public int MaxHealth { get; private set; }

        [SerializeField] UnityEvent onTakeDamageEvent;

        [ShowInInspector, ReadOnly]
        public int CurrentHealth { get; private set; }

        private Squad squad;

        private List<SquadMember> squadMembers;

        public delegate void HealthCalculatedEventHandler();
        public event HealthCalculatedEventHandler HealthCalculated;

        private void Awake()
        {
            squad = GetComponent<Squad>();
        }

        private void Start()
        {
            CalculateMaxHealth();
            CalculateHealth();
        }

        public void Damage(int damageTaken)
        {
            DistributeDamage(damageTaken);
            onTakeDamageEvent?.Invoke();
        }

        private void DistributeDamage(int totalDamage)
        {
            int damageRemaining = totalDamage;

            while(damageRemaining > 0)
            {
                GetSquadMembers();

                SquadMember squadMemberToDamage = GetRandomSquadMember();
                if (squadMemberToDamage == null)
                {
                    break;
                }

                HealthSystem thisHealthSystem = squadMemberToDamage.healthSystem;

                if (thisHealthSystem == null)
                {
                    DebugHelper.LogMissingComponent(thisHealthSystem.gameObject, thisHealthSystem);
                    break;
                }

                int thisDamage = Random.Range(0, damageRemaining + 1);
                thisHealthSystem.Damage(thisDamage);
                damageRemaining -= thisDamage;
            }

            CalculateHealth();
        }

        private void GetSquadMembers()
        {
            squadMembers = new List<SquadMember>(squad.squadMembers);
        }

        private SquadMember GetRandomSquadMember()
        {
            if (squadMembers.Count == 0)
            {
                return null;
            }

            int randomIndex = Random.Range(0, squadMembers.Count);
            return squadMembers[randomIndex];
        }

        private void CalculateMaxHealth()
        {
            GetSquadMembers();

            int newMaxHealth = 0;

            foreach (SquadMember thisSquadMember in squadMembers)
            {
                HealthSystem thisHealthSystem = thisSquadMember.healthSystem;

                if (thisHealthSystem == null)
                {
                    DebugHelper.LogMissingComponent(thisHealthSystem.gameObject, thisHealthSystem);
                    continue;
                }

                newMaxHealth += thisHealthSystem.maxHealth;
            }

            //If we have less than max soldiers, average the rest.
            int maxSquadMembers = squad.maxSquadMembers;
            if (squadMembers.Count > 0 && squadMembers.Count < maxSquadMembers)
            {
                int averageHealth = Mathf.RoundToInt(newMaxHealth / squadMembers.Count);
                int squadMembersRemaining = maxSquadMembers - squadMembers.Count;
                newMaxHealth += averageHealth * squadMembersRemaining;
            }

            MaxHealth = newMaxHealth;
        }

        private void CalculateHealth()
        {
            GetSquadMembers();

            int newCurrentHealth = 0;

            foreach(SquadMember thisSquadMember in squadMembers)
            {
                HealthSystem thisHealthSystem = thisSquadMember.healthSystem;

                if (thisHealthSystem == null)
                {
                    DebugHelper.LogMissingComponent(thisHealthSystem.gameObject, thisHealthSystem);
                    continue;
                }

                newCurrentHealth += thisHealthSystem.CurrentHealth;
            }

            CurrentHealth = newCurrentHealth;

            HealthCalculated?.Invoke();
            CheckSquadStatus();
        }

        private void CheckSquadStatus()
        {
            if (squadMembers.Count <= 0)
            {
                Destroy(gameObject);
            }
        }

        [ContextMenu("Test Damage")]
        private void TestDamage()
        {
            int damageAmount = Random.Range(50, 250);
            Damage(damageAmount);
            DebugHelper.Log("Damaged " + squad.SquadName + " for " + damageAmount + " damage.");
        }
    }
}
