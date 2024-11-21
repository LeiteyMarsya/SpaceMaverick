using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tharsinaa and Marsya

public class LootTableController : MonoBehaviour
{
    [field: SerializeField]
    public List<PowerUpController> PowerUps { get; private set; }

    [field: SerializeField]
    public float DropProbability { get; private set; }

    public void CheckDrop(DestructableController destructable)
    {
        float chance = Random.Range(0, 1);
        if (chance > DropProbability) return;
        int ix = Random.Range(0, PowerUps.Count);
        PowerUpController powerUp = PowerUps[ix];
        Instantiate(powerUp, destructable.transform.position, Quaternion.identity);
    }
}
