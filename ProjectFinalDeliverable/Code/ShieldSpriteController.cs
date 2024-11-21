using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tharsinaa and Norleitey Marsya


[RequireComponent(typeof(SpriteRenderer))]
public class ShieldSpriteController : MonoBehaviour
{
    public void Render(PlayerController player)
    {
        GetComponent<SpriteRenderer>().enabled = player.ShieldPower > 0;
    }
}
