using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PowerUp;
using TMPro;
using UnityEngine.UI;

namespace StartScreen
{
    public class ChoosePlayerController : MonoBehaviour
    {
        public GameObject[] buttons;

        public static ChoosePlayerController Instance;

        public TextMeshProUGUI penguinText;
        public TextMeshProUGUI tigerText;
        public Image locked;
        private float disapearTime;

        //singleton
        void Awake()
        {
            penguinText.enabled = false;
            tigerText.enabled = false;
            locked.enabled = false;
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(gameObject);
                return;
            }
            Instance = this;
            //DontDestroyOnLoad(gameObject);
           
            buttons[PlayerPrefs.GetInt("PlayerSprite")].GetComponent<Button>().Select();
            //buttons[0].GetComponent<Button>().Select();
        }

        void Update()
        {
            if (tigerText.enabled && (Time.time > disapearTime))
            {
                locked.enabled = false;
                tigerText.enabled = false;
            }
            if (penguinText.enabled && (Time.time > disapearTime))
            {
                locked.enabled = false;
                penguinText.enabled = false;
            }
        }

        public void ChooseChicken()
        {
            PlayerPrefs.SetInt("PlayerSprite", 0);
            PlayerPrefs.Save();
        }
        public void ChooseMouse()
        {
            if (PlayerPrefs.GetInt("unlockedTiger") == 1)
            {
                PlayerPrefs.SetInt("PlayerSprite", 1);
                PlayerPrefs.Save();
            }
            else
            {
                locked.enabled = true;
                tigerText.enabled = true;
                disapearTime = Time.time + 1f;
            }
        }
        public void ChooseBird()
        {
            if (PlayerPrefs.GetInt("unlockedPenguin") == 1)
            {
                PlayerPrefs.SetInt("PlayerSprite", 2);
                PlayerPrefs.Save();
            }
            else
            {
                locked.enabled = true;
                penguinText.enabled = true;
                disapearTime = Time.time + 1f;
            }
            
        }
    }
}
