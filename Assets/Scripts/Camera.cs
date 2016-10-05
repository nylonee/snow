using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class Camera : MonoBehaviour
    {

        public GameObject player;

        public Vector3 offset;
        public Vector3 rotation = new Vector3 { x = 45, y = 0, z = 0 };

        void Start()
        {
        }

        void LateUpdate()
        {
            transform.position = player.transform.position + offset;
            transform.rotation = Quaternion.Euler(rotation);
        }
    }
}
