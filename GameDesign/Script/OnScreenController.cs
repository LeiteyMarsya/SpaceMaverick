using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Vector2 position = gameObject.transform.position;
        if (position.y < -7 || position.y > 7)
        {
            // Comment out for testing purposes:
            // Destroy(this.gameObject);
        }
    }
}
