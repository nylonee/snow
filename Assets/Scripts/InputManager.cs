/*InputManager.cs
* Description: Handles user input.
*/
using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class InputManager : MonoBehaviour
    {
        [Tooltip("Forward force applied to player")]
        public float force = 1250.0f;

        [Tooltip("Rotation speed of the player")]
        public float rotationSpeed = 75.0f;

        [Tooltip("Amount of torque applied to 'lift' the player as they turn")]
        public float turnLift = 750.0f;

        [Tooltip("Amount to artificially assist the player in staying upright")]
        public float uprightAssist = 1.0f;

        private bool isTouchingGround = false;
        private Gyroscope gyro;
        private Rigidbody rb;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            gyro = Input.gyro;

            if (PlayerPrefs.GetInt("gyro") == 1)
                gyro.enabled = true;
            else
                gyro.enabled = false;
        }

        float getTilt()
        {
            float direction = 0.0f;

            if (gyro != null && gyro.enabled)
            {
                direction = (gyro.attitude * Vector3.left).z;
            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                    direction += 1.0f;

                if (Input.GetKey(KeyCode.A))
                    direction -= 1.0f;
            }

            return direction;
        }

        void FixedUpdate()
        {
            if (Input.GetKeyDown("g"))
            {
                if (gyro.enabled)
                {
                    gyro.enabled = false;
                    PlayerPrefs.SetInt("gyro", 0);
                }
                else if (SystemInfo.supportsGyroscope)
                {
                    gyro.enabled = true;
                    PlayerPrefs.SetInt("gyro", 1);
                }
            }

            // Normal movement if touching ground
            if (isTouchingGround)
            {
                rb.MoveRotation(rb.rotation * Quaternion.Euler(0.0f, getTilt() * rotationSpeed * Time.deltaTime, 0.0f));
                rb.AddRelativeTorque(getTilt() * Vector3.right * turnLift * Time.deltaTime);
                rb.AddForce(rb.rotation * Vector3.left * force * Time.deltaTime);

                // Try to keep upright
                rb.MoveRotation(Quaternion.Euler(Mathf.MoveTowardsAngle(FixAngle(rb.rotation.eulerAngles.x), 0.0f, uprightAssist * Time.deltaTime), rb.rotation.eulerAngles.y, rb.rotation.eulerAngles.z));
            }

            // If not touching ground, only rotate
            else
            {
                rb.MoveRotation(rb.rotation * Quaternion.Euler(0.0f, getTilt() * rotationSpeed * 2 * Time.deltaTime, 0.0f));
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

        private float FixAngle(float angle)
        {
            if (angle < 0.0f)
                angle += 360.0f;
            else
                while (angle > 360.0f)
                    angle -= 360.0f;

            return angle;
        }
    }
}
