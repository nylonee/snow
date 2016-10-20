using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class Goal : MonoBehaviour
    {
        [Tooltip("Distance past the goal at which the player fails the game")]
        public float distPastGoalToFail = 5.0f;

        private GameObject player;
        private TimeUpdate score;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            score = GameObject.FindGameObjectWithTag("Time").GetComponent<TimeUpdate>();
        }

        void Update()
        {
            if (player.transform.position.z > transform.position.z + distPastGoalToFail)
            {
                // Missed goal
                PlayerPrefs.SetString("endgamestate", "lose");
                UnityEngine.SceneManagement.SceneManager.LoadScene(2);
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