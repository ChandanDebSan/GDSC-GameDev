using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class Zombie2 : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Zombie Health and Damage")]
    private float zombieHealth = 100f;
    private float presentHealth;
    public float giveDamage = 5f;
    public Slider slider;
    public Image fill;

    // Start is called before the first frame update
    [Header("ZOmbie Things")]
    public LayerMask PlayerLayer;
    public NavMeshAgent zombieAgent;
    public Transform LookPoint;
    public Transform playerBody;
    public Camera attackingRaycastArea;

    [Header("Zombie Animation")]
    public Animator zom;

    [Header("Zombie Guarding")]
    
    public float zombieSpeed;
    

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
            Idle();
        }
        if (playerInvisionRadius && !playerInAttackingRadius)
        {
            PuresuePlayer();
        }
        if (playerInvisionRadius && playerInAttackingRadius)
        {
            AttackPlayer();
        }
    }
    private void Idle()
    {
        zom.SetBool("Idle", true);
        zom.SetBool("Running", false);
       zombieAgent.SetDestination(transform.position);
    }
    private void PuresuePlayer()
    {
        if (zombieAgent.SetDestination(playerBody.position))
        {
            //animations
            zom.SetBool("Idle", false);
            zom.SetBool("Running", true);
            zom.SetBool("Attacking", false);
            zom.SetBool("Death", false);
        }
       
    }
    private void AttackPlayer()
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
                
                zom.SetBool("Running", false);
                zom.SetBool("Attacking", true);
                
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
            
            zom.SetBool("Death", true);
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
