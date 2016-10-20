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
            if(player == null) player = GameObject.FindGameObjectWithTag("Player");

            // Check if player missed the checkpoint
            if (player.transform.position.z > transform.position.z + distPastCheckpointToFail)
            {
                PlayerPrefs.SetString("endgamestate", "lose");
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
            }
        }

        void OnTriggerEnter(Collider other)
        {
            // Remove this script if player passes through the checkpoint
            if(other.gameObject.tag == "Player")
            {
                Destroy(this);
            }
        }
    }
}