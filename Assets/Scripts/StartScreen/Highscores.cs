using System.Collections;
using TMPro;
using UnityEngine;
//script by sebastian lague
namespace StartScreen
{
    public class Highscores : MonoBehaviour
    {
        const string privateCode = "5-WMRw1fyEW6cnWn9rt9GgyH56oVGjMUqJImg5zWq8yA";
        const string publicCode = "637d2ba48f40bb11044e7495";
        const string webURL = "https://www.dreamlo.com/lb/";

        DisplayHighscores highscoreDisplay;
        public Highscore[] highscoresList;
        public static Highscores instance;
        
        //outlets
        public TMP_InputField name;
	
        void Awake() {
            highscoreDisplay = GetComponent<DisplayHighscores> ();
            instance = this;
        }

        void Start()
        {
            name.text = "";
            if (PlayerPrefs.HasKey("name"))
            {
                name.text = PlayerPrefs.GetString("name");
                if (PlayerPrefs.GetInt("roadsCrossed", 0) > 0)
                {
                    Highscores.AddNewHighscore(PlayerPrefs.GetString("name"),PlayerPrefs.GetInt("roadsCrossed"));
                }
            }
            name.onEndEdit.AddListener(ValueChangeCheck);
        }

        public void ValueChangeCheck(string text)
        {
            PlayerPrefs.SetString("name",text);
            PlayerPrefs.Save();
            name.text = PlayerPrefs.GetString("name");
        }

        public static void AddNewHighscore(string username, int score) {
            instance.StartCoroutine(instance.UploadNewHighscore(username,score));
        }

        IEnumerator UploadNewHighscore(string username, int score) {
            WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL(username) + "/" + score);
            yield return www;

            if (string.IsNullOrEmpty(www.error)) {
                print ("Upload Successful");
                DownloadHighscores();
            }
            else {
                print ("Error uploading: " + www.error);
            }
        }

        public void DownloadHighscores() {
            StartCoroutine("DownloadHighscoresFromDatabase");
        }

        IEnumerator DownloadHighscoresFromDatabase() {
            WWW www = new WWW(webURL + publicCode + "/pipe/");
            yield return www;
		
            if (string.IsNullOrEmpty (www.error)) {
                FormatHighscores (www.text);
                highscoreDisplay.OnHighscoresDownloaded(highscoresList);
            }
            else {
                print ("Error Downloading: " + www.error);
            }
        }

        void FormatHighscores(string textStream) {
            string[] entries = textStream.Split(new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
            highscoresList = new Highscore[entries.Length];

            for (int i = 0; i <entries.Length; i ++) {
                string[] entryInfo = entries[i].Split(new char[] {'|'});
                string username = entryInfo[0];
                int score = int.Parse(entryInfo[1]);
                highscoresList[i] = new Highscore(username,score);
                print (highscoresList[i].username + ": " + highscoresList[i].score);
            }
        }

    }

    public struct Highscore {
        public string username;
        public int score;

        public Highscore(string _username, int _score) {
            username = _username;
            score = _score;
        }
    }
}
