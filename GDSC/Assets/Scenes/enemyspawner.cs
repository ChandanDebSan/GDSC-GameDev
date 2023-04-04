using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    int carNo;
    public float delayTimer = 1f;
    float timer;
    public float enemyMaxCarLeft;
    public float enemyMaxCarRight;
    // Start is called before the first frame update
    void Start()
    {
        timer = delayTimer;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            Vector3 carPos = new Vector3(Random.Range(enemyMaxCarLeft, enemyMaxCarRight), transform.position.y, transform.position.z);
                carNo = Random.Range(0, 2);
            Instantiate(enemyPrefabs[carNo], carPos, transform.rotation);
            timer = delayTimer;
         }
    }
}
