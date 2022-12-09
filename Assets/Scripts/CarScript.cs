using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    public float carSpeed;
    public GameObject bulletPrefab;
    public float shotFrequency;
    public float bulletForce;
    public bool toTheRight = true;
    public int damageOnImpact = 40;
    Player player;
    public bool fireAlt = false;

    // Start is called before the first frame update
    void Start()
    {
        fireAlt = false;
        player = GameObject.FindObjectOfType<Player>();
        FireBullets();
        StartCoroutine(nameof(BulletTimer));
    }

    // Update is called once per frame
    void Update()
    {
        if (toTheRight)
        {
            transform.position += new Vector3(carSpeed*Time.deltaTime, 0, 0);
        }
        else
        {
            transform.position += new Vector3(-carSpeed*Time.deltaTime, 0, 0);
        }
    }

    IEnumerator BulletTimer()
    {
        yield return new WaitForSeconds(shotFrequency);
        if (!fireAlt)
        {
            FireBullets();
        }
        else
        {
            FireAltBullets();
        }
        
        StartCoroutine(nameof(BulletTimer));
    }

    void FireBullets()
    {
        if (player.currentHealth > 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D playerMotion = player.GetComponent<Rigidbody2D>();
            Vector2 deltaVel = (player.transform.position - transform.position + (Vector3)playerMotion.velocity).normalized * bulletForce;
            rbBullet.AddForce(deltaVel, ForceMode2D.Impulse);
        }
    }

    void FireAltBullets()
    {
        var playerPos = player.transform.position;
        var pos = transform.position;
        if (player.currentHealth > 0)
        {
            var direction1 = Quaternion.Euler(0, 0, 0) * (playerPos - pos);
            var direction2 = Quaternion.Euler(0, 0, 30) * (playerPos - pos);
            var direction3 = Quaternion.Euler(0, 0, 60) * (playerPos - pos);
            
            Rigidbody2D playerMotion = player.GetComponent<Rigidbody2D>();
            
            GameObject bullet1 = Instantiate(bulletPrefab, pos, Quaternion.identity);
            GameObject bullet2 = Instantiate(bulletPrefab, pos, Quaternion.identity);
            GameObject bullet3 = Instantiate(bulletPrefab, pos, Quaternion.identity);
            Rigidbody2D rbBullet1 = bullet1.GetComponent<Rigidbody2D>();
            Rigidbody2D rbBullet2 = bullet2.GetComponent<Rigidbody2D>();
            Rigidbody2D rbBullet3 = bullet3.GetComponent<Rigidbody2D>();
            
            //Vector2 deltaVel = (playerPos - pos + (Vector3)playerMotion.velocity).normalized * bulletForce;
            rbBullet1.AddForce(direction1.normalized *bulletForce, ForceMode2D.Impulse);
            rbBullet2.AddForce(direction2.normalized*bulletForce, ForceMode2D.Impulse);
            rbBullet3.AddForce(direction3.normalized*bulletForce, ForceMode2D.Impulse);
        }
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damageOnImpact);
          // collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(1,0), ForceMode2D.Impulse);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("RightBound") || col.gameObject.CompareTag("LeftBound"))
        {
            Destroy(gameObject);
        }
    }
    
    public void SetShotFreq(float newFreq)
    {
        shotFrequency = newFreq;
    }

    public void SetBulletForce(float newForce)
    {
        bulletForce = newForce;
    }

    public void SetSpeed(float newSpeed)
    {
        carSpeed = newSpeed;
    }

    public void SetFireAlt()
    {
        fireAlt = true;
    }
}
