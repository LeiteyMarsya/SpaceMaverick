using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//Aisyah Aida

public class OnScreenController : MonoBehaviour
{
    public UnityEvent OnDestroyed;
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -7 || transform.position.y > 7)
        {
            Destroy(this.gameObject);
            OnDestroyed.Invoke();
        }
    }
}
