using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectVietnam
{
    public class SelectionArea : MonoBehaviour
    {
        public static SelectionArea Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }


        private void Start()
        {
            HideSelectionArea();
        }

        public void ShowSelectionArea()
        {
            gameObject.SetActive(true);
        }

        public void HideSelectionArea()
        {
            gameObject.SetActive(false);
        }

        public void ResizeSelectionArea(Vector3 positionA, Vector3 positionB)
        {
            float lowerLeftX = Mathf.Min(positionA.x, positionB.x);
            float lowerLeftY = Mathf.Min(positionA.y, positionB.y);
            Vector3 lowerLeftPosition = new Vector3(lowerLeftX, lowerLeftY);

            float upperRightX = Mathf.Max(positionA.x, positionB.x);
            float upperRightY = Mathf.Max(positionA.y, positionB.y);
            Vector3 upperRightPosition = new Vector3(upperRightX, upperRightY);

            transform.position = lowerLeftPosition;
            transform.localScale = upperRightPosition - lowerLeftPosition;
        }
    }
}