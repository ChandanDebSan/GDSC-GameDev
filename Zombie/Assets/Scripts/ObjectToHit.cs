using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectToHit : MonoBehaviour
{
    public float ObjectHealth = 30f;
    public void ObjecthitDamage(float amount)
    {
        ObjectHealth -= amount;
        if(ObjectHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
