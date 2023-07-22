using UnityEngine;
using TMPro;

namespace ProjectVietnam
{
    [RequireComponent(typeof(TMP_Text))]
    public class SquadMemberNamePlate : MonoBehaviour
    {
        private TMP_Text textObject;

        public SquadMember MySquadMember { get; private set; }

        private void Awake()
        {
            if (!TryGetComponent(out textObject))
            {
                DebugHelper.LogMissingComponent(gameObject, textObject);
            }
        }

        public void AssignSoldier(SquadMember newSquadMember)
        {
            MySquadMember = newSquadMember;
            UpdateName();
        }
        public void UnassignSoldier()
        {
            MySquadMember = null;
            UpdateName();
        }

        public void UpdateName()
        {
            if (MySquadMember != null && MySquadMember.soldierBehaviour == null)
            {
                UnassignSoldier();
                return;
            }

            string newName = MySquadMember == null ? "" : MySquadMember.soldierBehaviour.DisplayName;
            textObject.text = newName;
            gameObject.name = "Name Plate - " + newName;
        }

        [ContextMenu("Set as First")]
        private void SetAsFirst()
        {
            transform.SetAsFirstSibling();
        }

        [ContextMenu("Set as Last")]
        private void SetAsLast()
        {
            transform.SetAsLastSibling();
        }

    }
}
