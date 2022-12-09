using System;
using PowerUp;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StartScreen
{
    public class UpgradeManager : MonoBehaviour
    {
        //outlets
        public TextMeshProUGUI maxHealthUpgradeText;
        public TextMeshProUGUI maxSpeedUpgradeText;
        public TextMeshProUGUI powerupUpgradeText;
        public TextMeshProUGUI deathDefiedText;
        public Image validPurchase;
        public TextMeshProUGUI validPurchaseText;
        public Image invalidPurchase;
        public TextMeshProUGUI invalidPurchaseText;
        
        //values
        private int healthCost = 20;
        private int speedCost = 30;
        private int deathCost = 40;
        private int powerupCost = 20;
        private int globalScore;
        
        //ui management
        private float disapearTime;
        
        public static UpgradeManager Instance;
        //singleton
        void Awake()
        {
            if(Instance != null && Instance != this) {
                DestroyImmediate(gameObject);
                return;
            }
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        
        void Start()
        {
            validPurchase.enabled = false;
            validPurchaseText.enabled = false;
            invalidPurchase.enabled = false;
            invalidPurchaseText.enabled = false;
            healthCost = PlayerPrefs.GetInt("HealthCost", 20);
            speedCost = PlayerPrefs.GetInt("SpeedCost", 30);
            powerupCost = PlayerPrefs.GetInt("PowerupCost", 20);
            //ENABLE CHEAT PlayerPrefs.SetInt("GlobalScore",1000000); ////
            globalScore = PlayerPrefs.GetInt("GlobalScore",0);
            maxHealthUpgradeText.text = "Increase MaxHealth: " + healthCost +" points";
            maxSpeedUpgradeText.text = "Increase MaxSpeed: " + speedCost +" points";
            powerupUpgradeText.text = "Increase PowerUp Frequency: " + powerupCost +" points";
            if (PlayerPrefs.GetString("DeathDefied","false") == "true")
            {
                deathDefiedText.text = "DEATH DEFIED ALREADY UNLOCKED";
            }
            else
            {
                deathDefiedText.text = "Unlock Death Defied: " + deathCost+" points";
            }
            
        }

        void Update()
        {
            //text management
            if (validPurchase.enabled && (Time.time > disapearTime))
            {
                validPurchase.enabled = false;
                validPurchaseText.enabled = false;
            }
            if (invalidPurchase.enabled && (Time.time > disapearTime))
            {
                invalidPurchase.enabled = false;
                invalidPurchaseText.enabled = false;
            }
        }
        public void UpgradeMaxHealth(int amt)
        {
            var maxHealth = PlayerPrefs.GetInt("MaxHealth",100);
            var cost = healthCost;

            if (globalScore >= cost)
            {
                EnableValidPurchase();
                PlayerPrefs.SetInt("GlobalScore",globalScore-cost);
                PlayerPrefs.SetInt("MaxHealth",maxHealth+amt);
                PlayerPrefs.SetInt("HealthCost",20+maxHealth/4);
                PlayerPrefs.Save();
                DisplayGlobalScore.Instance.UpdateGlobalScoreAfterPurchase();
                healthCost = PlayerPrefs.GetInt("HealthCost");
                maxHealthUpgradeText.text = "Increase MaxHealth: " + healthCost + " points";
                globalScore = PlayerPrefs.GetInt("GlobalScore");
            }
            else
            {
                EnableInvalidPurchase();
            }

        }

        public void UpgradeMaxSpeed(int amt)
        {
            var speed = PlayerPrefs.GetFloat("PlayerSpeed",250);
            var cost = speedCost;
            if (globalScore >= cost)
            {
                EnableValidPurchase();
                PlayerPrefs.SetInt("GlobalScore",globalScore-cost);
                PlayerPrefs.SetFloat("PlayerSpeed",speed+(float)amt);
                PlayerPrefs.SetInt("SpeedCost",25+Mathf.RoundToInt(speed+amt-250));
                PlayerPrefs.Save();
                DisplayGlobalScore.Instance.UpdateGlobalScoreAfterPurchase();
                speedCost = PlayerPrefs.GetInt("SpeedCost");
                maxSpeedUpgradeText.text = "Increase MaxSpeed: " + speedCost + " points";
                globalScore = PlayerPrefs.GetInt("GlobalScore");
            }
            else
            {
                EnableInvalidPurchase();
            }
            

        }
        public void UnlockDeathDefied()
        {
            if (globalScore >= deathCost && PlayerPrefs.GetString("DeathDefied","false") != "true")
            {
                EnableValidPurchase();
                PlayerPrefs.SetInt("GlobalScore",globalScore-deathCost);
                
                PlayerPrefs.SetString("DeathDefied", "true");
                PlayerPrefs.Save();
                DisplayGlobalScore.Instance.UpdateGlobalScoreAfterPurchase();

                globalScore = PlayerPrefs.GetInt("GlobalScore");
                deathDefiedText.text = "DEATH DEFIED ALREADY UNLOCKED";
            } else if (PlayerPrefs.GetString("DeathDefied") != "true")
            {
                EnableInvalidPurchase();
            }

        }

        public void UpgradePowerupFrequency()
        {
            if (globalScore >= powerupCost)
            {
                EnableValidPurchase();
                PlayerPrefs.SetInt("GlobalScore",globalScore-powerupCost);
                var prevSpawnRate = PlayerPrefs.GetFloat("powerupSpawnRate",20f);
                PlayerPrefs.SetFloat("powerupSpawnRate",prevSpawnRate-2);
                PlayerPrefs.SetInt("PowerupCost",powerupCost + 25);
                PlayerPrefs.Save();
                globalScore = PlayerPrefs.GetInt("GlobalScore");
                powerupCost = PlayerPrefs.GetInt("PowerupCost");
                powerupUpgradeText.text = "Increase PowerUp Frequency: " + powerupCost + " points";
                DisplayGlobalScore.Instance.UpdateGlobalScoreAfterPurchase();
            }
            else
            {
                EnableInvalidPurchase();
            }
            
        }

        private void EnableValidPurchase()
        {
            validPurchase.enabled = true;
            validPurchaseText.enabled = true;
            disapearTime = Time.time + 1f;
        }

        private void EnableInvalidPurchase()
        {
            invalidPurchase.enabled = true;
            invalidPurchaseText.enabled = true;
            disapearTime = Time.time + 1f;
        }
    }
}
