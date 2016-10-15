using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class WeatherManager : MonoBehaviour
    {
        public GameObject snowEffectPrefab;

        public Vector3 cameraFollowDist = new Vector3(0, 25.0f, 0.0f);

        public float chanceOfSnow = 0.5f;

        private GameObject weatherObj;
        private Transform cameraTransform;

        void Start()
        {
            cameraTransform = Camera.main.transform;

            if (Random.value < chanceOfSnow)
                weatherObj = (GameObject)Instantiate(snowEffectPrefab);
        }

        void Update()
        {
            if (weatherObj == null)
                return;

            weatherObj.transform.position = cameraTransform.position + cameraFollowDist;
        }
    }
}