using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class ImageEffectRenderer : MonoBehaviour
    {
        [Tooltip("Material with the image effect shader")]
        public Material imageEffectMaterial;

        void Start()
        {
            gameObject.GetComponentInParent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;   
        }

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            Graphics.Blit(src, dest, imageEffectMaterial);
        }
    }
}