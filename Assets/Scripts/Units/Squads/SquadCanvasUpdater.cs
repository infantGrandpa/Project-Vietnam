using UnityEngine;

namespace ProjectVietnam
{
    [RequireComponent(typeof(Squad))]
    public class SquadCanvasUpdater : MonoBehaviour
    {
        private Squad squad;
        private SquadHealth health;

        public SquadInfoBox squadInfoBox { get; private set; }

        private void Awake()
        {
            squad = GetComponent<Squad>();
            health = GetComponent<SquadHealth>();
        }

        private void OnEnable()
        {
            health.HealthCalculated += UpdateSquadHealth;
            squad.SquadMembersUpdated += UpdateSquadMembers;
        }

        private void OnDisable()
        {
            health.HealthCalculated -= UpdateSquadHealth;
            squad.SquadMembersUpdated -= UpdateSquadMembers;
        }
        private void Start()
        {
            squadInfoBox = NonDiageticCanvasBehaviour.Instance.CreateNewSquadBox(squad);
        }

        private void UpdateSquadHealth()
        {
            if (health == null)
            {
                DebugHelper.LogMissingComponent(gameObject, health);
                return;
            }

            if (squadInfoBox == null)
            {
                DebugHelper.LogMissingComponent(gameObject, squadInfoBox);
                return;
            }

            squadInfoBox.UpdateHealthBar(health.CurrentHealth, health.MaxHealth);

        }

        public void UpdateSquadMembers()
        {
            if (squadInfoBox == null)
            {
                return;
            }

            squadInfoBox.UpdateSquadNames();
            squadInfoBox.UpdateSquadMembers();
        }
    }
}
