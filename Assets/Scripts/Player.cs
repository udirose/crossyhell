using System;
using System.Collections;
using System.Collections.Generic;
using StartScreen;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player Instance;
    public bool isPaused;
    public float startingSpeed;
    private float speed;
    public int maxHealth;
    public int currentHealth;
    public Healthbar healthBar;
    public float mySpeed;
    bool movingBackwards;
    bool stopMoving;



    Animator animator;
    public SpriteRenderer sprite;
    bool isSpriteSideways;
    float checkIdle;
    private bool spedUp;
    private float time;
    private int roadsCrossed = 0;
    AudioSource audioSource;
    public TextMeshProUGUI roadCrossText;

    
    public bool deathDefied;
    public bool unlockedPenguin;
    public bool unlockedTiger;
    
    //tracking roadscrossed
    private string lastRoadCrossed;
    
    //sprites and color changes
    public SpriteRenderer bg1Sprite;
    public SpriteRenderer bg2Sprite;
    private bool hitEffect;
    private float hitEffectTime;


    Rigidbody2D _rigidbody;
    Vector2 lastPosition = Vector2.zero;



    void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("MaxHealth",PlayerPrefs.GetInt("MaxHealth",100));
        PlayerPrefs.SetFloat("PlayerSpeed",PlayerPrefs.GetFloat("PlayerSpeed",startingSpeed));
        PlayerPrefs.Save();
        if(!PlayerPrefs.HasKey("PlayerSprite"))
        {
            Debug.Log("hellooo");
            PlayerPrefs.SetInt("PlayerSprite", 0);
        }
    }

 
    void Start()
    {
        PlayerPrefs.SetInt("roadsCrossed",0);
        PlayerPrefs.Save();
        //DYNAMIC STUFF
        sprite = GetComponent<SpriteRenderer>();
        _rigidbody = GetComponent<Rigidbody2D>();


        
        if(PlayerPrefs.GetString("IsMuted","false")=="true")
        {
            AudioListener.volume = 0f;
        }
        else
        {
            AudioListener.volume = 1f;
        }
        
        deathDefied = GameController.Instance.hasDeathDefiedUnlocked;
        maxHealth = PlayerPrefs.GetInt("MaxHealth");
        startingSpeed = PlayerPrefs.GetFloat("PlayerSpeed");
        roadsCrossed = 0;
        speed = startingSpeed;
        mySpeed = speed;
        currentHealth = maxHealth;
        Debug.Log("Starting speed: "+ speed);
        Debug.Log("Max Health: "+maxHealth);
        Debug.Log("DeathDefied: "+deathDefied);
        healthBar.SetMaxHealth(maxHealth);
        animator = GetComponent<Animator>();

        //set all the layers to be cleared
        for(int i = 0; i<2; i++)
        {
 
            animator.SetLayerWeight(i, 0);

        }
        //set the preferred layer to be solid
        animator.SetLayerWeight(PlayerPrefs.GetInt("PlayerSprite"), 1);

        //sprite.sprite = spriteOptions[PlayerPrefs.GetInt("PlayerSprite")];
        // sprite.sprite = spriteOptions[1];
        //sprite.overrideSprite = spriteOptions[1];

        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = 1f;
    }

    void FixedUpdate()
    {
      
        roadCrossText.text = "Score: " + roadsCrossed;
        var position = _rigidbody.position;
        //end
        checkIdle = (((position - lastPosition).magnitude) / Time.fixedDeltaTime);
        lastPosition = position;
        //check if chicken is sped up
        if (mySpeed > speed)
        {
            spedUp = true;
        }

        speed = mySpeed;

        //check if chicken is idling
        if (checkIdle < 0.1)
        {
            animator.SetFloat("Speed", checkIdle);
        }
        else
        {
            animator.SetFloat("Speed", mySpeed);
        }

        animator.SetBool("isSideways", isSpriteSideways);
        //if chicken is boosted, give him 3 seconds of speed boost and then go back
        if (spedUp)
        {
            time += Time.deltaTime;
            audioSource.pitch += 0.001f;
            if (time >= 3)
            {
                mySpeed = startingSpeed;
                time = 0;
                spedUp = false;
                audioSource.pitch = 1f;
            }
        }

        //if the player has been made invincible, wait 5 seconds and then turn back
        if (CompareTag("InvinciblePlayer"))
        {
            Color c = sprite.material.color;
            c.a = 0.5f;
            sprite.material.color = c;

            time += Time.deltaTime;
            if (time >= 5)
            {
                tag = "Player";
                c.a = 1f;
                time = 0;
                sprite.material.color = c;
            }
        }

        //movement
        if (Input.GetKey(KeyCode.W))
        {
      
            _rigidbody.AddForce(Vector2.up * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            sprite.flipY = false;
            isSpriteSideways = false;
            movingBackwards = false;
            stopMoving = false;
        }

        if (Input.GetKey(KeyCode.A))
        {

            _rigidbody.AddForce(Vector2.left * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            if (PlayerPrefs.GetInt("PlayerSprite") == 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            sprite.flipY = false;
            isSpriteSideways = true;
            movingBackwards = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (!stopMoving)
            {

                _rigidbody.AddForce(Vector2.down * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
                sprite.flipY = true;
                isSpriteSideways = false;
                movingBackwards = true;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {

            _rigidbody.AddForce(Vector2.right * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
            if(PlayerPrefs.GetInt("PlayerSprite") == 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
            
            sprite.flipY = false;
            isSpriteSideways = true;
            movingBackwards = false;
        }

        //change background color
        switch (roadsCrossed)
        {
            case >= 25 and < 49:
                bg1Sprite.color = new Color(255, 108, 0, 200);
                bg2Sprite.color = new Color(255, 108, 0, 200);
                break;
            case >= 50 and < 89:
                bg1Sprite.color = new Color(255, 58, 0, 200);
                bg2Sprite.color = new Color(255, 58, 0, 200);
                break;
            case >= 90:
                bg1Sprite.color = new Color(255, 0, 0, 200);
                bg2Sprite.color = new Color(255, 0, 0, 200);
                break;
        }
    }

    void Update()
    {
        if (isPaused)
        {
            return;
        }
        //Death
        if (currentHealth <= 0)
        {
            if (deathDefied)
            {
                DeathDefied();
                deathDefied = false;
            } else {
                PlayerPrefs.SetInt("roadsCrossed", roadsCrossed);
                PlayerPrefs.SetInt("BossesBeat",(int)GameController.Instance.bossesBeat);
                PlayerPrefs.Save();
                GameController.Instance.AddToGlobalScore(roadsCrossed);
                GameController.Instance.AddToGlobalScore((20*(int)GameController.Instance.bossesBeat));
                SceneManager.LoadScene("DeathScene");
                Destroy(gameObject);
            }
        }
        //pause code
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MenuController.instance.Show();
        }
        
        if (hitEffect && (Time.time > hitEffectTime))
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
        }
        
        //unlock characters
        if (roadsCrossed >= 15)
        {
            PlayerPrefs.SetInt("unlockedPenguin",1);
            unlockedPenguin = true;
        }

        if (roadsCrossed >= 30)
        {
            PlayerPrefs.SetInt("unlockedTiger",1);
            unlockedTiger = true;
        }
    }

    public void TakeDamage(int damage)
    {
        hitEffect = true;
        GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        SoundManager.instance.PlaySoundHit();
        hitEffectTime = Time.time + .5f;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
  
      
        //road cross calculation
        if (col.gameObject.CompareTag("Road") && col.gameObject.name != lastRoadCrossed)
        {
            lastRoadCrossed = col.gameObject.name;
            if (movingBackwards)
            {
                Debug.Log("WHAYYYY");
                stopMoving = true;
                _rigidbody.velocity = Vector3.zero;
            }
            roadsCrossed++;
        }
    }

    public void HealAll()
    {
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth);
    }
    public void DeathDefied()
    {
        currentHealth = maxHealth/2;
        healthBar.SetHealth(currentHealth);
    }
}