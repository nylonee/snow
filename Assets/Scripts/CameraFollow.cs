using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class CameraFollow : MonoBehaviour
    {

        public GameObject player;

        public float height = 10; // The height of the camera above the snowboarder
        public float distance = 10; // The distance of the camera behind the snowboarder

        private Terrain terrain;
        private TerrainData terrainData;

        void Start()
        {
        }

        void LateUpdate()
        {
            if (terrain == null || terrainData == null)
            {
                terrain = Terrain.activeTerrain;
                terrainData = terrain.terrainData;
            }
            
            float y = (float)player.transform.position.y / (float)terrainData.alphamapHeight;
            float x = (float)player.transform.position.x / (float)terrainData.alphamapWidth;

            Vector3 normal = terrainData.GetInterpolatedNormal(y, x);
            Vector3 perp = Vector3.Cross(normal, Vector3.right);

            transform.position = player.transform.position + normal*height + perp*distance;
            transform.LookAt(player.transform);
        }
    }
}
