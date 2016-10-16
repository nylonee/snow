using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class Checkpoint : MonoBehaviour
    {
        public float distPastCheckpointToFail = 5.0f;
        private GameObject player;
        private TimeUpdate score;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            score = GameObject.FindGameObjectWithTag("Time").GetComponent<TimeUpdate>();
        }

        void Update()
        {
            if(player == null) player = GameObject.FindGameObjectWithTag("Player");

            if (player.transform.position.z > transform.position.z + distPastCheckpointToFail)
            {
                if(PlayerPrefs.GetFloat("highscore") > score.getTime()) PlayerPrefs.SetFloat("highscore", score.getTime());
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