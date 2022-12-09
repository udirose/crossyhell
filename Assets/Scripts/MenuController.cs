using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

    public class MenuController : MonoBehaviour
    {
        public static MenuController instance;


        //outlets
        public GameObject mainMenu;
        public GameObject instructionsMenu;

        private void Awake()
        {
            instance = this;
            Hide();
        }
        public void Show()
        {
            ShowMainMenu();
            gameObject.SetActive(true);
            Time.timeScale = 0;
            Player.Instance.isPaused = true;
        }
        public void Hide()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
            if (Player.Instance != null)
            {
            Player.Instance.isPaused = false;
            }
        }
        public void LoadLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        void SwitchMenu(GameObject someMenu)
        {
            //clean up menus
            mainMenu.SetActive(false);
            instructionsMenu.SetActive(false);

            //turn on requested menu
            someMenu.SetActive(true);
        }
        public void ShowMainMenu()
        {
            SwitchMenu(mainMenu);
        }
        public void ShowInstructionsMenu()
        {
            SwitchMenu(instructionsMenu);
        }
        
        public void Mute()
        {
            PlayerPrefs.SetString("IsMuted","true");
            AudioListener.volume = 0f;
            PlayerPrefs.Save();
        }

        public void Unmute()
        {
            PlayerPrefs.SetString("IsMuted","false");
            AudioListener.volume = 1f;
            PlayerPrefs.Save();
        }

        public void Quit()
        {
            SceneManager.LoadScene("StartScreen");
        }
       
    }


