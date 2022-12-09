using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//script by sebastian lague
namespace StartScreen
{
    public class DisplayHighscores : MonoBehaviour
    {
        public TextMeshProUGUI[] highscoreFields;
        Highscores highscoresManager;

        void Start() {
            for (int i = 0; i < highscoreFields.Length; i ++) {
                highscoreFields[i].text = i+1 + ". Fetching...";
            }

				
            highscoresManager = GetComponent<Highscores>();
            StartCoroutine("RefreshHighscores");
        }
	
        public void OnHighscoresDownloaded(Highscore[] highscoreList) {
            for (int i =0; i < highscoreFields.Length; i ++) {
                highscoreFields[i].text = i+1 + ". ";
                if (i < highscoreList.Length) {
                    highscoreFields[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
                }
            }
        }
	
        IEnumerator RefreshHighscores() {
            while (true) {
                highscoresManager.DownloadHighscores();
                yield return new WaitForSeconds(30);
            }
        }
    }
}
