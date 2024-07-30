using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header("Camera to Assign")]
    public GameObject Aimcam;
    public GameObject AimCanvas;
    public GameObject ThirdPersonCam;
    public GameObject ThirdPersonCanvas;

    [Header("Camera Animator")]
    public Animator animator;

    private void Update()
    {
        if (Input.GetButton("Fire2") && Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("Fire", false);
            animator.SetBool("FireWalk", false);
            animator.SetBool("IdleAim", true);
            animator.SetBool("RifleWalk", true);
            animator.SetBool("Reloading", false);
            animator.SetBool("Idle", false);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            animator.SetBool("Punch", false);

            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            Aimcam.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else if (Input.GetButton("Fire2"))
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


            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            Aimcam.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else
        {
            animator.SetBool("Fire", false);
            animator.SetBool("FireWalk", false);
            animator.SetBool("IdleAim", false);
            animator.SetBool("RifleWalk", false);
            animator.SetBool("Reloading", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Walk", false);
            animator.SetBool("Running", false);
            animator.SetBool("Punch", false);

            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            Aimcam.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }
}
