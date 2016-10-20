using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class OutlineToggler : MonoBehaviour
    {
        public Material posterizationMaterial;

        void Start()
        {
            posterizationMaterial.SetInt("_IsOutlineEnabled", PlayerPrefs.GetInt("outline"));
        }

        void Update()
        {
            if (Input.GetKeyDown("o"))
            {
                if (posterizationMaterial.GetInt("_IsOutlineEnabled") == 0)
                {
                    PlayerPrefs.SetInt("outline", 1);
                    posterizationMaterial.SetInt("_IsOutlineEnabled", 1);
                }
                else
                {
                    PlayerPrefs.SetInt("outline", 0);
                    posterizationMaterial.SetInt("_IsOutlineEnabled", 0);
                }
            }
        }
    }
}