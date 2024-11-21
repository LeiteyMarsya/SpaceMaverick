using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    public void Explode(DestructableController destructable)
    {
        ExplosionController e = Instantiate (this, destructable.transform.position, Quaternion.identity);
        e.gameObject.SetActive (true);
    }
}
