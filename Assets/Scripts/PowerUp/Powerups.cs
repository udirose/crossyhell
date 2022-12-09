using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//cited from https://www.youtube.com/watch?v=PkNRPOrtyls 
public abstract class Powerups : ScriptableObject
    //this is abstract so that we can make powerups that do diff things like give you more health or make you go faster
{
    public abstract void ApplyPowerup(GameObject player);
}
