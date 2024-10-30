using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserControl : MonoBehaviour
{
    // Start is called before the first frame update
    [field:SerializeField]
    public Vector2 Speed { get; private set; } = new Vector2(0, -5);

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestructableController destructable = collision.GetComponent<DestructableController>();
        if (destructable != null)
        {
            destructable.DestroyObject(); // Assuming DestroyObject handles destruction and scoring
            Destroy(gameObject); // Destroy the laser
        }
    }

}
