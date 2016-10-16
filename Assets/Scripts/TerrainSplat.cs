using UnityEngine;
using System.Collections;
using System.Linq;

namespace COMP30019.Project2
{
    public class TerrainSplat : MonoBehaviour
    {
        void Start()
        {
            TerrainData terrainData = ((Terrain)GetComponent("Terrain")).terrainData;
            
            float[,,] map = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

            for (int y = 0; y < terrainData.alphamapHeight; y++)
                for (int x = 0; x < terrainData.alphamapWidth; x++)
                    for (int i = 0; i < terrainData.alphamapLayers; i++)
                        map[x, y, i] = 1f/terrainData.alphamapLayers;
            
            terrainData.SetAlphamaps(0, 0, map);
        }
    }
}
