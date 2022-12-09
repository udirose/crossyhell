using UnityEngine;
using UnityEngine.SceneManagement;

namespace StartScreen
{
    public class StartButton : MonoBehaviour
    {
        [SerializeField] private string level = "main";
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene(level);
            }
        }

        public void StartGameButton()
        {

            SceneManager.LoadScene(level);
        }
    }
}

