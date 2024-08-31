using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [Header("ZombieSpawn var")]
    public GameObject zombiePrefab;
    public Transform zombieSpawnPositionp;
    private float repeatCycle = 1f;
    public GameObject dangerZone1;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 1f, repeatCycle);
            Destroy(gameObject, 10f);
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    void EnemySpawner()
    {
        
        Instantiate(zombiePrefab, zombieSpawnPositionp.position, zombieSpawnPositionp.rotation);
    }
}
