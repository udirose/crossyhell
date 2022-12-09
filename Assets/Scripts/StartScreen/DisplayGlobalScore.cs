using TMPro;
using UnityEngine;

namespace StartScreen
{
    public class DisplayGlobalScore : MonoBehaviour
    {
        public TextMeshProUGUI globalScoreDisplay;
        public static DisplayGlobalScore Instance;
        void Start()
        {
            Instance = this;
            globalScoreDisplay.text = "Total Points: "+PlayerPrefs.GetInt("GlobalScore");
        }

        public void UpdateGlobalScoreAfterPurchase()
        {
            globalScoreDisplay.text = "Total Points: "+PlayerPrefs.GetInt("GlobalScore");
        }

        // Update is called once per frame
        void Update()
        {
            globalScoreDisplay.text = "Total Points: "+PlayerPrefs.GetInt("GlobalScore");
        }
    }
}
