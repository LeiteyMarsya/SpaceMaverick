using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class PowerUpController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) return;
        OnCollect(player);
    }

    public virtual void OnCollect(PlayerController player)
    {
        Destroy(this.gameObject);
    }
}
