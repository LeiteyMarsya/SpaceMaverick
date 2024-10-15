using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [field: SerializeField]
    public PlayerController PlayerTemplate { get; private set; } // Fix the class name here

    [field: SerializeField]
    public Transform PlayerSpawnPoint { get; private set; }

    [field: SerializeField]
    public float SpawnDelay { get; private set; } = 3;

    [field: SerializeField]
    public float SpawnAt { get; private set; } = -1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnAt > 0 && Time.time > SpawnAt)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        PlayerController pc = Instantiate(PlayerTemplate);
        pc.GameController = this;
        pc.transform.position = PlayerSpawnPoint.position;
        SpawnAt = -1;
    }

    public void DestroyPlayer(PlayerController toDestroy)
    { 
        Destroy(toDestroy.gameObject);
        SpawnAt = Time.time + SpawnDelay;
    }
}
