using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myBomb : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<Collider>().enabled = false;
            FindObjectOfType<myGameManager>().Explode();
           

        }
    }
}
