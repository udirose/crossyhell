using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cited from https://www.youtube.com/watch?v=PkNRPOrtyls


public class NewPowerup : MonoBehaviour
{
    public Powerups powerups;

  
    //destroys the powerup and then applies it to the thing that hit the object(which is the player)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySoundPowerupCollected();
        if (collision.gameObject.name.Contains("Player"))
        {
            Destroy(gameObject);
            powerups.ApplyPowerup(collision.gameObject);
        }
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
