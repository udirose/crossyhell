using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//help from https://www.youtube.com/watch?v=E40ZdSaGsZY 
namespace PowerUp
{
    public class SpawnPowerups : MonoBehaviour
    {
        public GameObject[] diffPowerups;
        public float repeatRate = 6f;

        // Start is called before the first frame update
        void Start()
        {
            repeatRate = PlayerPrefs.GetFloat("powerupSpawnRate", 20);
            InvokeRepeating("SpawnSomePowerups", 2.0f, repeatRate);
            Debug.Log("PowerUpFrequency: "+repeatRate);
        }
        void SpawnSomePowerups()
        {
            Instantiate(diffPowerups[Random.Range(0, diffPowerups.Length)],
                new Vector3(transform.position.x + Random.Range(-10, 10),
                transform.position.y + Random.Range(-10, 10), 5), Quaternion.identity);

        }

        public void SetRepeatRate(float val)
        {
            repeatRate = val;
        }
        public float GetRepeatRate()
        {
            return repeatRate;
        }
        
  
    }
}

