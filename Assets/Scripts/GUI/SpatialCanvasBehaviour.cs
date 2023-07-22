using UnityEngine;

namespace ProjectVietnam
{
    public class SpatialCanvasBehaviour : CanvasBehaviour
    {
        public static SpatialCanvasBehaviour Instance
        {
            get
            {
                if (instance == null)
                    instance = FindObjectOfType(typeof(SpatialCanvasBehaviour)) as SpatialCanvasBehaviour;

                return instance;
            }
            set
            {
                instance = value;
            }
        }
        private static SpatialCanvasBehaviour instance;


    }
}
