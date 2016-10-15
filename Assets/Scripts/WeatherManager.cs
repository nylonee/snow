using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class WeatherManager : MonoBehaviour
    {
        public GameObject snowEffectPrefab;

        public Vector3 cameraFollowDist = new Vector3(0, 25.0f, 0.0f);

        private GameObject weatherObj;
        private Transform cameraTransform;

        void Start()
        {
            cameraTransform = Camera.main.transform;
            weatherObj = (GameObject)Instantiate(snowEffectPrefab);
        }

        void Update()
        {
            weatherObj.transform.position = cameraTransform.position + cameraFollowDist;
        }
    }
}