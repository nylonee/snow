using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class DestroyTreesInCheckpoint : MonoBehaviour
    {
        void OnCollisionEnter(Collision collision)
        {
            GameObject collisionObj = collision.collider.gameObject;

            if (collisionObj.tag == "Tree")
                Destroy(collisionObj);

            Debug.Log("boom");
        }

        void OnTriggerEnter(Collider other)
        {
            GameObject collisionObj = other.gameObject;

            if (collisionObj.tag == "Tree")
                Destroy(collisionObj);

            Debug.Log("boom");
        }

    }
}