using UnityEngine;
using DG.Tweening;

namespace ProjectVietnam
{
    public class SoldierDropdownBehaviour : MonoBehaviour
    {
        [Header("Hiding Soldier List")]
        [SerializeField] RectTransform squadMemberListTransform;

        [SerializeField, Tooltip("The height of the info box when the soldier list is hidden.")]
        float shortInfoBoxHeight;

        private float expandedInfoBoxHeight;
        private float expandedSoldierListHeight;

        private RectTransform boxRectTransform;

        private Tween infoBoxTween;
        private Tween soldierListTween;

        [Header("Tween Preferences")]
        [SerializeField] float tweenDuration = 0.25f;
        [SerializeField] Ease tweenEaseType = Ease.OutCirc;

        private bool isHidden;

        private void Awake()
        {
            isHidden = false;
            boxRectTransform = GetComponent<RectTransform>();

            expandedInfoBoxHeight = boxRectTransform.sizeDelta.y;
            expandedSoldierListHeight = squadMemberListTransform.sizeDelta.y;
        }

        private void Start() {
            HideSoldierList();
        }

        private void OnEnable()
        {
            NonDiageticCanvasBehaviour.Instance.soldierDropdowns.Add(this);
        }

        private void OnDisable()
        {
            if (NonDiageticCanvasBehaviour.Instance == null)
            {
                return;
            }

            NonDiageticCanvasBehaviour.Instance.soldierDropdowns.Remove(this);
        }

        [ContextMenu("Hide Soldier List")]
        public void HideSoldierList()
        {
            if (isHidden)
            {
                return;
            }

            //squadMemberListTransform.SetActive(false);
            Vector2 targetSize = new Vector2(boxRectTransform.sizeDelta.x, shortInfoBoxHeight);
            CreateNewTween(targetSize, 0);
            infoBoxTween.OnComplete(HideListComplete);            
        }

        [ContextMenu("Show Soldier List")]
        public void ShowSoldierList()
        {
            if (!isHidden)
            {
                return;
            }

            //squadMemberListTransform.SetActive(true);
            Vector2 targetSize = new Vector2(boxRectTransform.sizeDelta.x, expandedInfoBoxHeight);
            CreateNewTween(targetSize, 1);
            infoBoxTween.OnComplete(ShowListComplete);
        }

        public void OnDropDownButtonClick()
        {
            NonDiageticCanvasBehaviour.Instance.HideAllSoldiers();
            ShowSoldierList();
        }

        private void HideListComplete()
        {
            isHidden = true;
        }

        private void ShowListComplete()
        {
            isHidden = false;
        }

        private void CreateNewTween(Vector2 targetInfoBoxSize, float targetListScale)
        {
            if (infoBoxTween != null)
            {
                infoBoxTween.Complete(true);
            }

            if (soldierListTween != null) {
                soldierListTween.Complete(false);
            }

            infoBoxTween = boxRectTransform.DOSizeDelta(targetInfoBoxSize, tweenDuration);
            infoBoxTween.SetEase(tweenEaseType);

            soldierListTween = squadMemberListTransform.DOScaleY(targetListScale, tweenDuration);
            soldierListTween.SetEase(tweenEaseType);


        }
    }
}
