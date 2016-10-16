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
        public GameObject goalPrefab;

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

            while (curZDist < terrainWidth)
            {
                Vector3 pos = new Vector3(terrainWidth / 2.0f + Random.Range(minXDist, maxXDist) * leftOrRight, 0.0f, curZDist);
                pos.y = terrainData.GetHeight((int)pos.x, (int)pos.z);
                GameObject checkpointObj = (GameObject)Instantiate(checkpointPrefab, pos + checkpointPrefab.transform.position, checkpointPrefab.transform.rotation);
                checkpointObj.transform.parent = checkpointObjs.transform;
                curZDist += zInterval;
                leftOrRight = flip(leftOrRight);
            }

            // Replace last checkpoint with goal
            GameObject lastCheckpoint = checkpointObjs.transform.GetChild(checkpointObjs.transform.childCount - 1).gameObject;
            Vector3 goalPos = new Vector3(terrainWidth / 2, 0.0f, lastCheckpoint.transform.position.z);
            goalPos.y = terrainData.GetHeight((int)goalPos.x, (int)goalPos.z);
            Destroy(lastCheckpoint);
            Instantiate(goalPrefab, goalPos + goalPrefab.transform.position, goalPrefab.transform.rotation);
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