using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Obstacles
{
    //put it in our asset menu to organize
    [CreateAssetMenu(menuName = "Scripts/Obstacle/SmallObstacle")]


    public class SmallObstacle : Obstacles
    {
        //extends the obstacles script
        public float amt;

        public override void HitObstacle(GameObject playerHit)
        {
            
            //var player = playerHit.GetComponent<Player>();

            //    player.healthBar.GetComponent<Healthbar>().slider.value -= amt;
            //    player.currentHealth -= (int)amt;
            //Debug.Log(playerHit.tag);
            //playerHit.tag = "InvinciblePlayer";
            //Debug.Log(playerHit.tag);
        }
    }
}

