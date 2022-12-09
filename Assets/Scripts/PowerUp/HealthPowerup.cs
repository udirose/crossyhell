using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//put it in our asset menu to organize
[CreateAssetMenu(menuName = "Scripts/PowerUp/HealthPowerup")]

//extends the powerups script
public class HealthPowerup : Powerups
{
    public float amt;


   public override void ApplyPowerup(GameObject playerToHeal)
   {
        var player = playerToHeal.GetComponent<Player>();
        if (player.currentHealth < player.maxHealth)
        {
            player.healthBar.GetComponent<Healthbar>().slider.value += amt;
            player.currentHealth += (int)amt;
        }
   }
}
