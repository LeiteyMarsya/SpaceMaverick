using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector2 Speed = new Vector2(0, 6);
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveLaser();
    }


    private void moveLaser()
    {

        float newX = transform.position.x + (Speed.x * Time.deltaTime);
        float newY = transform.position.y + (Speed.y * Time.deltaTime);
        transform.position = new Vector2(newX, newY);

    }
}
