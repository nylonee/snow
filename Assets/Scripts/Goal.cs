using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class Goal : MonoBehaviour
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
            if (player.transform.position.z > transform.position.z + distPastCheckpointToFail)
            {
                // Missed goal
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                PlayerPrefs.SetString("endgamestate", "lose");
            }
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                // GOL!!!!!!!!!!!!!
                PlayerPrefs.SetFloat("yourscore", score.getTime());
                PlayerPrefs.SetString("endgamestate", "win");
                if (PlayerPrefs.GetFloat("highscore") > score.getTime() || PlayerPrefs.GetFloat("highscore") == 0f) PlayerPrefs.SetFloat("highscore", score.getTime());
                
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
                Destroy(this);
            }
        }
    }
}