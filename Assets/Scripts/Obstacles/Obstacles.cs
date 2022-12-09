using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Obstacles
{
    public abstract class Obstacles : ScriptableObject
    //this is abstract so that we can make powerups that do diff things like give you more health or make you go faster
    {
        public abstract void HitObstacle(GameObject player);
    }
}
