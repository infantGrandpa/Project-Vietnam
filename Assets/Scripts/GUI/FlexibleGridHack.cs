using UnityEngine;
using System.Collections;

namespace ProjectVietnam
{
    [RequireComponent(typeof(FlexibleGridLayout))]
    public class FlexibleGridHack : MonoBehaviour
    {
        private FlexibleGridLayout flexibleGridLayout;

        
        private void Awake() {
            flexibleGridLayout = GetComponent<FlexibleGridLayout>();
        }

        private void Start() {
            StartHack();
        }

        [ContextMenu("Fix")]
        private void StartHack() {
            StartCoroutine(HackyAssFix());
        }

        //Fixes an issue where the layout will stack everything incorrectly until its refreshed.
        private IEnumerator HackyAssFix() {
            //Flip fitX
            flexibleGridLayout.fitX = !flexibleGridLayout.fitX; 

            yield return new WaitForEndOfFrame();

            //Flip it back
            flexibleGridLayout.fitX = !flexibleGridLayout.fitX; 
        }

    }
}
