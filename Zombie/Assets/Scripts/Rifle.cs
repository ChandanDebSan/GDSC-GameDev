using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Rifle Things")]
    public Camera cam;
    public float giveDamageOf = 10f;
    public float shootingRange = 100f;
    public float fireCharge = 15f;
    private float nextTimeToShoot = 0f;
    public PlayerScript player;
    public Transform hand;
    public Animator animator;

    [Header("Handle Animation and Shootinh")]
    private int maximumAmmunition = 32;
    public int mag = 10;
    private int presentAmmunition;
    public float reloadingTime = 1.3f;
    private bool setRealoading = false;

    [Header("Rifle Effects")]
    public ParticleSystem muzzleSparrk;
    public GameObject WoddenEffect;
    public GameObject goreEffect;

    private void Awake()
    {
        transform.SetParent(hand);
        presentAmmunition = maximumAmmunition;
    }
    private void Update()
    {
        if (setRealoading)
            return;

        if(presentAmmunition <= 0)
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToShoot)
        {
            //--------------Fire Animation-----------
            animator.SetBool("Fire", false);
            animator.SetBool("FireWalk", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("Reloading", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            animator.SetBool("Punch", false);
            //------------------------------------------

            nextTimeToShoot = Time.time + 1f / fireCharge;
            shoot();
        }
        else if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow))
        {
            //--------Rifle Shoot Walk Animation-------
            animator.SetBool("Fire", false);
            animator.SetBool("FireWalk", true);
            animator.SetBool("IdleAim", true);
            animator.SetBool("RifleWalk", true);
            animator.SetBool("Reloading", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            animator.SetBool("Punch", false);
            //--------------------------------
        }

        else if (Input.GetButton("Fire2") && Input.GetButton("Fire1"))
        {
            //--------------Fire Animation-----------
            animator.SetBool("Fire", true);
            animator.SetBool("FireWalk", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("Reloading", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            animator.SetBool("Punch", false);
            //-------------------------------------
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("FireWalk", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("Reloading", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            animator.SetBool("Punch", false);
        }
        
    }
    private void shoot()
    {

        if(mag == 0)
        {
            //ammo out text
            return;
        }

        presentAmmunition--;
        if(presentAmmunition == 0)
        {
            mag--;
        }

        //updating UI


        muzzleSparrk.Play();
        RaycastHit hitInfo;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, shootingRange))
        {
            Debug.Log(hitInfo.transform.name);

            ObjectToHit objectToHit = hitInfo.transform.GetComponent<ObjectToHit>();
            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();
            if (objectToHit != null)
            {
                objectToHit.ObjecthitDamage(giveDamageOf);
                GameObject WoodGo = Instantiate(WoddenEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(WoodGo,1f);
            }
            else if(zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamageOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamageOf);
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
        }
        
        

    }
    IEnumerator Reload()
    {
        player.playerSpeed = 0f;
        player.playerSprint = 0f;
        setRealoading = true;

        Debug.Log("Reloading....");
        animator.SetBool("Fire", false);
        animator.SetBool("FireWalk", false);
        animator.SetBool("IdleAim", true);
        animator.SetBool("RifleWalk", false);
        animator.SetBool("Reloading", true);
        animator.SetBool("Idle", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Running", false);
        animator.SetBool("Punch", false);

        //play sound
        yield return new WaitForSeconds(reloadingTime);
        presentAmmunition = maximumAmmunition;
        player.playerSpeed = 1.9f;
        player.playerSprint = 3f;
        setRealoading = false;

    }

}
