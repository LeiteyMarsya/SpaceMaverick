using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Tharsinaa and Marsya

[System.Serializable]
public class LootTable
{
    [field: SerializeField]
    public List<(float, PowerUpController)> PowerUps { get; private set; }
}
