using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class GameState : MonoBehaviour
    {
        public void LoadGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        public void EndGame()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

}