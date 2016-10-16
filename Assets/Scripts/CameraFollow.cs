using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class CameraFollow : MonoBehaviour
    {
        [Tooltip("The height of the camera above the snowboarder")]
        public float height = 10;

        [Tooltip("The distance of the camera behind the snowboarder")]
        public float distance = 10;

        [Tooltip("In degrees")]
        public float maxCameraPan = 90.0f;

        public Vector3 orientation = Vector3.right; // 1 = normal, 2 = looking back, 3 = looking right, 4 = looking left

        private Terrain terrain;
        private TerrainData terrainData;
        private Transform cameraTransform;

        private GameObject player;

        void Start()
        {
            cameraTransform = Camera.main.transform;
        }

        void LateUpdate()
        {
            if (terrain == null || terrainData == null || player == null)
            {
                terrain = Terrain.activeTerrain;
                terrainData = terrain.terrainData;
                player = GameObject.FindGameObjectWithTag("Player");
            }

            CameraPan();

            float y = (float)player.transform.position.y / (float)terrainData.alphamapHeight;
            float x = (float)player.transform.position.x / (float)terrainData.alphamapWidth;

            Vector3 normal = terrainData.GetInterpolatedNormal(y, x);
            Vector3 perp = Vector3.Cross(normal, orientation);
            
            cameraTransform.position = player.transform.position + normal*height + perp*distance;

            // Ensure that the position of the camera is always above a certain height
            cameraTransform.position += new Vector3{ x = 0f, y = 7f, z = 0f};
            cameraTransform.LookAt(transform);
            cameraTransform.rotation *= Quaternion.Euler(-10.0f, 0.0f, 0.0f);
        }

        void CameraPan()
        {
            if (Input.GetMouseButton(0))
            {
                float panAmount = 2 * (Input.mousePosition.x / Screen.width) - 1.0f; // between -1 and 1
                orientation = Quaternion.Euler(0.0f, 90.0f * panAmount, 0.0f) * Vector3.right;
            }
            else
                orientation = Vector3.right;
        }
    }
}
