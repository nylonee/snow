using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class CameraPan : MonoBehaviour
    {

        private CameraFollow pCamera;

        void Update()
        {
            if(pCamera == null) pCamera = GameObject.FindGameObjectWithTag("Player").GetComponent<CameraFollow>();
        }

        public void lookForward()
        {
            pCamera.orientation = Vector3.right;
        }

        public void lookBackward()
        {
            pCamera.orientation = Vector3.left;
        }

        public void lookLeft()
        {
            pCamera.orientation = Vector3.forward;
        }

        public void lookRight()
        {
            pCamera.orientation = Vector3.back;
        }
    }
}
