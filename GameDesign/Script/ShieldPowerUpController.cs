using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShieldPowerUpController : PowerUpController
{
    public override void OnCollect(PlayerController player)
    {
        player.ShieldPower++;
        base.OnCollect(player);
    }
}
