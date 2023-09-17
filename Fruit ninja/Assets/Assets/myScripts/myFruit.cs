using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myFruit : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody fruitRigidBody;
    private Collider fruitCollider;
    private ParticleSystem juiceParticleEffect;

    private void Awake()
    {
        fruitRigidBody = GetComponent<Rigidbody>();
        fruitCollider = GetComponent<Collider>();
        juiceParticleEffect = GetComponentInChildren<ParticleSystem>();   
    }
    private void Slice(Vector3 direction, Vector3 position, float force)
    {
        FindObjectOfType<myGameManager>().IncreaseScore();
        whole.SetActive(false);
        sliced.SetActive(true);
        juiceParticleEffect.Play();
        fruitCollider.enabled = false;
        float angle = Mathf.Atan2(direction.y, direction.x)* Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f,0f,angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody slice in slices) { 
            slice.velocity = fruitRigidBody.velocity;
            slice.AddForceAtPosition(direction * force, position, ForceMode.Impulse);
        }
    }
   
    // Start is called before the first frame update
   

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            myBlade blade = other.GetComponent<myBlade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);

        }
    }
}
