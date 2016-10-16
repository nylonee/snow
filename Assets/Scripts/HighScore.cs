using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace COMP30019.Project2
{
    public class HighScore : MonoBehaviour
    {
        Text timeText;
        float timeStart;

        void Start()
        {
            timeText = GetComponent<Text>();
            timeStart = Time.time;
        }

        void Update()
        {
            timeText.text = "High Score: " + PlayerPrefs.GetFloat("highscore").ToString("00.00");
        }
    }
}
