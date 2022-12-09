using TMPro;
using UnityEngine;

namespace DeathScreen
{
    public class DisplayBossesBeat : MonoBehaviour
    {
        public TextMeshProUGUI bossesBeat;
        private float t;
        // Start is called before the first frame update
        void Start()
        {
            bossesBeat.text = "You beat " + PlayerPrefs.GetInt("BossesBeat") + " bosses!";
            PlayerPrefs.SetInt("BossesBeat",0);
            PlayerPrefs.Save();
        }
    }
}
