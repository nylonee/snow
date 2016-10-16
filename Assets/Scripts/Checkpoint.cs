using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class Checkpoint : MonoBehaviour
    {
        public float distPastCheckpointToFail = 5.0f;

        private GameObject player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update()
        {
            if(player.transform.position.z > transform.position.z + distPastCheckpointToFail)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.tag == "Player")
            {
                Destroy(this);
            }
        }
    }
}