using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayRoadsCrossed : MonoBehaviour
{
    public TextMeshProUGUI roadText;
    private float t;
    // Start is called before the first frame update
    void Start()
    {
        roadText.text = "You crossed " + PlayerPrefs.GetInt("roadsCrossed") + " roads!";
    }
}
