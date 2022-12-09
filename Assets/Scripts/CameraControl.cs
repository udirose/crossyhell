using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Obstacles;
using PowerUp;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraControl : MonoBehaviour
{
    
    //heavily inspired by this tutorial https://www.youtube.com/watch?v=H6q-Y5JAiDk (vertical infinite scrolling)
    public Transform target;

    public Transform bg1;
    public Transform bg2;
    private float size;
   
    //setters
    private Vector3 cameraTargetPos = new Vector3();
    private Vector3 bg1TargetPos = new Vector3();
    private Vector3 bg2TargetPos = new Vector3();
    


    // Start is called before the first frame update
    void Start()
    {

        var targetTransformPos = target.transform.position;
        var cameraTransformPos = transform.position;
        size = bg1.GetComponent<Transform>().localScale.y;
        transform.position = SetPos(cameraTargetPos, cameraTransformPos.x, targetTransformPos.y + 5f, cameraTransformPos.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var transformPos = transform.position;
        Vector3 targetPos = SetPos(cameraTargetPos, transformPos.x, target.position.y+5f, transformPos.z);
        transform.position = Vector3.Lerp(transformPos, targetPos, .5f);
        
        var bg1Pos = bg1.position;
        var bg2Pos = bg2.position;
        //background
        if (transform.position.y-5> bg2.position.y)
        {
            bg1.position = SetPos(bg1TargetPos, bg1Pos.x, bg2.position.y + size, bg1Pos.z);
            SwitchBackground();
        }
        if (transform.position.y < bg1.position.y-6)
        {
            bg2.position = SetPos(bg2TargetPos, bg2Pos.x, bg1.position.y - size, bg2Pos.z);
            SwitchBackground();
        }
    }

    void SwitchBackground()
    {
        GameController.Instance.MoveBottomBound();
        SpawnObstacles.Instance.SpawnSomeObstacles();
        (bg1, bg2) = (bg2, bg1);
    }
    private Vector3 SetPos(Vector3 pos, float x, float y, float z)
    {
        pos.x = x;
        pos.y = y;
        pos.z = z;
        return pos;
    }
}
