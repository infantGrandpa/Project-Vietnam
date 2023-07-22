using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

namespace ProjectVietnam
{
    [RequireComponent(typeof(PingManager))]
    public class SelectionController : MonoBehaviour
    {
        public static SelectionController Instance { get; private set; }

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
            GetComponents();
        }

        private Vector3 startPosition;
        private List<SelectableObject> selectedObjectList = new List<SelectableObject>();

        private PingManager pingManager;

        private void GetComponents()
        {
            pingManager = GetComponent<PingManager>();
        }

        private void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                LeftMouseDown();
            }

            if (Input.GetMouseButton(0))
            {
                LeftMousePressed();
            }

            if (Input.GetMouseButtonUp(0))
            {
                LeftMouseUp();
            }

            if (Input.GetMouseButtonDown(1))
            {
                RightMouseDown();
            }
        }

        private void LeftMouseDown()
        {
            startPosition = UtilsClass.GetMouseWorldPosition();
            SelectionArea.Instance.ShowSelectionArea();
        }

        private void LeftMousePressed()
        {
            SelectionArea.Instance.ResizeSelectionArea(startPosition, UtilsClass.GetMouseWorldPosition());
        }

        private void LeftMouseUp()
        {
            Collider2D[] collidersInSelection = Physics2D.OverlapAreaAll(startPosition, UtilsClass.GetMouseWorldPosition());

            DeselectAll();

            foreach (Collider2D thisCollider in collidersInSelection)
            {
                if (!thisCollider.TryGetComponent(out SelectableObject selectableObject))
                {
                    continue;
                }
                SelectObject(selectableObject);
            }

            SelectionArea.Instance.HideSelectionArea();
        }

        private void RightMouseDown()
        {
            Vector3 targetPosition = UtilsClass.GetMouseWorldPosition();

            List<Vector3> targetPositionList = GetPositionListAround(targetPosition, 1f, selectedObjectList.Count);

            if (selectedObjectList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < selectedObjectList.Count; i++)
            {
                selectedObjectList[i].MoveToPosition(targetPositionList[i]);
            }

            pingManager.ShowCommandPingAtPosition(targetPosition, CommandType.Move);
        }

        private List<Vector3> GetPositionListAround(Vector3 startPosition, float distance, int positionCount)
        {
            List<Vector3> positionList = new();

            if (positionCount == 1)
            {
                distance = 0;
            }

            for (int i = 0; i < positionCount; i++)
            {
                float angle = i * (360f / positionCount);
                Vector3 dir = ApplyRotationToVector(new Vector3(1, 0), angle);
                Vector3 position = startPosition + dir * distance;
                positionList.Add(position);
            }


            return positionList;
        }

        private Vector3 ApplyRotationToVector(Vector3 vector, float angle)
        {
            return Quaternion.Euler(0, 0, angle) * vector;
        }

        private void DeselectAll()
        {
            foreach (SelectableObject thisObject in selectedObjectList)
            {
                DeselectObject(thisObject, false);
            }

            selectedObjectList.Clear();
        }

        private void DeselectObject(SelectableObject selectableObject, bool removeFromList = true)
        {
            selectableObject.DeselectObject();

            if (removeFromList)
            {
                selectedObjectList.Remove(selectableObject);
            }

        }

        private void SelectObject(SelectableObject selectableObject)
        {
            selectableObject.SelectObject();
            selectedObjectList.Add(selectableObject);
        }

        
    }
}