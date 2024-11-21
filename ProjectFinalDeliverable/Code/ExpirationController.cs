using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zalikha


[RequireComponent(typeof(SpriteRenderer))]
public class ExpirationController : MonoBehaviour
{
    [field:SerializeField]
    public float TimeRemaining { get; private set; }

    public bool IsFlickering => TimeRemaining < 3;

    public bool IsExpired => TimeRemaining <= 0;

    public bool IsVisible => !IsFlickering || Mathf.Sin(Time.time * 20) > 0;

    private SpriteRenderer _renderer;



    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        TimeRemaining -= Time.deltaTime;
        _renderer.enabled = IsVisible;
        if (IsExpired)
        {
            Destroy(this.gameObject);
        }
    }

}
