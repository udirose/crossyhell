using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PowerUp

//help from https://www.youtube.com/watch?v=S61J3kDQ5Mk
{
    //put it in our asset menu to organize
[CreateAssetMenu(menuName = "Scripts/PowerUp/InvinciblePowerup")]
    public class MakeInvincible : Powerups
    {

       
        //change the player's tag to be invincible player - doesn't have a capsule collider
        public override void ApplyPowerup(GameObject player)
        {
            Debug.Log(player.tag);
            player.tag = "InvinciblePlayer";
            Debug.Log(player.tag);

        }
    }
}