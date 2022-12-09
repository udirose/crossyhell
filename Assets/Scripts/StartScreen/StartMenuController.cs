using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StartScreen
{
    public class StartMenuController : MonoBehaviour
    {
        public static StartMenuController Instance;
        
        //outlets
        public GameObject shop;
        public GameObject options;
        public GameObject leaderboard;

        public GameObject sprites;
        
        void Awake()
        {
            Instance = this;
            Hide();
        }

        void Update(){
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetString("name"));
            if (PlayerPrefs.GetString("IsMuted","false") == "true")
            {
                AudioListener.volume = 0f;
            }
            else
            {
                AudioListener.volume = 1f;
            }
        }


        public void ShowShopMenu()
        {
            Show();
            options.SetActive(false);
            leaderboard.SetActive(false);
            shop.SetActive(true);
            sprites.SetActive(false);
        }

        public void ShowOptionsMenu()
        {
            Show();
            shop.SetActive(false);
            leaderboard.SetActive(false);
            options.SetActive(true);
            sprites.SetActive(false);
        }
        public void ShowSpritesMenu()
        {
            Show();
            shop.SetActive(false);
            options.SetActive(false);
            leaderboard.SetActive(false);
            sprites.SetActive(true);
        }

        public void ShowLeaderboard()
        {
            Show();
            options.SetActive(false);
            shop.SetActive(false);
            sprites.SetActive(false);
            leaderboard.SetActive(true);
            Highscores.instance.DownloadHighscores();
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        void SwitchMenu(GameObject someMenu)
        {
            options.SetActive(false);
            shop.SetActive(false);
            leaderboard.SetActive(false);
            sprites.SetActive(false);
            //turn on requested menu
            someMenu.SetActive(true);

        }

        public void ResetProgress()
        {
            PlayerPrefs.DeleteAll();
            PlayerPrefs.Save();
        }

        public void Mute()
        {
            PlayerPrefs.SetString("IsMuted","true");
            PlayerPrefs.Save();
        }

        public void Unmute()
        {
            PlayerPrefs.SetString("IsMuted","false");
            PlayerPrefs.Save();
        }

       
    }
}
    

