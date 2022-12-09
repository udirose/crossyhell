using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


//help from https://www.youtube.com/watch?v=E40ZdSaGsZY 
namespace Obstacles
{
    public class SpawnObstacles : MonoBehaviour
    {
        public GameObject[] diffObstacles;

        public static SpawnObstacles Instance;
        //public float repeatRate = 5f;

        // Start is called before the first frame update
        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            //repeatRate = PlayerPrefs.GetFloat("obstacleSpawnRate", 20);
            //InvokeRepeating("SpawnSomeObstacles", 2.0f, repeatRate);
            //Debug.Log("PowerUpFrequency: " + repeatRate);
        }
        public void SpawnSomeObstacles()
        {
            Instantiate(diffObstacles[Random.Range(0, diffObstacles.Length)],
                new Vector3(transform.position.x + Random.Range(-10, 10),
                transform.position.y + Random.Range(-10, 10), 5), Quaternion.identity);

        }

        /*public void SetRepeatRate(float val)
        {
            repeatRate = val;
        }
        public float GetRepeatRate()
        {
            return repeatRate;
        }*/


    }
}

