using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class WorldGenerator : MonoBehaviour
    {
        public GameObject terrainGeneratorObj;

        private TerrainGenerator terrainGenerator;

        void Start()
        {
            terrainGenerator = terrainGeneratorObj.GetComponent<TerrainGenerator>();

            terrainGenerator.Generate();
        }
    }
}
