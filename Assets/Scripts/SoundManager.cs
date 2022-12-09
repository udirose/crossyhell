using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    AudioSource audioSource;
    public AudioClip hitSprite;
    public AudioClip pickupPowerup;


    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

   public void PlaySoundHit()
    {
        audioSource.PlayOneShot(hitSprite);

    }
    public void PlaySoundPowerupCollected()
    {
        audioSource.PlayOneShot(pickupPowerup);
    }
}
