/*InputManager.cs
* Description: Handles user input.
*/
using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class InputManager : MonoBehaviour
    {
        public float force = 1000.0f;
        public float rotationSpeed = 75.0f;
        public float turnLift = 250.0f;

        private bool isTouchingGround = false;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        float getTilt()
        {
            float direction = 0.0f;

            if (Input.GetKey(KeyCode.D)) direction += 1.0f;
            if (Input.GetKey(KeyCode.A)) direction -= 1.0f;

            return direction;
        }

        void FixedUpdate()
        {
            if (isTouchingGround)
            {
                rb.MoveRotation(transform.rotation * Quaternion.Euler(0.0f, getTilt() * rotationSpeed * Time.deltaTime, 0.0f));
                rb.AddRelativeTorque(getTilt() * Vector3.right * turnLift * Time.deltaTime);
                rb.AddForce(rb.rotation * Vector3.left * force * Time.deltaTime);
            }
            else
            {
                rb.MoveRotation(transform.rotation * Quaternion.Euler(0.0f, getTilt() * rotationSpeed * 2 * Time.deltaTime, 0.0f));
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.tag == "Terrain")
                isTouchingGround = true;
        }

        void OnCollisionExit(Collision collision)
        {
            if (collision.collider.gameObject.tag == "Terrain")
                isTouchingGround = false;
        }
    }
}
