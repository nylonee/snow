using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class SlalomManager : MonoBehaviour
    {
        public float minXDist = 5.0f;
        public float maxXDist = 10.0f;
        public float zInterval = 75.0f;
        public float startingZDist = 100.0f;
        public bool isEnabled = true;

        public GameObject checkpointPrefab;

        public void GenerateSlalom()
        {
            if (!isEnabled)
            {
                Debug.Log("SlalomManager not enabled");
                return;
            }

            int leftOrRight = new System.Random().Next(2) - 1; // -1 or 1
            float curZDist = startingZDist;
            TerrainData terrainData = Terrain.activeTerrain.terrainData;
            float terrainWidth = terrainData.heightmapWidth;
            GameObject checkpointObjs = new GameObject("Checkpoints");
            
            while(curZDist < terrainWidth)
            {
                Vector3 pos = new Vector3(terrainWidth / 2.0f + Random.Range(minXDist, maxXDist) * leftOrRight, 0.0f, curZDist + zInterval);
                pos.y = terrainData.GetHeight((int)pos.x, (int)pos.z);
                GameObject checkpointObj = (GameObject)Instantiate(checkpointPrefab, pos + checkpointPrefab.transform.position, checkpointPrefab.transform.rotation);
                checkpointObj.transform.parent = checkpointObjs.transform;
                curZDist = pos.z;
                leftOrRight = flip(leftOrRight);
            }
        }

        private int flip(int leftOrRight)
        {
            if (leftOrRight == -1)
                return 1;
            else
                return -1;
        }
    }
}