using UnityEngine;
using System.Collections;

namespace COMP30019.TeamBronze
{
    public class OutlineToggler : MonoBehaviour
    {
        public Material posterizationMaterial;

        void Update()
        {
            if (Input.GetKeyDown("o"))
            {
                if (posterizationMaterial.GetInt("_IsOutlineEnabled") == 0)
                    posterizationMaterial.SetInt("_IsOutlineEnabled", 1);
                else
                    posterizationMaterial.SetInt("_IsOutlineEnabled", 0);
            }
        }
    }
}