using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

//Akmal

public class SpeedController : MonoBehaviour
{
    [field:SerializeField]
    public Vector2 Speed {  get; set; }


    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void SpeedY (float y) => Speed = new (Speed.x, y);
    public void SpeedX(float x) => Speed = new(x, Speed.y);


    public virtual void Move()
    {
       transform.position = (Vector2)transform.position + (Speed * Time.deltaTime);
    }
}
