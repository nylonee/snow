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

        [Tooltip("The shader used for rendering the splatmap")]
        public Shader shader;

        [Tooltip("The number of trees and rocks to add to the terrain")]
        public int numTreesAndRocks;

        [Tooltip("Percentage of trees out of total trees+rocks")]
        public float treePercentage;

        private GameObject terrainObj;
        private Terrain terrain;
        private TerrainData terrainData;
        private float[,] heightmap;

        public void Start()
        {
            terrain = Terrain.activeTerrain;
            terrainObj = terrain.gameObject;
            terrainData = terrain.terrainData;

            GenerateHeightmap();
            terrainData.SetHeights(0, 0, heightmap);

            // Generate the splat, adding textures to the terrain
            GenerateTerrainSplat();

            AddTreesAndRocks();
        }

        public float[,] GetHeightmap()
        {
            return heightmap;
        }

        private void GenerateHeightmap()
        {
            float height;
            float noise;

            float seed = Random.Range(0.0f, 100.0f);

            heightmap = new float[heightmapResolution, heightmapResolution];

            for(int i = 0; i < heightmapResolution; i++)
                for(int j = 0; j < heightmapResolution; j++)
                {
                    height = Mathf.Lerp(slopeTopHeight, slopeBottomHeight, (float)i / (heightmapResolution - 1));

                    noise = Mathf.PerlinNoise(i * noiseVariation * 0.01f + seed, j * noiseVariation * 0.01f + seed);
                    noise = Mathf.Clamp(noise, 0.0f, 1.0f);
                    noise = noise - 0.5f;
                    noise *= noiseMagnitude;
                    
                    SetHeightmapValue(i, j, height + noise);
                }
        }

        private void SetHeightmapValue(int x, int y, float value)
        {
            heightmap[x, y] = Mathf.Clamp(value, 0.0f, 1.0f);
        }

        void GenerateTerrainSplat()
        {
            SplatPrototype[] terrainTexture = new SplatPrototype[textures.Length];

            for(int i = 0; i < terrainTexture.Length; i++)
            {
                terrainTexture[i] = new SplatPrototype();
                byte[] fileData = System.IO.File.ReadAllBytes(textures[i]);
                Texture2D tex = new Texture2D(2, 2);
                tex.LoadImage(fileData);
                terrainTexture[i].texture = tex;
            }

            terrainData.splatPrototypes = terrainTexture;
            
            terrainObj.AddComponent(typeof(TerrainSplat));

            MeshRenderer renderer = terrainObj.AddComponent<MeshRenderer>();

            renderer.material.shader = shader;
        }

        void AddTreesAndRocks()
        {
            // Clear current trees
            terrainData.treeInstances = new TreeInstance[numTreesAndRocks];
            System.Random random = new System.Random();
            int numTreePrototypes = terrainData.treePrototypes.Length;
            TreeInstance tree;

            

            // Add trees
            for(int i = 0; i < numTreesAndRocks; i++)
            {
                tree = new TreeInstance();
                tree.color = Color.white;
                tree.lightmapColor = Color.white;
                tree.position = new Vector3(Random.value, 0.0f, Random.value);

                // Tree (index 0) or rock (rest)
                if (Random.value <= treePercentage)
                    tree.prototypeIndex = 0;
                else
                    tree.prototypeIndex = random.Next(1, numTreePrototypes);

                tree.rotation = Random.Range(0.0f, 2*Mathf.PI);
                tree.heightScale = 1.0f;
                tree.widthScale = 1.0f;
                terrain.AddTreeInstance(tree);
            }
        }
    }
}
