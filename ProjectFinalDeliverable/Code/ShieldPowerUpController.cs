using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tharsinaa and Norleitey Marsya

public class ShieldPowerUpController : PowerUpController
{
    public override void OnCollect(PlayerController player)
    {
        player.ShieldPower++;
        base.OnCollect(player);
    }
}
