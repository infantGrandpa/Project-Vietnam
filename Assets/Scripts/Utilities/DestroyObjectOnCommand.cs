using UnityEngine;

namespace ProjectVietnam
{
    public class DestroyObjectOnCommand : MonoBehaviour
    {
        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }
}
