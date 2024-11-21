using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fatin Naqila and Norleitey Marsya

public class PlayerImpactor : MonoBehaviour
{
    public virtual void OnImpact(PlayerController player)
    {
        Destroy(this.gameObject);
    }
}
