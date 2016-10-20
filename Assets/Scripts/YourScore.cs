using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace COMP30019.Project2
{
    public class YourScore : MonoBehaviour
    {
        Text timeText;

        void Start()
        {
            timeText = GetComponent<Text>();
        }

        void Update()
        {
            float score = PlayerPrefs.GetFloat("yourscore");

            if (PlayerPrefs.GetString("endgamestate") == "win")
            {
                if (score != 0f)
                    timeText.text = "Your Time: " + score.ToString("00.00");
                else
                    timeText.text = "";
            }
            else
            {
                timeText.text = "You failed to complete the slalom course!";
            }
        }
    }
}
