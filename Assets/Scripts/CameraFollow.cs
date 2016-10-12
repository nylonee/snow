using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class CameraFollow : MonoBehaviour
    {

        public GameObject player;

        public float offset;

        private Terrain terrain;
        private TerrainData terrainData;

        void Start()
        {
        }

        void LateUpdate()
        {
            if (terrain == null || terrainData == null)
            {
                terrain = (Terrain)(GameObject.FindGameObjectWithTag("Terrain").GetComponent<Terrain>());
                terrainData = terrain.terrainData;
            }


            float y = (float)player.transform.position.y / (float)terrainData.alphamapHeight;
            float x = (float)player.transform.position.x / (float)terrainData.alphamapWidth;

            Vector3 normal = terrainData.GetInterpolatedNormal(y, x);
            float steepness = terrainData.GetSteepness(y, x);

            transform.position = player.transform.position + (normal * offset * steepness);

            transform.LookAt(player.transform);
        }
    }
}
