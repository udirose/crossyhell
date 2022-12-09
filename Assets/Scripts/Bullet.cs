using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDmg = 10;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            collision.gameObject.GetComponent<Player>().TakeDamage(bulletDmg);
        }
        if (collision.gameObject.CompareTag("InvinciblePlayer"))
        {
            Destroy(gameObject);
           
        }
        if (collision.gameObject.CompareTag("RightBound") || collision.gameObject.CompareTag("LeftBound") || collision.gameObject.CompareTag("BotBound") ||  collision.gameObject.CompareTag("Obstacle")) {
            Destroy(gameObject);
        }
        
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
