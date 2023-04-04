using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float enemySpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * enemySpeed * Time.deltaTime);
        if(transform.position.y < -10f)
        {
            Destroy(gameObject);
        }
    }
}
