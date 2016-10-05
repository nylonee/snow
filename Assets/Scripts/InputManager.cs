/*InputManager.cs
* Description: Handles user input.
*/
using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class InputManager : MonoBehaviour
    {
        public Rigidbody player;
        public float strength = 100;
        
        void Start()
        {
        }

        Vector3 getTilt()
        {
            Vector3 direction = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) direction += Vector3.forward;
            if (Input.GetKey(KeyCode.S)) direction += Vector3.back;
            if (Input.GetKey(KeyCode.D)) direction += Vector3.right;
            if (Input.GetKey(KeyCode.A)) direction += Vector3.left;

            if (Input.GetKey(KeyCode.Space)) direction += 10 * Vector3.up; // jump

            return direction;
        }

        void Update()
        {
            player.AddForce(strength * getTilt());
        }
    }
}
