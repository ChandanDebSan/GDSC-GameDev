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
        
         if (Input.GetButton("Fire2"))
        {
            


            ThirdPersonCam.SetActive(false);
            ThirdPersonCanvas.SetActive(false);
            Aimcam.SetActive(true);
            AimCanvas.SetActive(true);
        }
        else
        {
            

            ThirdPersonCam.SetActive(true);
            ThirdPersonCanvas.SetActive(true);
            Aimcam.SetActive(false);
            AimCanvas.SetActive(false);
        }
    }
}
