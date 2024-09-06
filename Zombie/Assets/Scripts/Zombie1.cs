using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class Zombie1 : MonoBehaviour
{
    [Header("Zombie Health and Damage")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;

    // Start is called before the first frame update
    [Header("ZOmbie Things")]
    public LayerMask PlayerLayer;
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Transform playerBody;
    public Camera attackingRaycastArea;
    public Slider slider;
    public Image fill;

    [Header("Zombie Animation")]
    public Animator zom;

    [Header("Zombie Guarding")]
    public GameObject[] walkpoints;

    int currentZombiePosition = 0;
    public float zombieSpeed;
    float walkingPointRadius = 2f;

    [Header("Zombie Attacking Var")]
    public float timeBtwAttack;
    bool previouslyAttack;

    [Header("Zombie States")]
    public float visionRadius;
    public float attackingRadius;
    public bool playerInvisionRadius;
    public bool playerInAttackingRadius;

    private void Awake()
    {
        presentHealth = zombieHealth;
        slider.value = presentHealth;
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInvisionRadius = Physics.CheckSphere(transform.position, visionRadius, PlayerLayer);
        playerInAttackingRadius = Physics.CheckSphere(transform.position, attackingRadius, PlayerLayer);
        if (!playerInAttackingRadius && !playerInvisionRadius)
        {
            Guard();
        }
        if(playerInvisionRadius && !playerInAttackingRadius)
        {
            PuresuePlayer();
        }
        if (playerInvisionRadius && playerInAttackingRadius)
        {
            AttackPlayer();
        }
    }
    private void Guard()
    {
        if (Vector3.Distance(walkpoints[currentZombiePosition].transform.position, transform.position) < walkingPointRadius)
        {
            currentZombiePosition = Random.Range(0, walkpoints.Length);
            if(currentZombiePosition >= walkpoints.Length)
            {
                currentZombiePosition = 0;

            }
        }
        transform.position = Vector3.MoveTowards(transform.position, walkpoints[currentZombiePosition].transform.position, Time.deltaTime * zombieSpeed) ;
        //change zombie facing
        transform.LookAt(walkpoints[currentZombiePosition].transform.position);
    }
    private void PuresuePlayer()
    {
       if(zombieAgent.SetDestination(playerBody.position))
        {
            //animations
            zom.SetBool("Walking", false);
            zom.SetBool("Running", true);
            zom.SetBool("Attacking", false);
            zom.SetBool("Dead", false);
        }
        else
        {
            zom.SetBool("Walking", false);
            zom.SetBool("Running", false);
            zom.SetBool("Attacking", false);
            zom.SetBool("Dead", true);
        }
    }
    private  void AttackPlayer()
    {
        zombieAgent.SetDestination(transform.position);
        transform.LookAt(LookPoint);
        if (!previouslyAttack)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(attackingRaycastArea.transform.position, attackingRaycastArea.transform.forward, out hitInfo, attackingRadius))
            {
                Debug.Log("Attacking" + hitInfo.transform.name);

                PlayerScript playerBodyScript = hitInfo.transform.GetComponent<PlayerScript>();

                if (playerBodyScript != null)
                {
                    playerBodyScript.playerHitDamage(giveDamage);
                }
                zom.SetBool("Walking", false);
                zom.SetBool("Running", true);
                zom.SetBool("Attacking", true);
                zom.SetBool("Dead", false);
            }

            previouslyAttack = true;
            Invoke(nameof(ActiveAttacking), timeBtwAttack);
        }
    }
    private void ActiveAttacking()
    {
        previouslyAttack = false;
    }
    public void zombieHitDamage(float takeDamage)
    {
        presentHealth -= takeDamage;
        slider.value = presentHealth;
        if (presentHealth <= 0)
        {
            zom.SetBool("Walking", false);
            zom.SetBool("Running", false);
            zom.SetBool("Attacking", false);
            zom.SetBool("Dead", true);
            zombieDie();
        }
    }
    private void zombieDie()
    {
        zombieAgent.SetDestination(transform.position);
        zombieSpeed = 0f;
        attackingRadius = 0f;
        visionRadius = 0f;
        playerInAttackingRadius = false;
        playerInvisionRadius = false;
        Object.Destroy(gameObject, 5.0f);
    }
}
