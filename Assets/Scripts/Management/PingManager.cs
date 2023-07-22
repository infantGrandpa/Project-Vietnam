using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace ProjectVietnam
{
    public class PingManager : MonoBehaviour
    {
        [System.Serializable]
        private class Ping
        {
            [EnumToggleButtons] public CommandType commandType;
            [SceneObjectsOnly] public GameObject pingObject;
        }

        [SerializeField] List<Ping> pings;

        public void ShowCommandPingAtPosition(Vector2 worldPosition, CommandType commandType)
        {
            GameObject pingObject = GetPingFromCommandType(commandType);
            ResetTweens(pingObject);

            pingObject.transform.position = SpatialCanvasBehaviour.Instance.ConvertWorldToCanvasPosition(worldPosition);
            pingObject.SetActive(true);
        }

        private GameObject GetPingFromCommandType(CommandType commandType)
        {
            GameObject ping = null;

            foreach(Ping thisPing in pings)
            {
                if (thisPing.commandType != commandType)
                {
                    continue;
                }

                ping = thisPing.pingObject;
            }

            return ping;
        }

        private void ResetTweens(GameObject pingObject)
        {
            DOTween.Restart(pingObject);
        }
    }
}
