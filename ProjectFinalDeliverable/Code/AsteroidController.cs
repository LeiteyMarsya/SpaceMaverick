using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//Fatin Naqila and Norleitey Marsya

[RequireComponent(typeof(DestructableController))]
public class AsteroidController : MonoBehaviour
{
    [field:SerializeField]
    public float RotationSpeed { get; private set; }

    [field:SerializeField]
    public Vector2 Speed {  get; private set; }

    [field: SerializeField]
    private GameController GameController;

    [field: SerializeField]
    public GameObject onDestroyTemplate;


    public static AsteroidController Spawn (AsteroidController template, float rotationSpeed, Vector2 speed, GameController gameController)
    {
        AsteroidController newAsteroid = Instantiate (template);
        newAsteroid.GameController = gameController;
        newAsteroid.GetComponent<DestructableController>().GameController = gameController;
        newAsteroid.RotationSpeed = rotationSpeed;
        newAsteroid.Speed = speed;
        return newAsteroid;
    }
    // Update is called once per frame
    void Update()
    {
        RotateAsteroid();
        MoveAsteroid();
    }

    private void OnDestroy()
    {
        if (onDestroyTemplate != null)
        {
            AsteroidController newObj = Spawn(onDestroyTemplate.GetComponent<AsteroidController>(), RotationSpeed, Speed, GameController);
            newObj.transform.position = this.transform.position;
        }
    }

    public void OnLaserHit(LaserController Laser, DestructableController destructable)
    {
        destructable.DefaultDestroy(Laser);
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
