using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace COMP30019.Project2
{
    public class TimeUpdate : MonoBehaviour
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
            timeText.text = (Time.time - timeStart).ToString("00:00.00");
        }
    }
}