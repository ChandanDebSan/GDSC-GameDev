using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 destination;
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField]private float checkDelay;

    

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private playerLife playerlife;
    private float checkTimer;
    private bool attacking;
    private Vector3[] directions = new Vector3[4];
   


    private void Update()
    {
        if (attacking)
        transform.Translate(destination * Time.deltaTime* speed);
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }
    private void CheckForPlayer()
    {
        calculateDirections();
        for(int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i],Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if(hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
                playerlife = hit.transform.GetComponent<playerLife>();
            }
        }
    }
    private void calculateDirections()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }
    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<playerLife>().Die();
        }
        Stop();
    }
    private void OnEnable()
    {
        Stop();
    }
   
}
