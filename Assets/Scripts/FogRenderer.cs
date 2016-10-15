using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class FogRenderer : MonoBehaviour
    {
        [Tooltip("Material with the fog shader")]
        public Material fogMaterial;

        void Start()
        {
            gameObject.GetComponentInParent<Camera>().depthTextureMode = DepthTextureMode.Depth;   
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, fogMaterial);
        }
    }
}