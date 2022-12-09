using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    
    private Vector2 moveDirection;
    [SerializeField] private float moveSpeed = 10f;

    private float bulletDmg = 10f;

    private void OnEnable()
    {
       Invoke(nameof(Destroy), 10f);
       moveSpeed = BossVan.Instance.bulletForce;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        //transform.position = Vector2.Lerp(transform.position, moveDirection, Time.deltaTime * moveSpeed);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("HIT PLAYER");
            collision.gameObject.GetComponent<Player>().TakeDamage((int)bulletDmg);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("LeftBound") || collision.gameObject.CompareTag("RightBound") ||
            collision.gameObject.CompareTag("BotBound"))
        {
            Destroy(gameObject);
        }
    }
}

