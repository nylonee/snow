using UnityEngine;
using System.Collections;

namespace COMP30019.Project2
{
    public class GameState : MonoBehaviour
    {
        public void loadGame()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        }

        public void endGame()
        {
            Application.Quit();
        }

        public void mainMenu()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

}