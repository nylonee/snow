using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class TerrainGenerator : MonoBehaviour
    {
        [Tooltip("The total size in world units of the terrain")]
        public Vector3 size = new Vector3(1024.0f, 1024.0f, 1024.0f);

        [Tooltip("Resolution of the heightmap for the terrain, value of 2^n + 1")]
        public int heightmapResolution = 1025;

        [Tooltip("x and y parameters of Mathf.PerlinNoise are multiplied by this value")]
        public float noiseVariation = 0.5f;

        [Tooltip("Value between 0.0f and 1.0f")]
        public float noiseMagnitude = 0.1f;

        [Tooltip("Value between 0.0f and 1.0f")]
        public float slopeTopHeight = 0.7f;

        [Tooltip("Value between 0.0f and 1.0f")]
        public float slopeBottomHeight = 0.3f;

        [Tooltip("The material for the terrain")]
        public Material terrainMaterial;

        [Tooltip("The texture for the terrain")]
        public string[] textures;

        [Tooltip("The number of trees and rocks to add to the terrain")]
        public int numTrees;

        [Tooltip("Slalom manager")]
        public GameObject slalomManager;

        [Tooltip("Player prefab")]
        public GameObject playerPrefab;
        
        [Tooltip("Tree prefabs")]
        public GameObject[] treePrefabs;
        
        private Terrain terrain;
        private TerrainData terrainData;
        private float[,] heightmap;

        public void Start()
        {
            terrain = Terrain.activeTerrain;
            terrainData = terrain.terrainData;

            GenerateHeightmap();
            terrainData.SetHeights(0, 0, heightmap);

            AddTrees();

            AddPlayer();

            slalomManager.GetComponent<SlalomManager>().GenerateSlalom();
        }

        public float[,] GetHeightmap()
        {
            return heightmap;
        }

        private void GenerateHeightmap()
        {
            float height;
            float baseNoise;
            float extraNoise;

            float seed = Random.Range(0.0f, 100.0f);

            heightmap = new float[heightmapResolution, heightmapResolution];

            for(int i = 0; i < heightmapResolution; i++)
                for(int j = 0; j < heightmapResolution; j++)
                {
                    height = Mathf.Lerp(slopeTopHeight, slopeBottomHeight, (float)i / (heightmapResolution - 1));

                    baseNoise = Mathf.PerlinNoise(i * noiseVariation * 0.01f + seed, j * noiseVariation * 0.01f + seed);
                    baseNoise = Mathf.Clamp(baseNoise, 0.0f, 1.0f);
                    baseNoise = baseNoise - 0.5f;
                    baseNoise *= noiseMagnitude;

                    extraNoise = Mathf.PerlinNoise(i * noiseVariation * 2 * 0.01f + seed, j * noiseVariation * 2 * 0.01f + seed);
                    extraNoise = Mathf.Clamp(extraNoise, 0.0f, 1.0f);
                    extraNoise = extraNoise - 0.5f;
                    extraNoise *= noiseMagnitude / 2;

                    SetHeightmapValue(i, j, height + baseNoise + extraNoise);
                }
        }

        private void SetHeightmapValue(int x, int y, float value)
        {
            heightmap[x, y] = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        void AddTrees()
        {
            System.Random random = new System.Random();
            int numTreeTypes = treePrefabs.Length;
            float terrainWidth = terrainData.heightmapWidth;
            GameObject treeObjs = new GameObject("Trees");

            // Add trees
            for(int i = 0; i < numTrees; i++)
            {
                /// Make sure trees dont spawn too close to where player starts
                Vector3 pos = new Vector3(Random.value, 0.0f, Random.Range(100.0f / heightmapResolution, 1.0f)) * terrainWidth;
                int treeTypeIndex = random.Next(0, numTreeTypes);

                // Add trees manually instead of as a terrain tree
                GameObject treeObj = (GameObject)Instantiate
                (
                    treePrefabs[treeTypeIndex],
                    new Vector3(pos.x, terrainData.GetHeight((int)(pos.x), (int)(pos.z)), pos.z),
                    Quaternion.Euler(0.0f, Random.Range(0.0f, 360.0f), 0.0f)
                );

                treeObj.transform.parent = treeObjs.transform;
                treeObj.tag = "Tree";
            }
        }

        void AddPlayer()
        {
            Vector3 startPos = new Vector3(size.x / 2.0f, 0.0f, 50.0f);

            startPos.y = heightmap[(int)startPos.z, (int)startPos.x] * terrainData.heightmapHeight + 5.0f;

            Instantiate(playerPrefab, startPos, playerPrefab.transform.rotation);
        }
    }
}
