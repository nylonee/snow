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

            if (player.transform.position.z > transform.position.z + distPastCheckpointToFail)
            {
                PlayerPrefs.SetFloat("yourscore", GameObject.FindGameObjectWithTag("Time").GetComponent<TimeUpdate>().getTime());
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
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