using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trackmover : MonoBehaviour
{
    public float scrollSpeed;
    Vector3 intiPos;
    // Start is called before the first frame update
    void Start()
    {
        intiPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y > (intiPos + new Vector3(0, -51 ,0)).y)
        transform.position += Vector3.down * scrollSpeed * Time.deltaTime;
        else
            transform.position = intiPos;
        
    }
}
