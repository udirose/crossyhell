using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossVan : MonoBehaviour
{
    [SerializeField] private float carSpeed;
    private float startSpeed;
    public GameObject bulletPrefab;
    [SerializeField] private float shotFrequency = .8f;
    public float bulletForce = 8f;
    private float bullet1_timer;
    private float bullet2_timer;
    
    private float start_time;
    
    //for spread
    private int bulletAmount = 36;
    private float startAngle = 90f, endAngle = 180f;
    private Vector2 bulletMoveDirection;

    private Player target;

    public static BossVan Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance != null && Instance != this) {
            DestroyImmediate(gameObject);
            return;
        }
        Instance = this;
    }

    private void Start()
    {
        target = Player.Instance;
        shotFrequency /= (GameController.Instance.bossesBeat + 1);
        bulletForce += (5*GameController.Instance.bossesBeat);
        start_time = Time.time;
        startSpeed = carSpeed;
        bullet1_timer = shotFrequency;
        bullet2_timer = shotFrequency;
    }

    // Update is called once per frame
    private void Update()
    {
        float time = Time.time - start_time;
        //move car
        transform.position += new Vector3(carSpeed * Time.deltaTime, 0, 0);
        //Fire One Code
        if (time < 10)
        {
            bullet1_timer -= Time.deltaTime;
            if (bullet1_timer <= 0)
            {
                //Fire1();
                FireSpread();
                bullet1_timer = shotFrequency;
            }
        }

        if (time is > 10)
        {
            carSpeed = startSpeed + .5f;
            bullet2_timer -= Time.deltaTime;
            if (bullet2_timer <= 0)
            {
                //Fire2();
                FireSpread();
                bullet2_timer = shotFrequency/1.5f;
            }
        }
    }

    private void OnDestroy()
    {
        GameController.Instance.bossPresent = false;
        GameController.Instance.bossDied = true;
        GameController.Instance.bossesBeat++;
    }
    private void FireSpread()
    {
        for(int i = 0; i < bulletAmount + 1; i++)
        {
            var playerPos = target.transform.position;
            var pos = transform.position;
            var direction = Quaternion.Euler(0, 0, i*18) * (playerPos - pos);

            GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D playerMotion = target.GetComponent<Rigidbody2D>();
            rbBullet.AddForce((direction+(Vector3)playerMotion.velocity).normalized *bulletForce, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);
    }
}
