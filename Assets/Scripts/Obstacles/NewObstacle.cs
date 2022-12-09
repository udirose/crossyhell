using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
namespace Obstacles
{
    public class NewObstacle : MonoBehaviour
    {
        public Obstacles obstacles;

        private void OnCollisionStay2D(Collision2D collision)
        {


                if (collision.gameObject.CompareTag("Player"))
                {
                    obstacles.HitObstacle(collision.gameObject);
                   // collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1, 0), ForceMode2D.Impulse);
                }
            
               
        }

        void Update()
        {
            if (GameController.Instance.bossPresent)
            {
                Destroy(gameObject);
            }
        }
       


        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
