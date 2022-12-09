using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelBounds : MonoBehaviour
{
  
        // Start is called before the first frame update
        //private void OnTriggerEnter2D(Collider2D other)
        //{
        //    if (other.gameObject.CompareTag("Car"))
        //    {
        //        Destroy(other.gameObject);
        //    }

        //    if (other.gameObject.CompareTag("Bullet"))
        //    {
        //        Destroy(other.gameObject);
        //    }
        //}
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Car"))
        {
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
        }
    }

    //keeps the chicken from going out of bounds on left or right
    //private void OnTriggerStay2D(Collider2D other)
    //{

    //    other.attachedRigidbody.AddForce(-0.1F * other.attachedRigidbody.velocity);

    //}



}


