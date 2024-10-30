using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collision2D))]

public class DestructableController : MonoBehaviour
{
    [field:SerializeField]
    public GameController GameController {  get; set; }

    [field: SerializeField]
    private UnityEvent<LaserController, DestructableController> OnHit { get; set; }

    [field: SerializeField]
    private int scoreValue { get; set; }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        LaserControl laser = collision.gameObject.GetComponent<LaserControl>();
        if (laser != null)
        {
            // Destroy this object and the laser upon collision
            Destroy(gameObject); // Destroy the asteroid/enemy
            Destroy(laser.gameObject); // Destroy the laser

            // Update the score
            GameController.Instance.AddScore(scoreValue);

            Debug.Log($"Object destroyed, score increased by {scoreValue}!");
        }
    }

    public void DefaultDestroy(LaserController laser)
    {
        GameController.AddScore(scoreValue);
        Destroy(laser.gameObject);
        Destroy(this.gameObject);
    }

    public void DestroyObject()
    {
        GameController.Instance.AddScore(scoreValue); // Update the score
        Destroy(gameObject); // Destroy this object
        Debug.Log($"Object destroyed, score increased by {scoreValue}!");
    }
}
