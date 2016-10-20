using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace COMP30019.Project2
{
    public class HighScore : MonoBehaviour
    {
        Text timeText;

        void Start()
        {
            timeText = GetComponent<Text>();
        }

        void Update()
        {
            float highscore = PlayerPrefs.GetFloat("highscore");
            timeText.text = "Best Time: " + highscore.ToString("00.00");

            // If no highscore
            if (highscore == 0.0f)
                timeText.text = "";
        }
    }
}
