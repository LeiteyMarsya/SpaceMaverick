using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float RotationSpeed;
    public Vector2 Speed;
    public AsteroidController OnDestroyTemplate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateAsteroid();
        MoveAsteroid();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LaserControl asLaser = other.GetComponent<LaserControl>();
        if (asLaser != null)
        {
            OnLaserHit(asLaser);
        }
    }

    private void OnLaserHit(LaserControl Laser)
    {
        Destroy(Laser.gameObject);
        if (OnDestroyTemplate != null)
        {
            AsteroidController newObj = Instantiate(OnDestroyTemplate);
            newObj.transform.position = this.transform.position;
            newObj.RotationSpeed = RotationSpeed;
            newObj.Speed = Speed;
        }
        Destroy(this.gameObject);
    }

    private void RotateAsteroid()
    {
        float newZ = transform.rotation.eulerAngles.z + (RotationSpeed * Time.deltaTime);
        Vector3 newR = new(0, 0, newZ);
        transform.rotation = Quaternion.Euler(newR);
    }

    public void MoveAsteroid()
    {
        float newX = transform.position.x + (Speed.x * Time.deltaTime);
        float newY = transform.position.y + (Speed.y * Time.deltaTime);
        transform.position = new Vector2(newX, newY);
    }
}
