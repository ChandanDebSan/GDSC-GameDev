using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myBomb : MonoBehaviour
{
    public AudioSource bombSource;
    private void Awake()
    {
        
        bombSource = gameObject.GetComponent<AudioSource>();
    }
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<myGameManager>().Explode(bombSource);
           

        }
    }
}
