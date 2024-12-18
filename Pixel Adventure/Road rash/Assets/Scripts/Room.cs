using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] enemies;
    private Vector3[] initialPositin;
    private void Awake()
    {
        initialPositin = new Vector3[enemies.Length];
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                initialPositin[i] = enemies[i].transform.position;
            }
        }


    }
    public void ActivateRoom(bool _status)
    {
        for(int  i = 0; i< enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].SetActive(_status);
                enemies[i].transform.position = initialPositin[i];
            }
        }
    }
}
