using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace COMP30019.Project2
{
    public class PlayerFallingOverHandler : MonoBehaviour
    {
        private bool isTouchingGround = false;
        private float t;

        void Update()
        {
            if (isTouchingGround)
                if (t < 0.0f)
                    SceneManager.LoadScene(2);
                else
                    t -= Time.deltaTime;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.collider.gameObject.tag == "Terrain")
            {
                isTouchingGround = true;
                t = 3.0f;
            }
        }

        void OnCollisionExit(Collision collision)
        {
            if (collision.collider.gameObject.tag == "Terrain")
                isTouchingGround = false;
        }
    }
}