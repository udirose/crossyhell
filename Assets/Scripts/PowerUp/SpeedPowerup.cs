using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//put it in our asset menu to organize
[CreateAssetMenu(menuName = "Scripts/PowerUp/SpeedPowerup")]

public class SpeedPowerup : Powerups
{
    public float amt;

    public override void ApplyPowerup(GameObject player)
    {

        player.GetComponent<Player>().mySpeed += amt;
       
       

    }
   

}

