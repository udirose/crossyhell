using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float startTime;
    private float gameTime;
    
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        gameTime = Time.time - startTime;
        /*string minutes = ((int)gameTime/60).ToString();
        string seconds = (gameTime % 60).ToString();
        timerText.text = minutes + ":" + seconds;*/
    }

    public float getGameTime()
    {
        return gameTime;
    }
}
